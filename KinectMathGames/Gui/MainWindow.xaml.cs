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
using KinectMathGames.Gui;

namespace KinectMathGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PositionGameClick(object sender, RoutedEventArgs e)
        {
            PositionWindow positionWindow = new PositionWindow();
            positionWindow.Show();
            this.Close();
        }

        private void VelocityGameClicked(object sender, RoutedEventArgs e)
        {
            VelocityWindow velocityWindow = new VelocityWindow();
            velocityWindow.Show();
            this.Close();
        }

        private void GraphGameClicked(object sender, RoutedEventArgs e)
        {
            GraphWindow graphWindow = new GraphWindow();
            graphWindow.Show();
            this.Close();
        }
    }
}
