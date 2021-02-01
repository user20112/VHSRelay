using System;

namespace VHSRelay.Interfaces
{
    public abstract class IReader
    {
        /// <summary>
        /// true if big endian otherwise it is little endian.
        /// </summary>
        public bool EndianNess;

        public IReader(bool bigEndian)
        {
            EndianNess = bigEndian;
        }

        /// <summary>
        /// returns weather or not the next byte is available
        /// </summary>
        /// <returns></returns>
        public abstract bool CanBeRead { get; }

        /// <summary>
        /// while data available read bytes into a byte array.
        /// </summary>
        /// <returns></returns>
        public abstract byte[] ReadAllBytes();

        /// <summary>
        ///     Read 1 byte from the Serial Device
        /// </summary>
        /// <returns></returns>
        public abstract byte ReadByte();

        /// <summary>
        /// reads a single byte and casts it to a char (no encoding)
        /// </summary>
        /// <returns></returns>
        public char ReadChar()
        {
            return (char)ReadByte();
        }

        /// <summary>
        /// reads 2 bytes and bitconverts it to a short. Using set endianness
        /// </summary>
        /// <returns></returns>
        public short ReadInt16()
        {
            if (EndianNess)
                return ReadInt16BEndian();
            else
                return ReadInt16LEndian();
        }

