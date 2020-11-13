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
using System.Windows.Media.Animation;
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
        private double retDouble;
        private Rect recCur = new Rect();
        private Rect gat = new Rect();
        private Rect intRec = new Rect(132, 0, 20, 283);
        

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
            //Canvas.SetTop(cursor, -scale + (kinect.zPosition * scale));
            //Canvas.SetTop(curRec, -scale + (kinect.zPosition * scale));
            foreach (var x in MyCanvas.Children.OfType<Image>()) {
               
                gat = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), 40, 30);
                

                if (Canvas.GetLeft(x) > 450)
                {
                    Canvas.SetTop(x, pLogic.randomYCoord());                        
                }

                if (intRec.IntersectsWith(gat))
                {
                    recCur = new Rect(getCursorLeft(), getCursorTop(), 1, 20);
                    if (recCur.IntersectsWith(gat))
                    {
                        if ((string)x.Tag != "locked") 
                        {
                            score++;
                            x.Tag = "locked";
                        }
                        txtscore.Content = "Score: " + score;
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
        public double getCursorLeft()
        {
            foreach (var x in MyCanvas.Children.OfType<Ellipse>())
            {
                if ((string)x.Tag == "cursor")
                {
                    retDouble = Canvas.GetLeft(x);
                    break;
                }
            }
            return retDouble;
        }

    }
}
