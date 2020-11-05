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
        Ellipse point = new Ellipse() { Name = "Point", Width = 5, Height = 5, Fill = Brushes.Blue, SnapsToDevicePixels = true, Margin= new Thickness(-5,-5,-5,-5) };
        Line slope = new Line() { Stroke = Brushes.Green, StrokeThickness = 2, SnapsToDevicePixels = true };
        Random random = new Random();
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

            MyCanvas.Children.Add(slope);
            MyCanvas.Children.Add(point);
            Generate_Point();
            

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Timer_Tick;

            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer.Start();

            DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
            dispatcherTimer1.Tick += Big_Tick;

            dispatcherTimer1.Interval = TimeSpan.FromSeconds(20);
            dispatcherTimer1.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double playerYPos = -75 + (sensor.zPosition * scale);
            double playerXPos = 50 + (sensor.xPosition * scale);
            Canvas.SetTop(rec1, playerYPos);
            Canvas.SetLeft(rec1, playerXPos);
        }

        private void Big_Tick(object sender, EventArgs e)
        {
            Generate_Point();
        }

        private void Generate_Point()
        {
            double pointX = 50+random.Next(-3, 3) * 10;
            double pointY = 50+random.Next(-3, 3) * 10;
            double slopeRise = random.Next(-5, 5) * 10;
            double slopeRun = random.Next(-5, 5) * 10;
            while (slopeRun == 0)
                slopeRun = random.Next(-5, 5) * 10;

            slope.X1 = pointX;
            slope.X2 = pointX;
            slope.Y1 = pointY;
            slope.Y2 = pointY;

            slope.X1 += slopeRun*-10;
            slope.Y1 += slopeRise*10;
            slope.X2 += slopeRun*10;
            slope.Y2 += slopeRise*-10;

            pointSlopeLbl.Text = "(y ";
            if ((5-(pointY / 10)) < 0)
                pointSlopeLbl.Text += "- ";
            else
                pointSlopeLbl.Text += "+ ";
            pointSlopeLbl.Text += Math.Abs(5-(pointY / 10)) + ") = ";
            pointSlopeLbl.Text +=(slopeRise/10) + "/" + (slopeRun/10);
            pointSlopeLbl.Text += "(x ";
            if (((pointX / 10) - 5) < 0)
                pointSlopeLbl.Text += "- ";
            else
                pointSlopeLbl.Text += "+ ";
            pointSlopeLbl.Text += Math.Abs((pointX / 10) - 5) + ")";
        }

        private void Remove_Point(object sender, EventArgs e)
        {

        }
    }
}
