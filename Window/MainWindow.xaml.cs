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

namespace WaifuViewer {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        private static String[] waifus = { "Mashiro", "Asuka" };
        private static Hashtable photos = new Hashtable();
        private static int num = 0;
        private static int pic = 0;
        private static string title = "Waifu 查看器 [WPF C# Demo By Klop233] [Image from Aokana] ";


        public MainWindow()
        {
            ImageSource[] Mashiro =
            {
                toSource(Properties.Resources.Mashiro1),
                toSource(Properties.Resources.Mashiro2),
                toSource(Properties.Resources.Mashiro3),
                toSource(Properties.Resources.Mashiro4)
            };

            ImageSource[] Asuka =
            {
                toSource(Properties.Resources.Asuka1),
                toSource(Properties.Resources.Asuka2),
                toSource(Properties.Resources.Asuka3),
                toSource(Properties.Resources.Asuka4),
                toSource(Properties.Resources.Asuka5)
            };

            photos.Add("Mashiro", Mashiro);
            photos.Add("Asuka", Asuka);

            InitializeComponent();
        }

        private ImageSource toSource(Bitmap img)
        {
            IntPtr ptr = img.GetHbitmap();
            var imageSource = Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageUtil.DeleteObject(ptr);
            return imageSource;
        }

        private void OnSwitchWaifu(object sender, RoutedEventArgs e)
        {
            if (num + 1 >= waifus.Length)
            {
                num = 0;
            }
            else
            {
                num++;
            }
            pic = 0;
            PhotoBox.Source = ((ImageSource[]) photos[waifus[num]])[pic];
            Title = title + string.Format("[Current:{0} Pic: {1}]", waifus[num], pic+1);
        }

        private void OnSwitchPhoto(object sender, RoutedEventArgs e)
        {
            string waifu = waifus[num];
            ImageSource[] imgs = (ImageSource[]) photos[waifu];
            if (pic+1 >= imgs.Length)
            {
                pic = 0;
                PhotoBox.Source = imgs[pic];
                Title = title + string.Format("[Current:{0} Pic: {1}]", waifus[num], pic + 1);
                return;
            }
            pic++;
            Title = title + string.Format("[Current:{0} Pic: {1}]", waifus[num], pic + 1);
            PhotoBox.Source = imgs[pic];
            
        }
    }
}
