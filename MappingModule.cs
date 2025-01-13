using System.Windows.Controls;

namespace NITHtemplate
{
    /// <summary>
    /// A blank mapping module, to contain the interaction strategy and mapping. Completely optional, just a tidy suggestion.
    /// </summary>
    public class MappingModule
    {
        private ProgressBar prbMouth;
        public double MouthAperture { get; set; } = 0;

        public MappingModule(ProgressBar prbMouth)
        {
            this.prbMouth = prbMouth;
        }

        public void UpdateMouthAperture(double mouthAperture)
        {
            MouthAperture = mouthAperture;
        }
    }
}