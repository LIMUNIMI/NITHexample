using ConsoleEmulation;
using NITHexample.Behaviors;
using NITHlibrary.Nith.Internals;
using NITHlibrary.Nith.Module;
using NITHlibrary.Nith.PortDetector;
using NITHlibrary.Nith.Preprocessors;
using NITHlibrary.Nith.Wrappers.NithWebcamWrapper;
using NITHlibrary.Tools.Ports;
using NITHtemplate.Modules;

namespace NITHtemplate.Setups
{
    /// <summary>
    /// The DefaultSetup class is responsible for initializing and configuring the necessary modules and connections for the application.
    /// It manages the creation of various modules such as mapping, rendering, and communication receivers, 
    /// as well as handling cleanup through the Dispose method.
    /// </summary>
    public class DefaultSetup
    {
        /// <summary>
        /// Initializes a new instance of the DefaultSetup class with a specified main window.
        /// </summary>
        /// <param name="window">The main window reference used for rendering.</param>
        public DefaultSetup(MainWindow window)
        {
            Window = window;
            Disposables = new List<IDisposable>();
        }

        private List<IDisposable> Disposables { get; set; }
        private MainWindow Window { get; set; }

        /// <summary>
        /// Launches the setup actions for the application.
        /// This method initializes various modules, connects them, 
        /// and starts the rendering process for visual output.
        /// </summary>
        public void Setup()
        {
            // Create the Console emulator using the textbox in the main window
            Rack.ConsoleTextToTextBox = new ConsoleTextToTextBox(this.Window.txtConsole, this.Window.scrConsole);

            // Make the USB receivers
            Rack.UDPreceiver = new UDPreceiver();

            // Make the NITH module
            Rack.NithModule = new NithModule();

            // Connect the NITH module to the ports as a listener
            Rack.UDPreceiver.Listeners.Add(Rack.NithModule);

            // Add behaviors
            Rack.NithModule.SensorBehaviors.Add(new MouthClickBehavior());
            Rack.NithModule.ErrorBehaviors.Add(new ErrorToStringBehavior(Rack.NithModule, Rack.ConsoleTextToTextBox));

            // Add preprocessors
            Rack.NithModule.Preprocessors.Add(new NithPreprocessor_WebcamWrapper());
            List<NithParameters> ParamsToFilter = new List<NithParameters> { NithParameters.mouth_ape };
            Rack.NithModule.Preprocessors.Add(new NithPreprocessor_MAfilterParams(ParamsToFilter, 0.5f));

            // Extra modules
            Rack.MappingModule = new MappingModule(Window.prbMouthGauge);
            Rack.RenderingModule = new RenderingModule(Window);

            // Add disposables to list
            Disposables.Add(Rack.RenderingModule);
            Disposables.Add(Rack.UDPreceiver);
            Disposables.Add(Rack.NithModule);

            // Fire up the RenderingModule. You will probably want to leave this at the end!
            Rack.RenderingModule.StartRendering();
        }

        /// <summary>
        /// Disposes all the disposable modules to free up resources.
        /// This method should be called upon program exit to ensure proper cleanup.
        /// </summary>
        public void Dispose()
        {
            foreach (IDisposable disposable in Disposables)
            {
                disposable.Dispose();
            }
        }
    }
}