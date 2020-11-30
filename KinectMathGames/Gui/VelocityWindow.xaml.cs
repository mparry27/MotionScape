using System;
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
using KinectMathGames.Gui;

namespace KinectMathGames
{
    /// <summary>
    /// Interaction logic for velocity.xaml
    /// </summary>
    public partial class VelocityWindow : Window
    {
        //declare all variables
        DispatcherTimer gameTimer = new DispatcherTimer();
        int score = 0;
        double scale = 500;
        double speed = 15;
        int rounds = 20;
        Kinect sensor = new Kinect();
        Random rand = new Random(DateTime.Now.Millisecond);
        Polygon topTriangle;
        Polygon bottomTriangle;
        SolidColorBrush yellowFill = new SolidColorBrush(Color.FromRgb(255, 255, 0));
        SolidColorBrush redFill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        SolidColorBrush greenFill = new SolidColorBrush(Color.FromRgb(0, 255, 0));

        public VelocityWindow()
        {
            InitializeComponent();
            gameTimer.Tick += MainEvenTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }

        private void MainEvenTimer(object sender, EventArgs e)
        {
            Canvas.SetTop(rec1, 470 + (sensor.ZVelocity * -scale)); // get the velocity for user
            playerHitLine.X1 = Canvas.GetLeft(rec1);
            playerHitLine.Y1 = Canvas.GetTop(rec1) + 30;
            playerHitLine.X2 = Canvas.GetLeft(rec1) + 60;
            playerHitLine.Y2 = Canvas.GetTop(rec1) + 30;

            if (PauseButton.Content.ToString() == "Pause")
            {
                if (score >= rounds)
                {
                    EndGame();
                    return;
                }

                foreach (var gate in MyCanvas.Children.OfType<Line>())
                {
                    switch (gate.Tag)
                    {
                        case "obs1":
                            topTriangle = obs1Top;
                            bottomTriangle = obs1Bottom;

                            gate.X1 -= speed;
                            gate.X2 -= speed;

                            Canvas.SetLeft(obs1Top, gate.X1 - 25);
                            Canvas.SetTop(obs1Top, gate.Y1 - 30);
                            Canvas.SetLeft(obs1Bottom, gate.X2 - 25);
                            Canvas.SetTop(obs1Bottom, gate.Y1 + 130);
                            Canvas.SetLeft(obs1HitLine, gate.X1);
                            Canvas.SetTop(obs1HitLine, gate.Y1);

                            break;
                        case "obs2":
                            topTriangle = obs2Top;
                            bottomTriangle = obs2Bottom;

                            gate.X1 -= speed;
                            gate.X2 -= speed;

                            Canvas.SetLeft(obs2Top, gate.X1 - 25);
                            Canvas.SetTop(obs2Top, gate.Y1 - 30);
                            Canvas.SetLeft(obs2Bottom, gate.X1 - 25);
                            Canvas.SetTop(obs2Bottom, gate.Y1 + 130);
                            Canvas.SetLeft(obs2HitLine, gate.X1);
                            Canvas.SetTop(obs2HitLine, gate.Y1);

                            break;
                        case "obs3":
                            topTriangle = obs3Top;
                            bottomTriangle = obs3Bottom;

                            gate.X1 -= speed;
                            gate.X2 -= speed;

                            Canvas.SetLeft(obs3Top, gate.X1 - 25);
                            Canvas.SetTop(obs3Top, gate.Y1 - 30);
                            Canvas.SetLeft(obs3Bottom, gate.X1 - 25);
                            Canvas.SetTop(obs3Bottom, gate.Y1 + 130);
                            Canvas.SetLeft(obs3HitLine, gate.X1);
                            Canvas.SetTop(obs3HitLine, gate.Y1);

                            break;
                    }

                    if (gate.X1 <= -50)
                    {
                        gate.X1 = 1500;
                        gate.X2 = 1500;
                        gate.Y1 = rand.Next(0, 800);
                        gate.Y2 = gate.Y1 + 150;
                        topTriangle.Fill = yellowFill;
                        bottomTriangle.Fill = yellowFill;
                    }
                    if (gate.X1 >= 500 - speed && gate.X1 <= 500 + speed)
                    {

                        if (IsIntersecting(playerHitLine, gate)) // if the Vbox hits gatebox logic then increment score
                        {
                            if (topTriangle.Fill == yellowFill)
                            {
                                score++;
                                txtscore.Text = "Score: " + score;
                                topTriangle.Fill = greenFill;
                                bottomTriangle.Fill = greenFill;
                            }
                        }
                        else
                        {
                            topTriangle.Fill = redFill;
                            bottomTriangle.Fill = redFill;
                        }
                    }
                }
            }
        }

