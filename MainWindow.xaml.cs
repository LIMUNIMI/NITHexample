using ConsoleEmulation;
using NITHtemplate.Modules;
using NITHtemplate.Setups;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NITHtemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConsoleTextToTextBox _consoleRedirector;
        private DefaultSetup setup;

        public MainWindow()
        {
            InitializeComponent();
            setup = new DefaultSetup(this);
            setup.Setup();
            _consoleRedirector = new ConsoleTextToTextBox(txtConsole, scrConsole);
            Console.SetOut(_consoleRedirector);
        }

        private void btnScanHT_Click(object sender, RoutedEventArgs e)
        {
            Rack.USBportDetector.Scan();
        }

        private void btnConnectUDP_Click(object sender, RoutedEventArgs e)
        {
            Rack.UDPreceiver.Connect();
        }
    }
}