using System;

namespace VHSRelay.Interfaces
{
    public abstract class IWriter
    {
        /// <summary>
        /// true if big endian false if little endian
        /// </summary>
        public bool EndianNess;

        public IWriter(bool bigEndian)
        {
            EndianNess = bigEndian;
        }

        /// <summary>
        /// true when the source is available to be written to.
        /// </summary>
        /// <returns></returns>
        public abstract bool CanWrite { get; }

        /// <summary>
        /// Returns a byte[] representation of the object.
        /// </summary>
        /// <returns></returns>
        public abstract byte[] ToArray();

        /// <summary>
        /// Writes a single byte to the source
        /// </summary>
        /// <returns></returns>
        public abstract void WriteByte(byte ToWrite);

        /// <summary>
        /// Writes a byte array to the source
        /// </summary>
        /// <returns></returns>
        public abstract void WriteBytes(byte[] ToWrite);

        /// <summary>
        /// writes a character to the source with no encoding ( straight cast)
        /// </summary>
        /// <returns></returns>
        public void WriteChar(char ToWrite)
        {
            WriteByte((byte)ToWrite);
        }

        /// <summary>
        ///writes a short to the source with selected endianness
        /// </summary>
        /// <returns></returns>
        public void WriteInt16(short ToWrite)
        {
            if (EndianNess)
                WriteInt16BEndian(ToWrite);
            else
                WriteInt16LEndian(ToWrite);
        }

        /// <summary>
        ///writes a short to the source big endian
        /// </summary>
        /// <returns></returns>
        public void WriteInt16BEndian(short ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
            else
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
            }
        }

        /// <summary>
        /// writes a short to the source little endian
        /// </summary>
        /// <returns></returns>
        public void WriteInt16LEndian(short ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
            }
            else
            {
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
        }

        /// <summary>
        /// writes an int to the source respecting endianness
        /// </summary>
        /// <returns></returns>
        public void WriteInt32(int ToWrite)
        {
            if (EndianNess)
                WriteInt32BEndian(ToWrite);
            else
                WriteInt32LEndian(ToWrite);
        }

