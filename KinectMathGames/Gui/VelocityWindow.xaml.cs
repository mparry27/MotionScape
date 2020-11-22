﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        private PositionLogic pLogic = new PositionLogic();
        double score;
        bool gameOver;
        private double retDouble;

        double scale = 130;
        Kinect sensor = new Kinect();

        Rect VBox; //store position of the velocity from GUI
        Rect GateBox;


        public VelocityWindow()
        {
            InitializeComponent();

            gameTimer.Tick += MainEvenTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            StartGame();
        }



        private void MainEvenTimer(object sender, EventArgs e)
        {
            Canvas.SetTop(rec1, 100 + (sensor.zVelocity * -scale)); // get the velocity for user
            
            VBox = new Rect(Canvas.GetLeft(rec1), Canvas.GetTop(rec1), 1, rec1.Height); // create rectangle for player width = 1 and height = rec1.height

            Random rand = new Random(); // generate random number of vertical position of gate
            int y1 = rand.Next(30, 220);

            Canvas.SetTop(rec1, Canvas.GetTop(rec1));

            if (score > 19)
            {
                EndGame();
            }



            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag != "cursor")
                    GateBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x) + 26, 1, 26); // create rectangle for gate width = 3 and height = gate.height
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 3); // make gates move slow in the beginning

                    if (Canvas.GetLeft(x) < 95)
                    {
                        Canvas.SetLeft(x, 900);
                        Canvas.SetTop(x, y1);
                        //score++;
                    }

                    if (VBox.IntersectsWith(GateBox)) // if the Vbox hits gatebox logic then increment score
                    {
                        score ++;
         
                        txtscore.Content = "Score: " + score;
                    }
                
            }
        }

        private void StartGame()
        {

            MyCanvas.Focus();

            score = 0;

            gameOver = false;
            Canvas.SetTop(rec1, 78);
            PauseButton.Content = "Pause";


            foreach (var x in MyCanvas.Children.OfType<Image>()) // all images move to the left
            {

                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 600);

                }

                if ((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 900);

                }

                if ((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 1200);

                }

            }
            gameTimer.Start();

        }
        public double getCursorTop()
        {
            foreach (var x in MyCanvas.Children.OfType<Ellipse>())
            {
                if ((string)x.Tag == "cursor")
                {
                    retDouble = Canvas.GetTop(x);
                    break;
                }
            }
            return retDouble;
        }
        

        private void EndGame()
        {
            gameTimer.Stop();
            gameOver = true;

            //txtscore.Content += "Congratulations !!! \nYou WIN !!!";
            WinText.Content += "Congratulations ! \nYou WIN ! \nPress reset to play again.";


        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
            gameTimer.Start();
            score = 0;
            txtscore.Content = "Score: " + score;
            WinText.Content = String.Empty;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            gameTimer.Stop();
            PauseButton.Content = "Start";

            //String state = (sender as Button).Content.ToString();
            //if (state == "Start") 
            //{
            //    gameTimer.Stop();
            //    PauseButton.Content = "Start";
            //}
            //else
            //{
            //    gameTimer.Start();
            //    PauseButton.Content = "Pause";
            //}


        }
    }
}
