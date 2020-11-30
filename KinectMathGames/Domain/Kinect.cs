using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Kinect;

namespace KinectMathGames.Domain
{
    class Kinect : INotifyPropertyChanged
    {
        private KinectSensor sensor;
        public bool isReady = false;
        private long prevTimeStamp = 0;
        private float zPos = 0;
        private float zPosPrev = 0;
        private float xPos = 0;
        private float xPosPrev = 0;
        private float zVel = 0;
        private float zVelPrev = 0;
        private float xVel = 0;
        private float xVelPrev = 0;
        private float zVelAvg = 0;
        private float xVelAvg = 0;
        private float velGate = 0.05F;
        private int velAvgCount = 0;
        private int velAvgCap = 2;
        private enum Mode { SYNC, AVERAGE, MIXED }
        private Mode velocityMode = Mode.AVERAGE;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public float ZPosition
        { 
            get
            {
                return zPos;
            } 
            set 
            {
                zPos = value;
                NotifyPropertyChanged("zPosition"); 
            }
        }
        public float XPosition
        {
            get
            {
                return xPos;
            }
            set
            {
                xPos = value;
                NotifyPropertyChanged("xPosition");
            }
        }
        public float ZVelocity
        {
            get
            {
                return zVel;
            }
            set
            {
                zVel = value;
                NotifyPropertyChanged("zVelocity");
            }
        }
        public float XVelocity
        {
            get
            {
                return xVel;
            }
            set
            {
                xVel = value;
                NotifyPropertyChanged("xVelocity");
            }
        }

        public Kinect()
        {
            while(!isReady)
            {
                try
                {
                    if (KinectSensor.KinectSensors.Count > 0)
                    {
                        sensor = KinectSensor.KinectSensors[0];
                        sensor.AllFramesReady += this.SensorAllFramesReady;
                    }
                    sensor.Start();
                    sensor.SkeletonStream.Enable();
                    isReady = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
                foreach (Skeleton skel in skeletons)
                {
                    if (skel.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        zPos = skel.Joints[JointType.Spine].Position.Z;
                        xPos = skel.Joints[JointType.Spine].Position.X;
                        long timeElapsed = (timeStamp - prevTimeStamp);
                        if (timeElapsed <= 0)
                            timeElapsed = 1;
                        if (velocityMode == Mode.SYNC)
                        {
                            zVel = (zPos - zPosPrev) * (1000 / timeElapsed) * -1;
                            if ((zVel - zVelPrev) > velGate)
                                zVel = zVelPrev + velGate;
                            if ((zVel - zVelPrev) < (-1 * velGate))
                                zVel = zVelPrev + (-1 * velGate);
                            xVel = (xPos - xPosPrev) * (1000 / timeElapsed) * -1;
                            if ((xVel - xVelPrev) > velGate)
                                xVel = xVelPrev + velGate;
                            if ((xVel - xVelPrev) < (-1 * velGate))
                                xVel = xVelPrev + (-1 * velGate);
                            zVelPrev = zVel;
                            ZVelocity = zVel;
                            xVelPrev = xVel;
                            XVelocity = xVel;
                            }
                        else if(velocityMode == Mode.AVERAGE)
                        {
                            if(velAvgCount < velAvgCap)
                            {
                                zVelAvg += (zPos - zPosPrev) * (1000 / timeElapsed) * -1;
                                xVelAvg += (xPos - xPosPrev) * (1000 / timeElapsed) * -1;
                                velAvgCount++;
                            }
                            else
                            {
                                zVelPrev = zVelAvg / velAvgCap;
                                ZVelocity = zVelAvg / velAvgCap;
                                xVelPrev = xVelAvg / velAvgCap;
                                XVelocity = xVelAvg / velAvgCap;

                                zVelAvg = 0;
                                xVelAvg = 0;
                                velAvgCount = 0;
                            }
                        } else if(velocityMode == Mode.MIXED)
                        {
                            if (velAvgCount < velAvgCap)
                            {
                                zVelAvg += (zPos - zPosPrev) * (1000 / timeElapsed) * -1;
                                xVelAvg += (xPos - xPosPrev) * (1000 / timeElapsed) * -1;
                                velAvgCount++;
                            }
                            else
                            {
                                zVel = zVelAvg / velAvgCap;
                                xVel = xVelAvg / velAvgCap;

                                if ((zVel - zVelPrev) > velGate)
                                    zVel = zVelPrev + velGate;
                                if ((zVel - zVelPrev) < (-1 * velGate))
                                    zVel = zVelPrev + (-1 * velGate);
                                if ((xVel - xVelPrev) > velGate)
                                    xVel = xVelPrev + velGate;
                                if ((xVel - xVelPrev) < (-1 * velGate))
                                    xVel = xVelPrev + (-1 * velGate);

                                zVelPrev = zVel;
                                ZVelocity = zVel;
                                xVelPrev = xVel;
                                XVelocity = xVel;

                                zVelAvg = 0;
                                xVelAvg = 0;
                                velAvgCount = 0;
                            }
                        }
                        zPosPrev = zPos;
                        ZPosition = zPos;
                        xPosPrev = xPos;
                        XPosition = xPos;
                        prevTimeStamp = timeStamp;
                    }
                }
            }
        }
    }    
}
