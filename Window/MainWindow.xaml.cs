using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WaifuViewer.Utils;
using static System.Collections.Specialized.BitVector32;

namespace WaifuViewer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static String[] waifus = { "Mashiro", "Asuka" };
        private static int num = 0;



        public MainWindow()
        {
            InitializeComponent();
        }

        private ImageSource toSource(Bitmap img)
        {
            IntPtr ptr = img.GetHbitmap();
            var imageSource = Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageUtil.DeleteObject(ptr);
            return imageSource;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (num >= waifus.Length)
                num = 0;

            switch (waifus[num])
            {
                case "Mashiro":
                    PhotoBox.Source = toSource(Properties.Resources.Mashiro);
                    break;
                case "Asuka":
                    PhotoBox.Source = toSource(Properties.Resources.Asuka);
                    break;
            }
            num++;
        }
    }
}
