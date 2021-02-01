using HidSharp;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHSRelay.Interfaces;

namespace VHSRelay.CommunicationAbstractions
{
    public class HIDSharpISerial : ISerial
    {
        private SerialDevice NativeDevice;
        private SerialStream NativeStream;

        private ConcurrentQueue<byte> ReceivedData = new System.Collections.Concurrent.ConcurrentQueue<byte>();

        public HIDSharpISerial(string ID, bool Endianness) : base(Endianness)
        {
            DeviceList list = DeviceList.Local;
            SerialDevice[] serialDeviceList = list.GetSerialDevices().ToArray();
            foreach (SerialDevice device in serialDeviceList)
                if (device.DevicePath == ID)
                    NativeDevice = device;
            if (NativeDevice == null)
                throw new System.Exception("Not Found Exception");
        }

        public override bool CanBeRead => !ReceivedData.IsEmpty;

        public override bool IsOpen => NativeStream != null;

        public static List<string> Scan()
        {
            DeviceList list = DeviceList.Local;
            SerialDevice[] serialDeviceList = list.GetSerialDevices().ToArray();
            List<string> ReturnData = new List<string>();
            foreach (SerialDevice device in serialDeviceList)
                if (device.ToString().Contains("COM11"))
                    ReturnData.Add(device.DevicePath);
            return ReturnData;
        }

        public override void Close()
        {
            NativeStream.Close();
            NativeStream = null;
        }

        public override void Dispose()
        {
            if (IsOpen)
                Close();
            if (NativeDevice == null)
                NativeDevice = null;
        }

        public override void Open()
        {
            NativeStream = NativeDevice.Open();
            Task.Run(() =>
            {
                while (IsOpen)
                    try
                    {
                        int temp = NativeStream.ReadByte();
                        if (temp != -1)
                        {
                            ReceivedData.Enqueue((byte)temp);
                            DataReceived?.Invoke(this, new System.EventArgs());
                        }
                    }
                    catch
                    {
                    }
            });
        }

        public override byte[] ReadAllBytes()
        {
            byte[] temp = ReceivedData.ToArray();
            ReceivedData.Clear();
            return temp;
        }

        public override byte ReadByte()
        {
            bool found = ReceivedData.TryDequeue(out byte result);
            if (found)
                return result;
            return 0;
        }

        public override string ReadExisting()
        {
            string temp = Encoding.ASCII.GetString(ReceivedData.ToArray());
            ReceivedData.Clear();
            return temp;
        }

        public override void Write(string ToWrite)
        {
            NativeStream.Write(ToWrite);
        }

        public override void WriteByte(byte ToWrite)
        {
            NativeStream.WriteByte(ToWrite);
        }
    }
}