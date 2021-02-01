using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VHSRelay.Events;

namespace VHSRelay.CommunicationAbstractions
{
    public class VHSConnection
    {
        public EventHandler<EventArgs> Connected;
        public EventHandler<DataReceivedEventArgs> DataReceived;
        private TcpClient Client;
        private ManualResetEvent Drop = new ManualResetEvent(false);
        private TcpListener Listener;
        private bool OneWay;
        private int Port;
        private bool Reading = false;
        private NetworkStream Stream;

        public VHSConnection(int port, bool oneway)
        {
            OneWay = oneway;
            Port = port;
            Listener = new TcpListener(System.Net.IPAddress.Loopback, Port);
            Task.Run(ListenerThread);
        }

        public void Close()
        {
            if (!OneWay)
            {
                Write(Encoding.ASCII.GetBytes("DC"));
                Reading = false;
            }
            Drop.Set();
        }

        public byte ReadByte()
        {
            if (Stream != null)
                return (byte)Stream.ReadByte();
            throw new System.Exception("stream null");
        }

        public void Write(byte[] Data)
        {
            if (Stream != null)
                Stream.Write(Data);
        }

        private void ListenerThread()
        {
            while (true)
            {
                if (Client != null)
                {
                    Listener.Stop();
                    Drop.WaitOne();
                    if (Stream != null)
                    {
                        Stream.Close();
                        Stream.Dispose();
                        Stream = null;
                    }
                    if (Client != null)
                    {
                        Client.Close();
                        Client.Dispose();
                        Client = null;
                    }
                }
                Listener.Start();
                Client = Listener.AcceptTcpClient();
                Stream = Client.GetStream();
                if (!OneWay)
                {
                    Task.Run(() =>
                    {
                        Connected?.Invoke(this, new EventArgs());
                    });
                    Task.Run(ReaderThread);
                }
            }
        }

        private void ReaderThread()
        {
            Reading = true;
            while (Reading)
            {
                byte character = (byte)Stream.ReadByte();
                if (character == ':')
                {
                    byte[] Value = new byte[4];
                    Value[0] = (byte)Stream.ReadByte();
                    Value[1] = (byte)Stream.ReadByte();
                    Value[2] = (byte)Stream.ReadByte();
                    Value[3] = (byte)Stream.ReadByte();
                    int NumBytes = BitConverter.ToInt32(Value);
                    byte[] data = new byte[4 + NumBytes];
                    data[0] = Value[0];
                    data[1] = Value[1];
                    data[2] = Value[2];
                    data[3] = Value[3];
                    for (int x = 4; x < NumBytes + 4; x++)
                        data[x] = (byte)Stream.ReadByte();
                    DataReceived?.Invoke(this, new DataReceivedEventArgs(data));
                }
            }
        }
    }
}