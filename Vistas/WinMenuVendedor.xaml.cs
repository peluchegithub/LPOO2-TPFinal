using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinMenuVendedor.xaml
    /// </summary>
    public partial class WinMenuVendedor : Window
    {
        public string vendedor;
        DispatcherTimer dis = new DispatcherTimer();
        public WinMenuVendedor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            menuVendedor.vendedor = vendedor;
            mostrarTiempo();
        }
        //mostrar fecha y hs//
        private void mostrarTiempo()
        {
            dis.Interval = new TimeSpan(0, 0, 1);
            dis.Tick += (s, a) =>
            {
                label_Hora.Content = DateTime.Now.ToLongTimeString();
                label_Fecha.Content = DateTime.Now.ToLongDateString();
            };
            dis.Start();
        }

    }
}
