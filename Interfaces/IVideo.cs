using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace VHSRelay.Interfaces
{
    public abstract class IVideo : IDisposable
    {
        public bool ButtonAvailable; //  if true the video source supports MFB button
        public Image<Gray, byte> CurrentImage; //  stores the current image
        public int FrameCounter; //  increments each time a frame is recieved
        public EventHandler<FrameReadyEventArgs> FrameReady; // called whenever a frame is recieved
        public EventHandler<EventArgs> OnMFB; //  called when MFB button is pressed
        public bool Paused; //  true when the video stream is paused
        public int TimeElapsedSinceLastFrame; //  contains the time since last frame in ms

        /// <summary>
        ///     call to dispose elements that need to be disposed
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        ///     gets a processed image for Analysis ( not used currently)
        /// </summary>
        /// <returns></returns>
        public abstract Image<Gray, byte> GetHDRImage();

        /// <summary>
        ///     Call to pause and resume the stream. pass in true to pause or false to resume
        /// </summary>
        /// <param name="Toggle"></param>
        public abstract void PauseStream(bool Toggle);

        /// <summary>
        ///     Call to set exposure value should be from 0 to 100
        /// </summary>
        /// <param name="SetExposure"></param>
        public abstract void SetExposure(int SetExposure);

        /// <summary>
        ///     Call to begin streaming from the device
        /// </summary>
        public abstract void StartStream();

        /// <summary>
        ///     call to stop streaming from the device. consider using pause if you want to resume it faster.
        /// </summary>
        public abstract void StopStream();
    }
}