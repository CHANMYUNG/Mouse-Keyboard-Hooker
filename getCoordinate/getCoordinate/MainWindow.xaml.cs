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
using System.Threading;
using System.Runtime.InteropServices;

namespace getCoordinate
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        // string mouseClickPosition = null;
        List<POINT> pointList = new List<POINT>();
        
        private POINT point;
        public MainWindow()
        {
            InitializeComponent();

            var mouseHook = new MouseHook();
            MouseHook.MouseClicked += MouseHook_MouseClicked;
            mouseHook.Start();

            var keybdHook = new KeyBdHook();
            KeyBdHook.KeyDown += KeyBdHook_KeyDown;
            KeyBdHook.KeyUp += KeyBdHook_KeyUp;
            keybdHook.Start();

            PointListBox.MouseLeftButtonDown += PointListBox_MouseLeftButtonDown;

            Closing += delegate
            {
                mouseHook.Stop();
                keybdHook.Stop();
            };
        }

        private void PointListBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            XYLabel.Content = PointListBox.SelectedItem;
        }

        private void KeyBdHook_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void KeyBdHook_KeyDown(object sender, KeyEventArgs e)
        {
            
            var key = e.Key;
            if(key == Keys.F7)
            {
                if (POINT.IsAddable(new POINT { x = point.x, y = point.y }, pointList))
                {
                    pointList.Add(new POINT { x = point.x, y = point.y });
                    PointListBox.Items.Add(pointList.Last());
                    
                }
              
            }

            else if(key == Keys.F8)
            {
                POINT pt;
                GetCursorPos(out pt);
                if (POINT.IsAddable(pt,pointList))
                {
                    pointList.Add(pt);
                    PointListBox.Items.Add(pointList.Last());
                }
                    XYLabel.Content = pt.ToString();
            }
            
        }
        

        private void MouseHook_MouseClicked(object sender, MouseClickedEventArgs e)
        {
            point = e.Hook.pt;
            XYLabel.Content = $"({point.x},{point.y})";
        }

        [DllImport("user32")]
        public static extern Int32 GetCursorPos(out POINT pt);
        
    }
}