        /// <summary>
        /// reads 2 bytes and bitconverts it to a short BigEndian
        /// </summary>
        /// <returns></returns>
        public short ReadInt16BEndian()
        {
            byte[] bytes = new byte[2];
            if (BitConverter.IsLittleEndian)
            {
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            else
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
            }
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// reads 2 bytes and bitconverts it to a short LittleEndian
        /// </summary>
        /// <returns></returns>
        public short ReadInt16LEndian()
        {
            byte[] bytes = new byte[2];
            if (BitConverter.IsLittleEndian)
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
            }
            else
            {
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// reads 4 bytes and bitconverts it to a int32.
        /// </summary>
        /// <returns></returns>
        public int ReadInt32()
        {
            if (EndianNess)
                return ReadInt32BEndian();
            else
                return ReadInt32LEndian();
        }

        /// <summary>
        /// reads 4 bytes and bitconverts it to a int32 BigEndian
        /// </summary>
        /// <returns></returns>
        public int ReadInt32BEndian()
        {
            byte[] bytes = new byte[4];
            if (BitConverter.IsLittleEndian)
            {
                bytes[3] = ReadByte();
                bytes[2] = ReadByte();
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            else
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
                bytes[2] = ReadByte();
                bytes[3] = ReadByte();
            }
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// reads 4 bytes and bitconverts it to a int32 LittleEndian
        /// </summary>
        /// <returns></returns>
        public int ReadInt32LEndian()
        {
            byte[] bytes = new byte[4];
            if (BitConverter.IsLittleEndian)
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
                bytes[2] = ReadByte();
                bytes[3] = ReadByte();
            }
            else
            {
                bytes[3] = ReadByte();
                bytes[2] = ReadByte();
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Reads 8 bytes and bit converts to int64
        /// </summary>
        /// <returns></returns>
        public long ReadInt64()
        {
            if (EndianNess)
                return ReadInt64BEndian();
            else
                return ReadInt64LEndian();
        }

        /// <summary>
        /// Reads 8 bytes and bit converts to int64 BigEndian
        /// </summary>
        /// <returns></returns>
        public long ReadInt64BEndian()
        {
            byte[] bytes = new byte[8];
            if (BitConverter.IsLittleEndian)
            {
                bytes[7] = ReadByte();
                bytes[6] = ReadByte();
                bytes[5] = ReadByte();
                bytes[4] = ReadByte();
                bytes[3] = ReadByte();
                bytes[2] = ReadByte();
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            else
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
                bytes[2] = ReadByte();
                bytes[3] = ReadByte();
                bytes[4] = ReadByte();
                bytes[5] = ReadByte();
                bytes[6] = ReadByte();
                bytes[7] = ReadByte();
            }
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// Reads 8 bytes and bit converts to int64 Little Endian
        /// </summary>
        /// <returns></returns>
        public long ReadInt64LEndian()
        {
            byte[] bytes = new byte[8];
            if (BitConverter.IsLittleEndian)
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
                bytes[2] = ReadByte();
                bytes[3] = ReadByte();
                bytes[4] = ReadByte();
                bytes[5] = ReadByte();
                bytes[6] = ReadByte();
                bytes[7] = ReadByte();
            }
            else
            {
                bytes[7] = ReadByte();
                bytes[6] = ReadByte();
                bytes[5] = ReadByte();
                bytes[4] = ReadByte();
                bytes[3] = ReadByte();
                bytes[2] = ReadByte();
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// reads 2 bytes and bitconverts it to a ushort. Using set endianness
        /// </summary>
        /// <returns></returns>
        public ushort ReadUInt16()
        {
            if (EndianNess)
                return ReadUInt16BEndian();
            else
                return ReadUInt16LEndian();
        }

        /// <summary>
        /// reads 2 bytes and bitconverts it to a ushort BigEndian
        /// </summary>
        /// <returns></returns>
        public ushort ReadUInt16BEndian()
        {
            byte[] bytes = new byte[2];
            if (BitConverter.IsLittleEndian)
            {
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            else
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
            }
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// reads 2 bytes and bitconverts it to a ushort LittleEndian
        /// </summary>
        /// <returns></returns>
        public ushort ReadUInt16LEndian()
        {
            byte[] bytes = new byte[2];
            if (BitConverter.IsLittleEndian)
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
            }
            else
            {
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// reads 4 bytes and bitconverts it to a uint32.
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt32()
        {
            if (EndianNess)
                return ReadUInt32BEndian();
            else
                return ReadUInt32LEndian();
        }

        /// <summary>
        /// reads 4 bytes and bitconverts it to a uint32 BigEndian
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt32BEndian()
        {
            byte[] bytes = new byte[4];
            if (BitConverter.IsLittleEndian)
            {
                bytes[3] = ReadByte();
                bytes[2] = ReadByte();
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            else
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
                bytes[2] = ReadByte();
                bytes[3] = ReadByte();
            }
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// reads 4 bytes and bitconverts it to a uint32 LittleEndian
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt32LEndian()
        {
            byte[] bytes = new byte[4];
            if (BitConverter.IsLittleEndian)
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
                bytes[2] = ReadByte();
                bytes[3] = ReadByte();
            }
            else
            {
                bytes[3] = ReadByte();
                bytes[2] = ReadByte();
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// Reads 8 bytes and bit converts to uint64
        /// </summary>
        /// <returns></returns>
        public ulong ReadUInt64()
        {
            if (EndianNess)
                return ReadUInt64BEndian();
            else
                return ReadUInt64LEndian();
        }

        /// <summary>
        /// Reads 8 bytes and bit converts to uint64 BigEndian
        /// </summary>
        /// <returns></returns>
        public ulong ReadUInt64BEndian()
        {
            byte[] bytes = new byte[8];
            if (BitConverter.IsLittleEndian)
            {
                bytes[7] = ReadByte();
                bytes[6] = ReadByte();
                bytes[5] = ReadByte();
                bytes[4] = ReadByte();
                bytes[3] = ReadByte();
                bytes[2] = ReadByte();
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            else
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
                bytes[2] = ReadByte();
                bytes[3] = ReadByte();
                bytes[4] = ReadByte();
                bytes[5] = ReadByte();
                bytes[6] = ReadByte();
                bytes[7] = ReadByte();
            }
            return BitConverter.ToUInt64(bytes, 0);
        }

        /// <summary>
        /// Reads 8 bytes and bit converts to uint64 Little Endian
        /// </summary>
        /// <returns></returns>
        public ulong ReadUInt64LEndian()
        {
            byte[] bytes = new byte[8];
            if (BitConverter.IsLittleEndian)
            {
                bytes[0] = ReadByte();
                bytes[1] = ReadByte();
                bytes[2] = ReadByte();
                bytes[3] = ReadByte();
                bytes[4] = ReadByte();
                bytes[5] = ReadByte();
                bytes[6] = ReadByte();
                bytes[7] = ReadByte();
            }
            else
            {
                bytes[7] = ReadByte();
                bytes[6] = ReadByte();
                bytes[5] = ReadByte();
                bytes[4] = ReadByte();
                bytes[3] = ReadByte();
                bytes[2] = ReadByte();
                bytes[1] = ReadByte();
                bytes[0] = ReadByte();
            }
            return BitConverter.ToUInt64(bytes, 0);
        }

        /// <summary>
        /// reads until it hits a null character. then reads that null character and returns the string ( usefull for pulling null terminated strings.
        /// </summary>
        /// <returns></returns>
        public string ReadUntilNull()
        {
            string value = "";
            for (byte CurrentByte = ReadByte(); CurrentByte != 0; CurrentByte = ReadByte())//every block starts with its name in a null terminated string.
                value += (char)CurrentByte;
            return value;
        }

        /// <summary>
        /// reads until it hits a null character. then reads that null character and returns the string ( usefull for pulling null terminated strings.
        /// </summary>
        /// <returns></returns>
        public string ReadUntilNull(ref int x)
        {
            string value = "";
            for (byte CurrentByte = ReadByte(); CurrentByte != 0; CurrentByte = ReadByte())//every block starts with its name in a null terminated string.
                value += (char)CurrentByte;
            return value;
        }

        /// <summary>
        /// read x bytes and return the array.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public byte[] ReadX(int x)
        {
            byte[] values = new byte[x];
            for (int y = 0; y < x; y++)
                values[y] = ReadByte();
            return values;
        }

        /// <summary>
        /// reads the next x bytes and returns them as a string with no encoding applied.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public string ReadXCharacters(int x)
        {
            string temp = "";
            for (int y = 0; y < x; y++)
                temp += ReadChar();
            return temp;
        }
    }
}