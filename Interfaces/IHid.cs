using System;

namespace VHSRelay.Interfaces
{
    public interface IHid : IDisposable
    {
        /// <summary>
        ///     returns true if a HID device has been loaded correctly
        /// </summary>
        bool Connected { get; }

        /// <summary>
        ///     returns the loaded device id
        /// </summary>
        string DeviceId { get; set; }

        /// <summary>
        ///     returns the path to the loaded device id
        /// </summary>
        string DevicePath { get; set; }

        /// <summary>
        ///     returns the GUID of the loaded device id
        /// </summary>
        Guid Guid { get; set; }

        /// <summary>
        ///     loads the given device
        /// </summary>
        /// <param name="devicePath"></param>
        void Load(string devicePath);

        /// <summary>
        ///     reads 1 hid packet and returns it as a byte[]
        /// </summary>
        /// <returns></returns>
        byte[] Read();

        /// <summary>
        ///     Scans for a device path that contains the given DevicePID
        /// </summary>
        /// <param name="DevicePID"></param>
        /// <returns></returns>
        string Scan(string DevicePID);

        /// <summary>
        ///     writes the given buffer to the HID Device. correctly adds nulls to pad out to cbToWrite so all platforms act
        ///     identical. (thanks android)
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="cbToWrite"></param>
        void Write(byte[] buffer, uint cbToWrite);
    }
}