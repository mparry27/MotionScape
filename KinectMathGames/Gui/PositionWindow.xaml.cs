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
        private static double scale = 200;
        private int score = 0;
        private PositionLogic pLogic = new PositionLogic();
        private static int xCoord = 380;
        private static int startXCoord = 850;
        private double retDouble;
        private int gateSpeed = 5;

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
            Canvas.SetTop(cursor, -scale + (kinect.zPosition * scale));

            foreach (var x in MyCanvas.Children.OfType<Image>()) {
                if ((string)x.Tag != "cursor")
                {
                    if (Canvas.GetLeft(x) >= 845)
                    {
                        Canvas.SetTop(x, pLogic.randomYCoord());
                    }
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - gateSpeed);
                    if (Canvas.GetLeft(x) == xCoord)
                    {
                        if (pLogic.isInGate(getCursorTop(), Canvas.GetTop(x)))
                        {
                            score++;
                            txtscore.Content = "Score: " + score;
                            if(score % 5 == 0)
                            {
                                gateSpeed += 5;
                            }
                        }
                    }

                    if (Canvas.GetLeft(x) <= -50)
                    {
                        Canvas.SetLeft(x, startXCoord);
                    }
                }                
            }

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

    }
}
