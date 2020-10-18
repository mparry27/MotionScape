using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Kinect;
using KinectMathGames.Domain;

namespace KinectMathGames
{
    /// <summary>
    /// Interaction logic for velocity.xaml
    /// </summary>
    public partial class VelocityWindow : Window
    {
        //declare all variables
        DispatcherTimer gameTimer = new DispatcherTimer();
        double score;
        bool gameOver;
        double scale = 200;
        Kinect sensor = new Kinect();

        Rect VBox; //store position of the velocity from GUI
        Rect GateBox;

        public VelocityWindow()
        {
            InitializeComponent();

            gameTimer.Tick += MainEvenTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            StartGame();
        }

        private void MainEvenTimer(object sender, EventArgs e)
        {
            Canvas.SetTop(cursor, -200 + (sensor.zPosition * scale)); // get the velocity for user

            txtscore.Content = "Score: " + score;

            VBox = new Rect(Canvas.GetLeft(cursor), Canvas.GetTop(cursor), cursor.Width, cursor.Height);

            Canvas.SetTop(cursor, Canvas.GetTop(cursor));

            if (score > 16)
            {
                EndGame();
            }

            foreach (var x in MyCanVas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3")

                GateBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (VBox.IntersectsWith(GateBox)) // if the Vbox hits gatebox logic
                {
                    score += 0;
                }

                {

                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 4); // make gates move slow in the beginning

                    if (Canvas.GetLeft(x) < -100)
                    {
                        Canvas.SetLeft(x, 4050);

                        score += 1;
                    }

                }
                
            }
        }

        private void StartGame()
        {

            MyCanVas.Focus();

            //int temp = 300;

            score = 0;

            gameOver = false;
            Canvas.SetTop(cursor, 78);

            Random rand = new Random(); // generate random number of vertical position of gate
            int y1 = rand.Next(130, 330);
            int y2 = rand.Next(130, 330);
            int y3 = rand.Next(130, 330);
            int y4 = rand.Next(130, 330);
            int y5 = rand.Next(130, 330);
            int y6 = rand.Next(130, 330);
            int y7 = rand.Next(130, 330);
            int y8 = rand.Next(130, 330);
            int y9 = rand.Next(130, 330);
            int y10 = rand.Next(130, 330);
            int y11= rand.Next(130, 330);
            int y12 = rand.Next(130, 330);
            int y13 = rand.Next(130, 330);
            int y14 = rand.Next(130, 330);
            int y15 = rand.Next(130, 330);
            int y16 = rand.Next(130, 330);
            int y17 = rand.Next(130, 330);
            int y18 = rand.Next(130, 330);
            int y19 = rand.Next(130, 330);

            foreach (var x in MyCanVas.Children.OfType<Image>()) // all images move to the left
            {

                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 450);
                    Canvas.SetTop(x, y1);

                }


                if ((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 650);
                    Canvas.SetTop(x, y2);

                }

                if ((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 850);
                    Canvas.SetTop(x, y3);

                }

                if ((string)x.Tag == "obs4")
                {
                    Canvas.SetLeft(x, 1050);
                    Canvas.SetTop(x, y4);
                }


                if ((string)x.Tag == "obs5")
                {
                    Canvas.SetLeft(x, 1250);
                    Canvas.SetTop(x, y5);

                }

                if ((string)x.Tag == "obs6")
                {
                    Canvas.SetLeft(x, 1450);
                    Canvas.SetTop(x, y6);

                }

                if ((string)x.Tag == "obs7")
                {
                    Canvas.SetLeft(x, 1650);
                    Canvas.SetTop(x, y7);

                }

                if ((string)x.Tag == "obs8")
                {
                    Canvas.SetLeft(x, 1850);
                    Canvas.SetTop(x, y8);
                }


                if ((string)x.Tag == "obs9")
                {
                    Canvas.SetLeft(x, 2050);
                    Canvas.SetTop(x, y9);

                }

                if ((string)x.Tag == "obs10")
                {
                    Canvas.SetLeft(x, 2250);
                    Canvas.SetTop(x, y10);

                }

                if ((string)x.Tag == "obs11")
                {
                    Canvas.SetLeft(x, 2450);
                    Canvas.SetTop(x, y11);
                }


                if ((string)x.Tag == "obs12")
                {
                    Canvas.SetLeft(x, 2650);
                    Canvas.SetTop(x, y12);

                }
                if ((string)x.Tag == "obs13")
                {
                    Canvas.SetLeft(x, 2850);
                    Canvas.SetTop(x, y13);

                }

                if ((string)x.Tag == "obs14")
                {
                    Canvas.SetLeft(x, 3050);
                    Canvas.SetTop(x, y14);
                }


                if ((string)x.Tag == "obs15")
                {
                    Canvas.SetLeft(x, 3250);
                    Canvas.SetTop(x, y15);

                }
                if ((string)x.Tag == "obs16")
                {
                    Canvas.SetLeft(x, 3450);
                    Canvas.SetTop(x, y16);

                }

                if ((string)x.Tag == "obs17")
                {
                    Canvas.SetLeft(x, 3650);
                    Canvas.SetTop(x, y17);
                }


                if ((string)x.Tag == "obs18")
                {
                    Canvas.SetLeft(x, 3850);
                    Canvas.SetTop(x, y18);

                }

                if ((string)x.Tag == "obs19")
                {
                    Canvas.SetLeft(x, 4050);
                    Canvas.SetTop(x, y19);

                }

            }

            gameTimer.Start();

        }

        private void EndGame()
        {
            gameTimer.Stop();
            gameOver = true;

            txtscore.Content += "Conglatulations !!! \nYou WIN !!!";
        }
    }
}
