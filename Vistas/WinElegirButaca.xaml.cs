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
    public partial class WinElegirButaca : Window
    {
        public WinElegirButaca()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnButaca = (Button)gridButacas.FindName("B3");
            btnButaca.Background = Brushes.Red;
        }

        private void botonButaca_Click(object sender, RoutedEventArgs e)
        {
            Button botonButaca = (Button)sender;
            if (botonButaca.Background != Brushes.Red)
            {
                if (botonButaca.Background == Brushes.LightGray)
                {
                    botonButaca.Background = Brushes.Green;
                }
                else
                {
                    botonButaca.Background = Brushes.LightGray;
                }
            }
        }

    }
}
