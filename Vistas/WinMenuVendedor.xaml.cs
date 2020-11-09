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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinMenuVendedor.xaml
    /// </summary>
    public partial class WinMenuVendedor : Window
    {
        public string vendedor;
        public WinMenuVendedor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            menuVendedor.vendedor = vendedor;
        }

    }
}
