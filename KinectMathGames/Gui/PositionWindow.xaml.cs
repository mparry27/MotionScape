using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using KinectMathGames.Domain;


namespace KinectMathGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PositionWindow : Window
    {
        private Kinect kinect = new Kinect();
        private double scale = 200;
        private int score = 0;
        PositionLogic pLogic;
        Rect gateBox;

        private int counter = 5;
        private static double MAX = 11.00;
        private static double MIN = 4.00;
        private static double RANGEBUFFER = 1;
        private double upperRange;
        private double lowerRange;
        private double skeletonCoord;
        private Timer timer1;

        bool alive = true;
        Random r = new Random();
        double randomPoint;
        public PositionWindow()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Canvas.SetTop(cursor, -200 + (kinect.zPosition * scale));

            //Timer for about 5 seconds (can change later)
            //Once timer hits 0, check game logic
            
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



        public void UpdateScore()
        {
            score += 1;
            txtscore.Content = "Score: " + score;
        }

    }
}
