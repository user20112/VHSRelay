using System;

namespace VHSRelay.Interfaces
{
    public abstract class ISerial : IReader, IDisposable
    {
        public ISerial(bool Endianness) : base(Endianness)
        {
        }

        /// <summary>
        ///     called whenever data is available for ReadExiting
        /// </summary>
        public EventHandler<EventArgs> DataReceived { get; set; }

        /// <summary>
        ///     returns true if Open has been sucessfully called
        /// </summary>
        public abstract bool IsOpen { get; }

        /// <summary>
        ///     Close the Serial Device
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// disposes of everything
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        ///     Open the Serial Device
        /// </summary>
        public abstract void Open();

        /// <summary>
        ///     Read All available data from the Serial Device
        /// </summary>
        /// <returns></returns>
        public abstract string ReadExisting();

        /// <summary>
        ///     Write an entire string to the serial device
        /// </summary>
        /// <param name="ToWrite"></param>
        public abstract void Write(string ToWrite);

        /// <summary>
        ///     write a single byte on the serial device.
        /// </summary>
        /// <param name="ToWrite"></param>
        public abstract void WriteByte(byte ToWrite);
    }
}