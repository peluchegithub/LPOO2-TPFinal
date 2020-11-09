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
    /// Interaction logic for WinButacasMini.xaml
    /// </summary>
    public partial class WinButacasMini : Window
    {
        public WinButacasMini()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Faltaria obtener todas las butacas, recorrerlas e ir poniendoles el fondo rojo
            //si es que estan ocupadas
            FondoRojoSegunNombre("B",3);
        }

        private void FondoRojoSegunNombre(string fila, int columna)
        {
            String nombreButaca = fila + columna.ToString();
            Button btnButaca = (Button)gridButacas.FindName(nombreButaca);
            btnButaca.Background = Brushes.Red;
        }

        private void botonButaca_Click(object sender, RoutedEventArgs e)
        {
            Button botonButaca = (Button)sender;
            if (botonButaca.Background != Brushes.Red)
            {
                if (botonButaca.Background == Brushes.LightGray)
                {
                    if (MessageBox.Show("BUTACA SELECCIONADA: " + botonButaca.Name + "\nEstá Seguro?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PasarSeleccionAventanaPadre(botonButaca.Name);
                        botonButaca.Background = Brushes.Green;
                        MessageBox.Show("Gracias");
                        this.Close();
                    }                        
                }
                else
                {
                    botonButaca.Background = Brushes.LightGray;
                }
            }
            else
            {
                MessageBox.Show("La Butaca " + botonButaca.Name + " se encuentra Ocupada\n Por favor seleccione una diferente");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private static void PasarSeleccionAventanaPadre(string butaca)
        {
            WinAltaTicket parent = Application.Current.Windows.OfType<WinAltaTicket>().FirstOrDefault();
            parent.btnSeleccionButaca.IsEnabled = false;
            parent.txtFila.Text = butaca.Substring(0,1);
            parent.txtNumero.Text = butaca.Substring(1);
            parent.btnConfirmar.IsEnabled = true;
        }
    }
}
