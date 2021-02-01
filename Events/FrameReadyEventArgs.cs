using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace VHSRelay
{
    public class FrameReadyEventArgs : EventArgs
    {
        public Image<Gray, byte> Image; //  non platform specific image.

        public FrameReadyEventArgs(Image<Gray, byte> image)
        {
            Image = image;
        }
    }
}