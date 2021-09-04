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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBlackScreen
{
    /// <summary>
    /// Thanks to Andrew Huberman for providing the insight to hide my own face in meetings.
    /// https://youtu.be/ClxRHJPz8aQ?t=9301
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _scaleHorizontally = true;

        // Icon taken from : https://thenounproject.com/term/no-cameras/954941/
        public MainWindow()
        {
            InitializeComponent();
        }

        // The Windows open bottom right of the screen it launched in
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = System.Windows.SystemParameters.WorkArea.Width - Width;
            Top = System.Windows.SystemParameters.WorkArea.Height - Height;
        }

        // When the mouse arrives on top of the window, it becomes semi translucid
        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0));
        }

        // When mouses leaves the window, it goes black again
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.Black);
        }

        // Clicking and holding pressed the left mouse button on the window enables to drag it
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // Double clicking the window closes it
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        // Mainly grab the cursors input to move the window on a per pixel basis...
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            double delta = 1;

            if (e.Key == Key.Left)
            {
                Left -= delta;
            }
            if (e.Key == Key.Right)
            {
                Left += delta;
            }
            if (e.Key == Key.Up)
            {
                Top -= delta;
            }
            if (e.Key == Key.Down)
            {
                Top += delta;
            }

            ///...and also keep tracks of wether the CTRL key is pressed to scale the window vertically 
            if(e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                _scaleHorizontally = false;
            }
        }

        // Just assume we are now releasing any key (and the CTRL) so that we start scaling horizontally again with the mouse wheel
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            _scaleHorizontally = true;
        }


        // use the MouseWheel to scale the window.  While holding CTRL on the keyboard, it scales vertically, horizontally otherwise.
        // Reference: https://social.msdn.microsoft.com/Forums/vstudio/en-US/df5a4b2a-c2ee-476b-a7aa-b7fe56ed931b/arrow-keys-events?forum=wpf
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int delta = e.Delta;
            double w1 = _scaleHorizontally ? ActualWidth : ActualHeight;
            double w2 = w1 + 2 * delta / 20;
            DoubleAnimation anima = new DoubleAnimation();
            anima.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            anima.From = w1;
            anima.To = w2;
            anima.FillBehavior = FillBehavior.HoldEnd;
            BeginAnimation(_scaleHorizontally ? Window.WidthProperty : Window.HeightProperty, anima);
        }

    }
}
