using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Video
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }


        // The main object for interacting with the audio/video devices.
        public CaptureSource captureSource;

        // Brush for the video feed.
        public VideoBrush webcamBrush;

        // Brush for the captured video frame.
        public ImageBrush capturedImage;

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize the CaptureSource.
            InitWebcam();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // verify the VideoCaptureDevice is not null
            if (captureSource.VideoCaptureDevice != null)
            {
                captureSource.Stop();
            }
        }
        public void InitWebcam()
        {
            // Create the CaptureSource.
            this.captureSource = new CaptureSource();

            // async capture failed event handler
            captureSource.CaptureFailed +=
                new EventHandler<ExceptionRoutedEventArgs>
               (CaptureSource_CaptureFailed);

            // async capture completed event handler
            captureSource.CaptureImageCompleted +=
                new EventHandler<CaptureImageCompletedEventArgs>
               (CaptureSource_CaptureImageCompleted);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (captureSource.State != CaptureState.Started)
            {
                // Set the video capture source the WebCam.
                captureSource.VideoCaptureDevice =
                    CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();

                // set the source on the VideoBrush used to display the video
                webcamBrush = new VideoBrush();
                webcamBrush.SetSource(captureSource);

                // Set the Fill property of the Rectangle to the VideoBrush.
                webcamDisplay.Fill = webcamBrush;

                // the brush used to fill the display rectangle
                capturedImage = new ImageBrush();

                // set the Fill property of the Rectangle to the ImageBrush
                capturedDisplay.Fill = capturedImage;

                // Request access to device and verify the VideoCaptureDevice is not null.
                if (CaptureDeviceConfiguration.RequestDeviceAccess() &&
                    captureSource.VideoCaptureDevice != null)
                {
                    try
                    {
                        captureSource.Start();
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Notify user that the webcam could not be started.
                        MessageBox.Show("There was a problem starting the webcam " +
                                        "If using a Mac, verify default device settings." +
                                        "Right click app to access the Configuration settings.");
                    }
                }
                else
                {
                    MessageBox.Show("Could not start Webcam. Verify device is connected " +
                                    "and privacy permission allow access to device.");
                }
            }
        }

        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            // verify the VideoCaptureDevice is not null and the device is started
            if (captureSource.VideoCaptureDevice != null
                 && captureSource.State == CaptureState.Started)
            {
                captureSource.CaptureImageAsync();
            }
        }

        private void CaptureSource_CaptureImageCompleted
            (object sender, CaptureImageCompletedEventArgs e)
        {
            // Set the ImageBrush to the WriteableBitmap 
            capturedImage.ImageSource = e.Result;
        }

        private void CaptureSource_CaptureFailed(
                        object sender,
                        ExceptionRoutedEventArgs e)
        {
            // Handle capture failure.
        }
    }
}
