using System;

namespace VHSRelay.Events
{
    public class DataReceivedEventArgs:EventArgs
    {
        public byte[] DataRecieved;

        public DataReceivedEventArgs(byte[] data)
        {
            DataRecieved = data;
        }
    }
}