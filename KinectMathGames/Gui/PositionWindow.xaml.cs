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
        private Rect finishRec = new Rect(-159, 0, 20, 283);
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Storyboard myStoryboard;

        public PositionWindow()
        {
            InitializeComponent();
            myStoryboard = Animation;
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();
            congrats.Visibility = Visibility.Hidden;
            FinalScore.Content = "";

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            //Canvas.SetTop(cursor, -scale + (kinect.zPosition * scale));
            //Canvas.SetTop(curRec, -scale + (kinect.zPosition * scale));
            foreach (var x in MyCanvas.Children.OfType<Image>()) {
               
                gat = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x)+52, 40, 30);
                

                if (Canvas.GetLeft(x) > 450)
                {
                    Canvas.SetTop(x, pLogic.randomYCoord());                        
                }

                if (intRec.IntersectsWith(gat))
                {
                    recCur = new Rect(getCursorLeft(), getCursorTop(), 1, 20);
                    if (recCur.IntersectsWith(gat) && pLogic.isInGate(getCursorTop(), Canvas.GetTop(x) + 45))
                    {
                        if ((string)x.Tag != "locked") 
                        {
                            score++;
                            x.Tag = "locked";
                            x.Source = (ImageSource)FindResource("GreenGateT");
                        }
                        txtscore.Content = "Score: " + score;
                    }                   
                }

                if(Canvas.GetLeft(x) < 140 && (string)x.Tag != "locked")
                {
                    x.Source = (ImageSource)FindResource("../Images/RedGateT");
                }

                if (x.Name == "img20" && finishRec.IntersectsWith(gat))
                {
                    FinalScore.Content = score;
                    congrats.Visibility = Visibility.Visible;
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

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /*private void OptionsClick(object sender, RoutedEventArgs e)
        {
            String state = optionsButton.Tag.ToString();
            if (state == "playing")
            {
                optionsButton.Tag = "paused";
                //pauseIcon.Source = (ImageSource)FindResource("PlayIcon");
                Animation.Stop();
            }
            else
            {
                optionsButton.Tag = "playing";
                //pauseIcon.Source = (ImageSource)FindResource("PauseIcon");
                Animation.Resume();
            }
        }*/

        private void StartResetClick(object sender, RoutedEventArgs e)
        {
            congrats.Visibility = Visibility.Hidden;
            instructions.Visibility = Visibility.Hidden;
            FinalScore.Content = "";
            String state = (sender as Button).Tag.ToString();
            if (state == "Start")
            {
                startResetButton.Content = "Reset";
            }
            startResetButton.Tag = "Reset";
            txtscore.Content = "Score: 0";
            score = 0;
            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "locked")
                {
                    x.Tag = "unLocked";
                }
            }
        }

        

    }
}
