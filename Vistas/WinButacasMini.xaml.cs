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
using ClasesBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinButacasMini.xaml
    /// </summary>
    public partial class WinButacasMini : Window
    {
        public string tipo;
        public int idProyeccion;
        public WinButacasMini()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int? idSala = TrabajarSalas.TraerIdSalaSegunIdProyeccion(idProyeccion);
            List<Butaca> listaButacasOcupadas = TrabajarButacas.TraerButacasOcupadasSegunIdSala(idSala);
            CrearButacasSegunTipo(tipo, listaButacasOcupadas);
        }

        private void CrearButacasSegunTipo(string tipo, List<Butaca> listaButacasOcupadas)
        {
            int maxFilas, columna;
            char fila;
            if (string.IsNullOrEmpty(tipo))
            {
                tipo = "2D";                
            }

            if (tipo.Contains("2D"))
            {
                lblPantalla.Content = "PANTALLA 2D";
                maxFilas = 7; 
                lblFilaF.Visibility = Visibility.Visible;
                lblFilaG.Visibility = Visibility.Visible;
            }
            else {//3D                
                lblPantalla.Content = "PANTALLA 3D";
                maxFilas = 5;
                lblFilaF.Visibility = Visibility.Hidden;
                lblFilaG.Visibility = Visibility.Hidden;
            }
            //CREACION DINAMICA DE BUTACAS
            fila = 'A';

            for (int i = 0; i < maxFilas; i++)//filas
            {
                columna = 1;
                for (int j = 0; j < 5; j++)//columnas
                {
                    Button MyButton = new Button();
                    MyButton.Content = fila + columna.ToString();
                    MyButton.Name = fila + columna.ToString();
                    MyButton.Background = Brushes.LightGray;
                    MyButton.Click += botonButaca_Click;

                    //Verifico si ya esta ocupada
                    if (listaButacasOcupadas.Where(item => item.But_Fila.Equals(fila) && item.But_Numero == columna).FirstOrDefault() != null)
                    {
                        MyButton.Background = Brushes.Red;
                    }

                    Grid.SetColumn(MyButton, j + 1);
                    Grid.SetRow(MyButton, i + 1);
                    gridButacas.Children.Add(MyButton);
                    columna++;
                }
                fila++;
            }
            
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

        private static void PasarSeleccionAventanaPadre(string butaca)
        {
            WinAltaTicket parent = Application.Current.Windows.OfType<WinAltaTicket>().FirstOrDefault();
            //parent.btnSeleccionButaca.IsEnabled = false;
            parent.txtFila.Text = butaca.Substring(0, 1);
            parent.txtNumero.Text = butaca.Substring(1);
            parent.btnConfirmar.IsEnabled = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
