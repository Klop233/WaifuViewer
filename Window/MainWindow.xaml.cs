using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        private static string[] waifus = { "Mashiro", "Asuka" };
        private static Hashtable photos = new Hashtable();
        private static int num = 0;
        private static int pic = 0;
        private static string defaultTitle = "Waifu 查看器 [WPF C# Demo By Klop233] [Image from Aokana] ";

       

        public MainWindow()
        {
            // Refelction class
            string resource = "WaifuViewer.Properties.Resources";
            Type type = Type.GetType(resource);
            var Asuka = new List<ImageSource>();
            var Mashiro = new List<ImageSource>();
            // Image count of a character
            int n = 0;

            foreach (PropertyInfo p in type.GetProperties())
            {
                if (p.Name.StartsWith("Asuka")) {
                    Asuka.Add(ToSource(p.GetValue(type) as Bitmap));
                }
                n++;
            }

            n = 0;

            foreach (PropertyInfo p in type.GetProperties())
            {
                if (p.Name.StartsWith("Mashiro")) {
                    Mashiro.Add(ToSource(p.GetValue(type) as Bitmap));
                }
            }

            photos.Add("Asuka", Asuka);
            photos.Add("Mashiro", Mashiro);

            InitializeComponent();
        }

        private ImageSource ToSource(Bitmap img)
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
            PhotoBox.Source = (photos[waifus[num]] as List<ImageSource>)[pic];
            Title = defaultTitle + string.Format("[Current:{0} Pic: {1}]", waifus[num], pic+1);
        }

        private void OnSwitchPhoto(object sender, RoutedEventArgs e)
        {
            string waifu = waifus[num];
            string title;
            var imgs = photos[waifu] as List<ImageSource>;
            if (pic+1 >= imgs.Count)
            {
                pic = 0;
            } else
            {
                pic++;
            }
            
            title = defaultTitle + string.Format("[Current:{0} Pic: {1}]", waifus[num], pic + 1);
            PhotoBox.Source = imgs[pic];
        }
    }
}