        private bool IsIntersecting(Line line1, Line line2)
        {
            Point a = new Point(line1.X1, line1.Y1);
            Point b = new Point(line1.X2, line1.Y2);
            Point c = new Point(line2.X1, line2.Y1);
            Point d = new Point(line2.X2, line2.Y2);
            float denominator = (float)(((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X)));
            float numerator1 = (float)(((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y)));
            float numerator2 = (float)(((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y)));

            // Detect coincident lines (has a problem, read below)
            if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }

        private void StartGame()
        {
            MyCanvas.Focus();
            score = 0;
            Canvas.SetTop(rec1, 470);
            obs1HitLine.X1 = 1500;
            obs1HitLine.X2 = 1500;
            obs1HitLine.Y1 = rand.Next(0, 800);
            obs1HitLine.Y2 = obs1HitLine.Y1 + 150;
            obs2HitLine.X1 = 2000;
            obs2HitLine.X2 = 2000;
            obs2HitLine.Y1 = rand.Next(0, 800);
            obs2HitLine.Y2 = obs2HitLine.Y1 + 150;
            obs3HitLine.X1 = 2500;
            obs3HitLine.X2 = 2500;
            obs3HitLine.Y1 = rand.Next(0, 800);
            obs3HitLine.Y2 = obs3HitLine.Y1 + 150;
            gameTimer.Start();

        }

        private void EndGame()
        {
            gameTimer.Stop();
            instructions.Visibility = Visibility.Hidden;
            txtscore.Text = "Total Score: " +  score;
            WinText.Text = "Congratulations ! \nYou WIN ! \nPress reset to play again.";
        }

        private void StartResetButton_Click(object sender, RoutedEventArgs e)
        {
            if(StartResetButton.Content.ToString() == "Start")
            {
                PauseButton.Content = "Pause";
                StartResetButton.Content = "Reset";
                StartGame();
            }
            else
            {
                txtscore.Text = "Score: 0";
                instructions.Visibility = Visibility.Visible;
                StartResetButton.Content = "Start";
                WinText.Text = String.Empty;
                PauseButton.Content = "Settings";

                Canvas.SetLeft(obs1Top, 1500);
                Canvas.SetLeft(obs1Bottom, 1500);
                Canvas.SetLeft(obs2Top, 1500);
                Canvas.SetLeft(obs2Bottom, 1500);
                Canvas.SetLeft(obs3Top, 1500);
                Canvas.SetLeft(obs3Bottom, 1500);

                obs1Top.Fill = yellowFill;
                obs1Bottom.Fill = yellowFill;
                obs2Top.Fill = yellowFill;
                obs2Bottom.Fill = yellowFill;
                obs3Top.Fill = yellowFill;
                obs3Bottom.Fill = yellowFill;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (PauseButton.Content.ToString() == "Settings")
            {
                var dlg = new VelocitySettingsDialog { Owner = this };
                dlg.ShowDialog();
                if (dlg.DialogResult == true)
                {
                    speed = (int)dlg.sldrSpeed.Value;
                    rounds = int.Parse(dlg.txtRounds.Text);
                }
            }
            else if (PauseButton.Content.ToString() == "Pause")
            {
                PauseButton.Content = "Resume";
            }
            else if (PauseButton.Content.ToString() == "Resume")
            {
                PauseButton.Content = "Pause";
            }
        }
    }
}
