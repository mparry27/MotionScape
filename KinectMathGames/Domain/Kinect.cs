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
        private float vel = 0;
        private float prevVel = 0;
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
        public float velocity
        {
            get
            {
                return vel;
            }
            set
            {
                vel = value;
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
                    vel = (zpos - prevZPos) * (1000 / (timeStamp - prevTimeStamp)) * -1;
                    if ((vel - prevVel) > velGate)
                        vel = prevVel + velGate;
                    if ((vel - prevVel) < (-1 * velGate))
                        vel = prevVel + (-1 * velGate);

                    prevZPos = zpos;
                    zPosition = zpos;
                    prevVel = vel;
                    velocity = vel;
                    prevTimeStamp = timeStamp;
                }
            }
        }
    }
}

    
}
