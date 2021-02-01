namespace VHSRelay.CommunicationAbstractions
{//using AppShell.Interfaces;
    using HidSharp;
    using System;
    using System.Linq;
    using VHSRelay.Interfaces;

    //to reenable this install HIDSharp in nuget manager

    namespace AppShell.DataClasses.InterfaceImplimentations
    {
        public class HIDSharpIHID : IHid
        {
            private HidDevice Device;
            private HidStream hidStream;
            private DeviceList list = DeviceList.Local;

            public HIDSharpIHID()
            {
                list.Changed += DevicesChanged;
                ListOfDevices = list.GetHidDevices().ToArray();
            }

            public bool Connected { get; set; } = false;
            public string DeviceId { get { return Device.VendorID + "&" + Device.ProductID; } set { } }
            public string DevicePath { get { return Device.DevicePath; } set { } }
            public EventHandler<DeviceListChangedEventArgs> DevicesChangedEvent { get; set; } = delegate { };
            public Guid Guid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public HidDevice[] ListOfDevices { get; set; }

            public void Dispose()
            {
                try
                {
                    hidStream.Dispose();
                }
                catch
                {
                }
            }

            public void Load(string devicePath)
            {
                ListOfDevices = list.GetHidDevices().ToArray();
                foreach (HidDevice device in ListOfDevices)
                    if (device.DevicePath == devicePath)
                    {
                        Device = device;
                        if (Device.TryOpen(out hidStream))
                            Connected = true;
                    }
            }

            public byte[] Read()
            {
                if (hidStream.CanRead)
                    return hidStream.Read();
                return new byte[65];
            }

            public string Scan(string DevicePID)
            {
                ListOfDevices = list.GetHidDevices().ToArray();
                foreach (HidDevice device in ListOfDevices)
                    if (device.DevicePath.Contains(DevicePID))
                        return device.DevicePath;
                return "";
            }

            public string ScanFirst()
            {
                ListOfDevices = list.GetHidDevices().ToArray();
                if (ListOfDevices.Count() > 0)
                    return ListOfDevices[0].DevicePath;
                return "";
            }

            public void Write(byte[] buffer, uint cbToWrite)
            {
                hidStream.Write(buffer);
            }

            public void WriteMultiple(byte[] buffer)
            {
                hidStream.Write(buffer);
            }

            private void DevicesChanged(object sender, DeviceListChangedEventArgs e)
            {
                ListOfDevices = list.GetHidDevices().ToArray();
            }
        }
    }
}