        /// <summary>
        /// writes an int to the source big endian
        /// </summary>
        /// <returns></returns>
        public void WriteInt32BEndian(int ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[3]);
                WriteByte(bytes[2]);
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
            else
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
                WriteByte(bytes[2]);
                WriteByte(bytes[3]);
            }
        }

        /// <summary>
        /// writes an int to the source little endian
        /// </summary>
        /// <returns></returns>
        public void WriteInt32LEndian(int ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
                WriteByte(bytes[2]);
                WriteByte(bytes[3]);
            }
            else
            {
                WriteByte(bytes[3]);
                WriteByte(bytes[2]);
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
        }

        /// <summary>
        /// writes an long to the source respecitng endianness
        /// </summary>
        /// <returns></returns>
        public void WriteInt64(long ToWrite)
        {
            if (EndianNess)
                WriteInt64BEndian(ToWrite);
            else
                WriteInt64LEndian(ToWrite);
        }

        /// <summary>
        /// writes a long to the source big endian
        /// </summary>
        /// <returns></returns>
        public void WriteInt64BEndian(long ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[7]);
                WriteByte(bytes[6]);
                WriteByte(bytes[5]);
                WriteByte(bytes[4]);
                WriteByte(bytes[3]);
                WriteByte(bytes[2]);
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
            else
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
                WriteByte(bytes[2]);
                WriteByte(bytes[3]);
                WriteByte(bytes[4]);
                WriteByte(bytes[5]);
                WriteByte(bytes[6]);
                WriteByte(bytes[7]);
            }
        }

        /// <summary>
        /// writes a long to the source little endian
        /// </summary>
        /// <returns></returns>
        public void WriteInt64LEndian(long ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
                WriteByte(bytes[2]);
                WriteByte(bytes[3]);
                WriteByte(bytes[4]);
                WriteByte(bytes[5]);
                WriteByte(bytes[6]);
                WriteByte(bytes[7]);
            }
            else
            {
                WriteByte(bytes[7]);
                WriteByte(bytes[6]);
                WriteByte(bytes[5]);
                WriteByte(bytes[4]);
                WriteByte(bytes[3]);
                WriteByte(bytes[2]);
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
        }

        /// <summary>
        /// writes a string to the source no encoding casting each char to byte
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public void WriteStringNoNull(string ToWrite)
        {
            for (int y = 0; y < ToWrite.Length; y++)
                WriteChar(ToWrite[y]);
        }

        /// <summary>
        /// writes a string to the source by casting each character to a byte. then adds a null at the end
        /// </summary>
        /// <returns></returns>
        public void WriteStringWithNull(string ToWrite)
        {
            for (int x = 0; x < ToWrite.Length; x++)//every block starts with its name in a null terminated string.
                WriteChar(ToWrite[x]);
            WriteByte(0);
        }

        /// <summary>
        /// writes a unsigned short to the source respecting endianness
        /// </summary>
        /// <returns></returns>
        public void WriteUInt16(ushort ToWrite)
        {
            if (EndianNess)
                WriteUInt16BEndian(ToWrite);
            else
                WriteUInt16LEndian(ToWrite);
        }

        /// <summary>
        /// writes a unsigned short to the source big endian
        /// </summary>
        /// <returns></returns>
        public void WriteUInt16BEndian(ushort ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
            else
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
            }
        }

        /// <summary>
        /// writes an unsigned short to the source little endian
        /// </summary>
        /// <returns></returns>
        public void WriteUInt16LEndian(ushort ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
            }
            else
            {
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
        }

        /// <summary>
        ///writes a unsigned int to the source respecting endian
        /// </summary>
        /// <returns></returns>
        public void WriteUInt32(uint ToWrite)
        {
            if (EndianNess)
                WriteUInt32BEndian(ToWrite);
            else
                WriteUInt32LEndian(ToWrite);
        }

        /// <summary>
        /// writes an unsigned int to the source big endian
        /// </summary>
        /// <returns></returns>
        public void WriteUInt32BEndian(uint ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[3]);
                WriteByte(bytes[2]);
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
            else
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
                WriteByte(bytes[2]);
                WriteByte(bytes[3]);
            }
        }

        /// <summary>
        /// writes an unsigned int to the source little endian
        /// </summary>
        /// <returns></returns>
        public void WriteUInt32LEndian(uint ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
                WriteByte(bytes[2]);
                WriteByte(bytes[3]);
            }
            else
            {
                WriteByte(bytes[3]);
                WriteByte(bytes[2]);
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
        }

        /// <summary>
        /// writes an unsigned long to the source respecting endian
        /// </summary>
        /// <returns></returns>
        public void WriteUInt64(ulong ToWrite)
        {
            if (EndianNess)
                WriteUInt64BEndian(ToWrite);
            else
                WriteUInt64LEndian(ToWrite);
        }

        /// <summary>
        /// writes an unsigned long to the soruce big endian
        /// </summary>
        /// <returns></returns>
        public void WriteUInt64BEndian(ulong ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[7]);
                WriteByte(bytes[6]);
                WriteByte(bytes[5]);
                WriteByte(bytes[4]);
                WriteByte(bytes[3]);
                WriteByte(bytes[2]);
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
            else
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
                WriteByte(bytes[2]);
                WriteByte(bytes[3]);
                WriteByte(bytes[4]);
                WriteByte(bytes[5]);
                WriteByte(bytes[6]);
                WriteByte(bytes[7]);
            }
        }

        /// <summary>
        /// writes an unsigned long to the source little endian
        /// </summary>
        /// <returns></returns>
        public void WriteUInt64LEndian(ulong ToWrite)
        {
            byte[] bytes = BitConverter.GetBytes(ToWrite);
            if (BitConverter.IsLittleEndian)
            {
                WriteByte(bytes[0]);
                WriteByte(bytes[1]);
                WriteByte(bytes[2]);
                WriteByte(bytes[3]);
                WriteByte(bytes[4]);
                WriteByte(bytes[5]);
                WriteByte(bytes[6]);
                WriteByte(bytes[7]);
            }
            else
            {
                WriteByte(bytes[7]);
                WriteByte(bytes[6]);
                WriteByte(bytes[5]);
                WriteByte(bytes[4]);
                WriteByte(bytes[3]);
                WriteByte(bytes[2]);
                WriteByte(bytes[1]);
                WriteByte(bytes[0]);
            }
        }

        /// <summary>
        /// writes x bytes as is  to the source.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public void WriteX(byte[] ToWrite)
        {
            for (int y = 0; y < ToWrite.Length; y++)
                WriteByte(ToWrite[y]);
        }
    }
}