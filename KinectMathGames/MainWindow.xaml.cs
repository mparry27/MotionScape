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
        private float prevTimeStamp = 0;
        private float prevZPos = 0;
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
            float timeStamp = prevTimeStamp;

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
                foreach (Skeleton skel in skeletons)
                {
                    if (skel.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        var zpos = skel.Joints[JointType.HipCenter].Position.Z;
                        lblDepth.Content = zpos.ToString( "#.##" ) + " m";
                        var vel = (1000/(timeStamp - prevTimeStamp))*(zpos - prevZPos)*-1;
                        lblVelocity.Content = vel.ToString( "#.##" ) + " m/s";
                        try { sldrVelocity.Value = vel; }catch(Exception ex) { }
                        try { sldrPosition.Value = zpos; } catch (Exception ex) { }
                        prevZPos = zpos;
                        prevTimeStamp = timeStamp;

                    }
                }
            }
        }
    }
}
