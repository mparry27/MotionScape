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
        private Timer timer1;
        private int counter = 5;
        private static double MAX = 11.00;
        private static double MIN = 4.00;
        private static double RANGEBUFFER = 1;
        private double upperRange;
        private double lowerRange;
        private int score = 0;
        private double skeletonCoord;
        

        public PositionLogic()
        {
            
        }
        
        public void PositionGame()
        {
            bool alive = true;
            Random r = new Random();
            double randomPoint;

            while (alive == true)
            {
                randomPoint = r.NextDouble() * ((MAX - RANGEBUFFER) - (MIN + RANGEBUFFER)) + MIN - RANGEBUFFER;
                upperRange = randomPoint + 1;
                lowerRange = randomPoint - 1;
                TimeStart();
                counter = 5;

                if(skeletonCoord > lowerRange && skeletonCoord < upperRange)
                {
                    score++;
                }
                else
                {
                    alive = false;
                }
            }
        }

        private void TimeStart()
        {
            timer1 = new System.Timers.Timer(1000);
            timer1.Elapsed += timer_Ticker;
            timer1.AutoReset = true;
            timer1.Enabled = true;
        }

        private void timer_Ticker(object sender, ElapsedEventArgs e)
        {
            counter--;
            if (counter == 0)
            {
                timer1.Stop();
            }
        }
    }
}
