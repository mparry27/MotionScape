using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Microsoft.Kinect;

namespace KinectMathGames
{
    class PositionLogic
    {
        private static double MAX = 243.00;
        private static double MIN = 0.00;
        private double upperRange;
        private double lowerRange;
        private Random r;
        private double randomPoint;



        public PositionLogic()
        {
            
        }

        public bool isInGate(double skeletonCoord, double gateCoord)
        {
            lowerRange = gateCoord;
            upperRange = gateCoord + 20;
            if (skeletonCoord >= lowerRange && skeletonCoord <= upperRange)
            {
                return true;
            }
            return false;
        }

        public double randomYCoord()
        {
            r = new Random();
            randomPoint = r.NextDouble() * (MAX - MIN) + MIN;
            return randomPoint;
        }

        public double getMAX()
        {
            return MAX;
        }
        public double getMIN()
        {
            return MIN;
        }
        
    }
}
