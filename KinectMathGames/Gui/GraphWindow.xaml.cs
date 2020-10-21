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
using KinectMathGames.Domain;

namespace KinectMathGames.Gui
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        double scale = 50;
        Kinect sensor = new Kinect();
        public GraphWindow()
        {
            InitializeComponent();

            //Set up squares for grid
            for(int i = 0; i <= 100; i+=10)
            {
                Line line = new Line() { Stroke = Brushes.Gray, StrokeThickness = 0.5, X1 = i, X2=i, Y1 = 0, Y2 = 100, SnapsToDevicePixels=true};
                MyCanvas.Children.Add(line);
                Line line2 = new Line() { Stroke = Brushes.Gray, StrokeThickness = 0.5, X1 = 0, X2 = 100, Y1 = i, Y2 = i };
                MyCanvas.Children.Add(line2);
            }

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Timer_Tick;

            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double playerYPos = -75 + (sensor.zPosition * scale);
            double playerXPos = 50 + (sensor.xPosition * scale);
            Canvas.SetTop(rec1, playerYPos);
            Canvas.SetLeft(rec1, playerXPos);
            ypos.Content = 5-Math.Round(playerYPos/10);
            xpos.Content = Math.Round(playerXPos/10)-5;
        }
    }
}
