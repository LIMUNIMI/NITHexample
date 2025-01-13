using NITHemulation.Modules.Mouse;
using NITHlibrary.Nith.Internals;
using NITHlibrary.Tools.Mappers;
using NITHtemplate.Modules;
using System.Globalization;
using System.Windows;

namespace NITHexample.Behaviors
{
    internal class MouthClickBehavior : INithSensorBehavior
    {
        private SegmentMapper mapper = new SegmentMapper(0, 100, 0, 50);
        private double mouthAperture;
        private bool mouthOpen = false;

        public void HandleData(NithSensorData nithData)
        {
            // Check if the mouth aperture parameter is present in the data before trying to access it
            if (nithData.ContainsParameter(NithParameters.mouth_ape))
            {
                // Remap the mouth aperture value from the range [0, 100] to [0, 50]
                mouthAperture = double.Parse(nithData.GetParameterValue(NithParameters.mouth_ape).Value.Base);
                mouthAperture = mapper.Map(mouthAperture);

                // Sending the value to the mapping module for memorization prior to rendering
                Rack.MappingModule.MouthAperture = mouthAperture;

                // ^^ sum mouth aperture to a new float 5f
            }

            // Check if the mouth is open parameter is present in the data before trying to access it
            if (nithData.ContainsParameter(NithParameters.mouth_isOpen))
            {
                bool mo = nithData.GetParameterValue(NithParameters.mouth_isOpen).Value.BaseAsBool;

                //Console.WriteLine(mo.ToString());

                // Use the mouth is open parameter to send mouse clicks
                if (mo != mouthOpen)
                {
                    mouthOpen = mo;
                    if (mouthOpen)
                    {
                        MouseSender.SendMouseButtonEvent(MouseButtonFlags.LeftDown);
                    }
                    else
                    {
                        MouseSender.SendMouseButtonEvent(MouseButtonFlags.LeftUp);
                    }
                }
            }

            //// 10% and 20% of the maximum mouth aperture are set as a double threshold for mouse clicks. Maximum is 100, minimum is 0.
            //if(mouthAperture > 20 && !clickStatus)
            //{
            //    clickStatus = true;
            //    MouseSender.SendMouseButtonEvent(MouseButtonFlags.LeftDown);
            //}
            //else if(mouthAperture < 10 && clickStatus)
            //{
            //    clickStatus = false;
            //    MouseSender.SendMouseButtonEvent(MouseButtonFlags.LeftUp);
            //}
        }
    }
}
