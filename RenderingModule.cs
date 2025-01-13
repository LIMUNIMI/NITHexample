using System.Windows;
using System.Windows.Threading;

namespace NITHtemplate.Modules
{
    /// <summary>
    /// The RenderingModule class is responsible for managing the rendering of graphics in the application.
    /// It utilizes a DispatcherTimer to repeatedly trigger rendering operations at specified intervals.
    /// </summary>
    public class RenderingModule : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the RenderingModule class.
        /// </summary>
        /// <param name="window">The main window instance where rendering will take place.</param>
        public RenderingModule(MainWindow window)
        {
            Window = window;
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Interval = new TimeSpan(10000); // 10000 nanoseconds = one millisecond
            DispatcherTimer.Tick += DispatcherUpdate;
        }

        private DispatcherTimer DispatcherTimer { get; set; }
        private MainWindow Window { get; set; }

        /// <summary>
        /// Releases the resources used by the RenderingModule.
        /// This stops the rendering timer.
        /// </summary>
        public void Dispose()
        {
            DispatcherTimer.Stop();
        }

        /// <summary>
        /// Starts the rendering timer which will invoke the update method at regular intervals.
        /// </summary>
        public void StartRendering()
        {
            DispatcherTimer.Start();
        }

        /// <summary>
        /// Stops the rendering timer to prevent further updates.
        /// </summary>
        public void StopRendering()
        {
            DispatcherTimer.Stop();
        }

        /// <summary>
        /// This method is called every time the dispatcher timer is triggered to update graphics.
        /// Place rendering instructions inside this method.
        /// </summary>
        private void DispatcherUpdate(object sender, EventArgs e)
        {
            Window.prbMouthGauge.Value = Rack.MappingModule.MouthAperture;
        }
    }
}