using ConsoleEmulation;
using NITHlibrary.Nith.Behaviors;
using NITHlibrary.Nith.Internals;
using NITHlibrary.Nith.Module;

namespace NITHexample.Behaviors
{
    internal class ErrorToStringBehavior : ANithErrorToStringBehavior
    {
        ConsoleTextToTextBox consoleTextToTextBox;

        public ErrorToStringBehavior(NithModule nithModule, ConsoleTextToTextBox consoleTextToTextBox) : base(nithModule)
        {
            this.consoleTextToTextBox = consoleTextToTextBox;
        }

        protected override void ErrorActions(string errorString, NithErrors error)
        {
            consoleTextToTextBox.WriteLine(errorString);
        }
    }
}
