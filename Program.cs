using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VHSRelay.CommunicationAbstractions;
using VHSRelay.CommunicationAbstractions.AppShell.DataClasses.InterfaceImplimentations;
using VHSRelay.Interfaces;

namespace VHSRelay
{
    internal class Program
    {
        private static Tuple<bool, bool> CameraState = new Tuple<bool, bool>(false, false);
        private static VHSConnection HIDConnection;
        private static IHid HIDDevice;
        private static int HIDPort = 13002;
        private static Tuple<bool, bool, string> HIDState = new Tuple<bool, bool, string>(false, false, "");
        private static VHSConnection ManagementConnection;
        private static int ManagementPort = 13000;
        private static VHSConnection SerialConnection;
        private static ISerial SerialDevice;
        private static int SerialPort = 13001;
        private static Tuple<bool, bool, string> SerialState = new Tuple<bool, bool, string>(false, false, "");
        private static bool TimeoutStillGoing = false;
        private static Stopwatch TimeoutWatch = new Stopwatch();
        private static VHSConnection VideoConnection;
        private static IVideo VideoDevice;
        private static int VideoPort = 13003;

        public static bool CheckForCamera()
        {
            bool CameraCon = false;
            try
            {
                string returnedString = "ls /sys/class/video4linux".Bash();
                try
                {
                    int VideoNumber = Convert.ToInt32(returnedString[(returnedString.IndexOf('o') + 1)..].Replace("\n", ""));
                    CameraCon = true;
                }
                catch
                {
                }
            }
            catch
            {
            }
            return CameraCon;
        }

        private static string CheckForHID()
        {
            HIDSharpIHID scanner = new HIDSharpIHID();
            //return scanner.ScanFirst();
            return scanner.Scan("pid_f5a0");
        }

        private static string CheckForPorts()
        {
            List<string> ports = HIDSharpISerial.Scan();
            if (ports.Count > 0)
                return ports[0];
            else
                return "";
        }

        private static void FrameRecieved(object sender, FrameReadyEventArgs e)
        {
            VideoConnection.Write(e.Image.Bytes);
        }

        private static void HIDDataReceived(byte[] Data)
        {
            HIDConnection.Write(Data);
        }

        private static void Main(string[] args)
        {
            HIDConnection = new VHSConnection(HIDPort, true);
            SerialConnection = new VHSConnection(SerialPort, true);
            VideoConnection = new VHSConnection(VideoPort, true);
            ManagementConnection = new VHSConnection(ManagementPort, false);
            ManagementConnection.Connected += OnNewConnection;
            while (true)
            {
                CameraState = new Tuple<bool, bool>(CameraState.Item2, CheckForCamera());
                if (CameraState.Item2 && !CameraState.Item1)
                    OnCameraDetected();
                if (CameraState.Item1 && !CameraState.Item2)
                    OnCameraDisconnected();
                string SerialPort = CheckForPorts();
                SerialState = new Tuple<bool, bool, string>(SerialState.Item2, !string.IsNullOrWhiteSpace(SerialPort), SerialPort);
                if (SerialState.Item2 && !SerialState.Item1)
                    OnSerialDetected(SerialPort);
                if (SerialState.Item1 && !SerialState.Item2)
                    OnSerialDisconnected();
                string HID = CheckForHID();
                HIDState = new Tuple<bool, bool, string>(HIDState.Item2, !string.IsNullOrWhiteSpace(HID), HID);
                if (HIDState.Item2 && !HIDState.Item1)
                    OnHIDDetected(HID);
                if (SerialState.Item1 && !SerialState.Item2)
                    OnHIDDisconnected();
            }
        }

        private static void ManagementDataReceived(object sender, Events.DataReceivedEventArgs e)
        {
            char command = (char)e.DataRecieved[4];
            int NumBytes = BitConverter.ToInt32(e.DataRecieved);
            byte[] Data = new byte[NumBytes - 5];
            for (int x = 0; x < NumBytes - 5; x++)
                Data[x] = e.DataRecieved[x - 5];
            switch (command)
            {
                case 's':
                    SerialConnection.Write(Data);
                    break;

                case 'h':
                    HIDDevice.Write(Data, (uint)NumBytes - 5);
                    break;

                case 'v':
                    break;

                case 'm':
                    switch ((char)Data[0])
                    {
                        case 'W':
                            TimeoutWatch.Restart();
                            break;

                        case 'I':
                            // send the info on what is currently connected to the requesting device.
                            break;
                    }
                    break;
            }
        }

        private static void OnCameraDetected()
        {
            if (VideoDevice != null)
            {
                VideoDevice.StopStream();
                VideoDevice.Dispose();
                VideoDevice = null;
            }
            VideoDevice = new EmguIVideo();
            VideoDevice.StartStream();
            VideoDevice.FrameReady += FrameRecieved;
        }

        private static void OnCameraDisconnected()
        {
        }

        private static void OnHIDDetected(string hID)
        {
            if (HIDDevice != null)
            {
                HIDDevice.Dispose();
                HIDDevice = null;
            }
            HIDDevice = new HIDSharpIHID();
            HIDDevice.Load(hID);
            Task.Run(() =>
            {
                while (HIDState.Item2)
                    HIDDataReceived(HIDDevice.Read());
            });
        }

        private static void OnHIDDisconnected()
        {
        }

        private static void OnNewConnection(object sender, EventArgs e)
        {
            if (!TimeoutWatch.IsRunning)
                TimeoutWatch.Start();
            else
                TimeoutWatch.Restart();
            if (!TimeoutStillGoing)
                Task.Run(() =>
                {
                    TimeoutStillGoing = true;
                    while (TimeoutWatch.ElapsedMilliseconds < 5000)
                        Thread.Sleep(200);
                    VideoConnection.Close();
                    SerialConnection.Close();
                    HIDConnection.Close();
                    ManagementConnection.Close();
                    TimeoutStillGoing = false;
                });
        }

        private static void OnSerialDetected(string serialPort)
        {
            if (SerialDevice != null)
            {
                SerialDevice.Close();
                SerialDevice.Dispose();
                SerialDevice = null;
            }
            SerialDevice = new HIDSharpISerial(serialPort, true);
            SerialDevice.Open();
            SerialDevice.DataReceived += SerialDataRecieved;
        }

        private static void OnSerialDisconnected()
        {
        }

        private static void SerialDataRecieved(object sender, EventArgs e)
        {
            SerialConnection.Write(Encoding.ASCII.GetBytes(SerialDevice.ReadExisting()));
        }
    }
}