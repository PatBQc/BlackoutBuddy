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

namespace WpfBlackScreen
{
    /// <summary>
    /// Thanks to Andrew Huberman for providing the insight to hide my own face in meetings.
    /// https://youtu.be/ClxRHJPz8aQ?t=9301
    /// </summary>
    public partial class MainWindow : Window
    {

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
            _window.Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0));
        }

        // When mouses leaves the window, it goes black again
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            _window.Background = new SolidColorBrush(Colors.Black);
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

    }
}
