using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace KinectMathGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor sensor;
        private long prevTimeStamp = 0;
        private float prevZPos = 0;
        private float avgSum = 0;
        private int avgCount = 0;
        private int avgAcuracy = 5;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (KinectSensor.KinectSensors.Count > 0)
            {
                sensor = KinectSensor.KinectSensors[0];
                sensor.AllFramesReady += this.SensorAllFramesReady;
            }
            sensor.Start();
            sensor.SkeletonStream.Enable();
        }

        private void SensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];
            long timeStamp = 0;

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                    timeStamp = skeletonFrame.Timestamp;
                }
            }

            if (skeletons.Length != 0)
            {
                foreach(Skeleton skel in skeletons) {
                    if (skel.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        var zpos = skel.Joints[JointType.Spine].Position.Z;
                        var vel = (zpos - prevZPos) * (1000 /(timeStamp - prevTimeStamp))*-1;
                        avgSum += vel;
                        avgCount++;
                        if(avgCount >= avgAcuracy)
                        {
                            lblDepth.Content = zpos.ToString("0.00") + " m";
                            lblVelocity.Content = vel.ToString("0.00") + " m/s";
                            sldrVelocity.Value = avgSum/avgCount;
                            sldrPosition.Value = zpos;
                            avgSum = 0;
                            avgCount = 0;
                        }
                        prevZPos = zpos;
                        prevTimeStamp = timeStamp;
                    }
                }
            }
        }
    }
}
