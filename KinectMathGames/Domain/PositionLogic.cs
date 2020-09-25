using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Kinect;

namespace KinectMathGames
{
    class PositionLogic
    {
        static double MAX = 10;
        static double MIN = 0;
        static double RANGE = 1;
        private double upperRange;
        private double lowerRange;

        public PositionLogic()
        {
            
        }
        
        public void PositionGame()
        {
            Random r = new Random();
            double randomPoint = r.NextDouble() * ((MAX - RANGE) - (MIN + RANGE)) + MIN - RANGE;
            upperRange = randomPoint + 1;
            lowerRange = randomPoint - 1;

            /*
             * 
             *  Thread.Sleep(5000);
             *  if(skeleton is in range){
             *      GOOD!
             *      add points if that's what we want
             *      PositionGame();
             *  }else{
             *      BAD!
             *      lose game
             *  }
             *      
             */
        }

    }
}
