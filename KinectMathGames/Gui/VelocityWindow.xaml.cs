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

namespace KinectMathGames
{
    /// <summary>
    /// Interaction logic for velocity.xaml
    /// </summary>
    public partial class Velocity : Window
    {
        //declare all variables
        DispatcherTimer gameTimer = new DispatcherTimer();
        double score;
        bool gameOver;

        Rect VBox; //store position of the velocity from GUI
        Rect GateBox;

        public Velocity()
        {
            InitializeComponent();

            gameTimer.Tick += MainEvenTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            StartGame();
        }

        private void MainEvenTimer(object sender, EventArgs e)
        {
            txtscore.Content = "Score: " + score;

            VBox = new Rect(Canvas.GetLeft(sldrVelocity), Canvas.GetTop(sldrVelocity), sldrVelocity.Width, sldrVelocity.Height);

            Canvas.SetTop(sldrVelocity, Canvas.GetTop(sldrVelocity));

            if (score > 16)
            {
                EndGame();
            }

            foreach (var x in MyCanVas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3")
                {

                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 1); // make gates move slow in the beginning

                    if (Canvas.GetLeft(x) < -100)
                    {
                        Canvas.SetLeft(x, 600);

                        score += 0.5;
                    }


                    if (score > 7)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - 2.5); // make gates move faster as the user has higher score more than 7

                        if (Canvas.GetLeft(x) < -100)
                        {
                            Canvas.SetLeft(x, 600);

                            score += 0.5;
                        }

                    }

                    if (score > 11)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - 4); // make gates move faster as the user has higher score more than 11

                        if (Canvas.GetLeft(x) < -100)
                        {
                            Canvas.SetLeft(x, 600);

                            score += 0.5;
                        }

                    }

                    //GateBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    //if (VBox.IntersectsWith(GateBox)) // if the Vbox hits gatebox logic
                    //{
                    //    score += 0;
                    //}

                    if (score > 16)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - 7); // make gates move faster as the user has higher score more than 11

                        if (Canvas.GetLeft(x) < -100)
                        {
                            Canvas.SetLeft(x, 500);

                            score += 0.5;
                        }

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
            Canvas.SetTop(sldrVelocity, 78);

            foreach (var x in MyCanVas.Children.OfType<Image>()) // all images move to the left
            {
                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 220);
                }

                if ((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 460);
                }


                if ((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 700);
                }

            }

            gameTimer.Start();

        }

        private void EndGame()
        {
            gameTimer.Stop();
            gameOver = true;
            txtscore.Content += "Conglatulation !!! \nYou're WIN !!!";
        }
    }
}
