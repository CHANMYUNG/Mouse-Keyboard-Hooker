using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using Gma.UserActivityMonitor;
using System.Runtime.InteropServices;

namespace getCoordinate
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        // string mouseClickPosition = null;
        
        
        public MainWindow()
        {
            InitializeComponent();
            var mouseHook = new MouseHook();
            MouseHook.MouseClicked += MouseHook_MouseClicked;
            mouseHook.Start();

            Closing += delegate
            {

                mouseHook.Stop();
            };
            
            
        }

        private void MouseHook_MouseClicked(object sender, MouseClickedEventArgs e)
        {
            var point = e.Hook.pt;

            XYbox.Text = $"({point.x},{point.y})";
        }
    }
}
