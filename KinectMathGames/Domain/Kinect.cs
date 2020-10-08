using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectMathGames.Domain
{
    class Kinect : INotifyPropertyChanged
    {
        private KinectSensor sensor;
        private long prevTimeStamp = 0;
        private float zpos = 0;
        private float prevZPos = 0;
        private float xpos = 0;
        private float prevXPos = 0;
        private float zVel = 0;
        private float prevZVel = 0;
        private float xVel = 0;
        private float prevXVel = 0;
        private float velGate = 0.02F;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public float zPosition
        { 
            get
            {
                return zpos;
            } 
            set 
            {
                zpos = value;
                NotifyPropertyChanged("zPosition"); 
            }
        }
        public float xPosition
        {
            get
            {
                return xpos;
            }
            set
            {
                xpos = value;
                NotifyPropertyChanged("xPosition");
            }
        }
        public float velocity
        {
            get
            {
                return zVel;
            }
            set
            {
                zVel = value;
                NotifyPropertyChanged("velocity");
            }
        }

        public Kinect()
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
            foreach (Skeleton skel in skeletons)
            {
                if (skel.TrackingState == SkeletonTrackingState.Tracked)
                {
                    zpos = skel.Joints[JointType.Spine].Position.Z;
                    xpos = skel.Joints[JointType.Spine].Position.X;
                    long timeElapsed = (timeStamp - prevTimeStamp);
                    if (timeElapsed <= 0)
                        timeElapsed = 1;
                    zVel = (zpos - prevZPos) * (1000 / timeElapsed) * -1;
                    if ((zVel - prevZVel) > velGate)
                        zVel = prevZVel + velGate;
                    if ((zVel - prevZVel) < (-1 * velGate))
                        zVel = prevZVel + (-1 * velGate);
                    prevZPos = zpos;
                    zPosition = zpos;
                    prevXPos = xpos;
                    xPosition = xpos;
                    prevZVel = zVel;
                    velocity = zVel;
                    prevTimeStamp = timeStamp;
                }
            }
        }
    }
}

    
}
