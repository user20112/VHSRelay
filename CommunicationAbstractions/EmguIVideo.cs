using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Threading;
using System.Threading.Tasks;
using VHSRelay.Interfaces;

namespace VHSRelay.CommunicationAbstractions
{
    public class EmguIVideo : IVideo
    {
        private VideoCapture _capture;
        private bool _captureInProgress;
        private bool KeepGoing = true;

        public EmguIVideo()
        {
        }

        ~EmguIVideo()
        {
            KeepGoing = false;
        }

        public void CaptureFrameStart()
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {
                    _captureInProgress = false; //Flag the state of the camera
                }
                else
                {
                    RetrieveCaptureInformation(); //Get Camera information
                    _capture.SetCaptureProperty(CapProp.Monochrome, 1);
                    Task.Run(() =>
                    {
                        while (KeepGoing)
                            if (!Paused)
                            {
                                Thread.Sleep(100);
                                RetrieveImage();
                                FrameReady?.Invoke(this, new FrameReadyEventArgs(CurrentImage));
                            }
                    });
                    _captureInProgress = true; //Flag the state of the camera
                }
            }
            else
            {
                SetupCapture();
                CaptureFrameStart(); //recursivly backup
            }
        }

        public override void Dispose()
        {
            try
            {
                StopStream();
            }
            catch
            {
            }

            KeepGoing = false;
            GC.SuppressFinalize(this);
        }

        public override Image<Gray, byte> GetHDRImage()
        {
            throw new NotImplementedException();
        }

        public override void PauseStream(bool Toggle)
        {
            Paused = Toggle;
        }

        public void RetrieveImage()
        {
            try
            {
                CurrentImage = _capture.QueryFrame().ToImage<Gray, byte>();
            }
            catch
            {
            }
            if (CurrentImage == null)
                CurrentImage = new Image<Gray, byte>(640, 480);
        }

        public override void SetExposure(int SetExposure)
        {
            _capture.SetCaptureProperty(CapProp.Exposure, SetExposure);
        }

        public override void StartStream()
        {
            CaptureFrameStart();
            KeepGoing = true;
            Paused = false;
        }

        public override void StopStream()
        {
            _capture.Dispose();
            KeepGoing = false;
        }

#pragma warning disable CA1822 // Mark members as static

        private void RetrieveCaptureInformation()
        {
            //string CameraName = WebCams[Videology].Device_Name;
            //string Brightness = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Brightness).ToString();
            //string Contrast = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Contrast).ToString();
            //string Sharpness = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Sharpness).ToString();
            //string RGB = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.ConvertRgb).ToString();
            //string Exposure = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Exposure).ToString();
            //string FrameHeight = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight).ToString();
            //string FrameWidth = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth).ToString();
            //string Gain = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Gain).ToString();
            //string Gamma = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Gamma).ToString();
            //string Hue = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Hue).ToString();
            //string Saturation = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Saturation).ToString();
            //string Trigger = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Trigger).ToString();
            //string TriggerDelay = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.TriggerDelay).ToString();
            //string WhiteBalanceBlue = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.WhiteBalanceBlueU).ToString();
            //string WhiteBalanceR = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.WhiteBalanceRedV).ToString();
            //string MaxDc1394 = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.MaxDC1394).ToString();
            //string CurrentCaptureMode = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Mode).ToString();
            //string MonoCrome = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Monochrome).ToString();
            //string Rectification = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Rectification).ToString();
            //string Preview = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PreviewFormat).ToString();
            //string Format = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Format).ToString();
        }

#pragma warning restore CA1822 // Mark members as static

        private void SetupCapture()
        {
            if (_capture != null)
                _capture.Dispose();
            try
            {
                _capture = new VideoCapture();
            }
            catch
            {
            }
        }
    }
}