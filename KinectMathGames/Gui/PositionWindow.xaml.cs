using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        public PositionWindow()
        {
            InitializeComponent();
            
            this.DataContext = kinect;
        }
        
        public void MoveDot()
        {
            Canvas.SetTop(rec1, -200 + (kinect.zPosition * scale));
        }

        public void UpdateScore()
        {
            score += 1;
            txtscore.Content = "Score: " + score;
        }
    }
}
