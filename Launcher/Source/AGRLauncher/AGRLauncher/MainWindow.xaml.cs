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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AGRLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool signedIn;
        public MainWindow()
        {
            InitializeComponent();

            // Set signed in
            signedIn = false;
            btnEditAccount.Visibility = Visibility.Hidden;
            PilotName.Content = "Not logged in";
            PilotRank.Content = "Not Logged in";

            // Setup avatar
            //
            //-> No Account
            BitmapImage defaultAvatar = new BitmapImage();
            defaultAvatar.BeginInit();
            defaultAvatar.UriSource = new Uri("https://db.tt/g8wFaQ4z", UriKind.Absolute);
            defaultAvatar.EndInit();
            PilotAvatar.Source = defaultAvatar;
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimise_Click(object sender, RoutedEventArgs e)
        {
            Window.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AboutAGR2280_Collapsed(object sender, RoutedEventArgs e)
        {
            DarkenCover.Visibility = Visibility.Hidden;
            AboutRect.Visibility = Visibility.Hidden;
        }

        private void AboutAGR2280_Expanded(object sender, RoutedEventArgs e)
        {
            DarkenCover.Visibility = Visibility.Visible;
            AboutRect.Visibility = Visibility.Visible;
        }

        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.Background = Brushes.Red;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            btnClose.Background = (Brush)bc.ConvertFrom("#FF00A5FF");
        }

        private void btnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnClose.Background = Brushes.DarkRed;
        }

        private void btnMinimise_MouseEnter(object sender, MouseEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            btnMinimise.Background = (Brush)bc.ConvertFrom("#FF60C7FF");
        }

        private void btnMinimise_MouseLeave(object sender, MouseEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            btnMinimise.Background = (Brush)bc.ConvertFrom("#FF00A5FF");
        }

    }
}
