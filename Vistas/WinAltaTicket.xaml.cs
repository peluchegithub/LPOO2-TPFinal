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
using System.Data;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinAltaTicket.xaml
    /// </summary>
    public partial class WinAltaTicket : Window
    {
        public string vendedor;
        public List<string> listaButacasSeleccionadas=new List<string>();
        public WinAltaTicket()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCliente.ItemsSource = TrabajarClientes.TraerClientesCombo().DefaultView;
            cmbCliente.SelectedIndex = 0;

            lblVendedor.Content = "vendedor";

            dateProyeccion.DisplayDateStart = DateTime.Today;
            dateProyeccion.SelectedDate = DateTime.Today;
            dateProyeccion.Text = "Seleccione...";
            dateProyeccion.SelectedDate = null;

            cmbProyeccion.ItemsSource = TrabajarProyecciones.TraerProyeccionesCombo().DefaultView;
            cmbProyeccion.SelectedIndex = 0;
            cbxSalas.SelectedIndex = 0;

            if (cmbProyeccion.Items.Count == 0)
            {
                cmbProyeccion.ToolTip = "NO se encontraron proyecciones para HOY ni futuras.";
            }
            ButacasToggleButton.IsChecked = true;
            ButacasToggleButton.IsChecked = false;
        }

        private void btnSeleccionButaca_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProyeccion.SelectedValue != null)
            {
                WinButacasMini oWinButacasMini = new WinButacasMini();
                oWinButacasMini.tipo = cmbProyeccion.Text.Split('-')[0];
                oWinButacasMini.idProyeccion = (int)cmbProyeccion.SelectedValue;
                oWinButacasMini.ShowDialog();
            }
            else
            {
                MessageBox.Show("Primero seleccione una Proyeccion Por Favor");
            }
            
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            int IdTicket = 0;
            if (txtFila.Text != "" && txtNumero.Text != "")
            {
                Ticket oTicket = new Ticket();
                
                foreach (var butaca in listaButacasSeleccionadas)
                {
                    oTicket.Tic_FechaVenta = DateTime.Now;
                    oTicket.But_Fila = butaca.Substring(0,1);
                    oTicket.But_Numero = int.Parse(butaca.Substring(1));
                    oTicket.Cli_Id = int.Parse(cmbCliente.SelectedValue.ToString());
                    oTicket.Pro_Id = int.Parse(cmbProyeccion.SelectedValue.ToString());
                    oTicket.Tic_Precio = decimal.Parse(txtPrecio.Text);
                    //Butaca Ocupada
                    int? idSala = TrabajarSalas.TraerIdSalaSegunIdProyeccion(oTicket.Pro_Id);
                    TrabajarButacas.CambiarEstadoButaca(oTicket.But_Fila, oTicket.But_Numero, idSala);
                    //Guardar en BASE
                    IdTicket = TrabajarTickets.AgregarTicket(oTicket);
                    oTicket.Tic_Id = IdTicket;
                    
                    //Mostrar ticket                    
                    WinVentaTicket oWinVentaTicket = new WinVentaTicket();
                    oWinVentaTicket.vendedor = lblVendedor.Content.ToString();
                    oWinVentaTicket.ticket = oTicket;
                    oWinVentaTicket.InfoProyeccion = cmbProyeccion.Text;
                    oWinVentaTicket.InfoCliente = cmbCliente.Text;

                    oWinVentaTicket.Show();    
                }
                

                txtFila.Clear();
                txtNumero.Clear();
                //btnSeleccionButaca.IsEnabled = true;
                ButacasToggleButton.IsEnabled = true;
                ButacasToggleButton.IsChecked = false;
                btnConfirmar.IsEnabled = false;
                MessageBox.Show("Gracias por su compra");

            }
        }

        private void cbxSalas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSalas.SelectedValue != null)
            {
                string seleccionSala = cbxSalas.SelectedValue.ToString();

                if (seleccionSala == "Todas")
                {
                    cmbProyeccion.ItemsSource = TrabajarProyecciones.TraerProyeccionesCombo().DefaultView;
                }
                else
                {
                    cmbProyeccion.ItemsSource = TrabajarProyecciones.TraerProyeccionesComboFiltroSala(seleccionSala).DefaultView;
                }
                dateProyeccion.Text = "Seleccione...";
                dateProyeccion.SelectedDate = null;
                cmbProyeccion.SelectedIndex = 0;
            }
        }

        private void dateProyeccion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? fechaSeleccionada = dateProyeccion.SelectedDate;
            if (fechaSeleccionada.HasValue)
            {
                cbxSalas.SelectedIndex = -1;
                cmbProyeccion.ItemsSource = TrabajarProyecciones.TraerProyeccionesComboFiltroFecha(fechaSeleccionada).DefaultView;
                cmbProyeccion.SelectedIndex = 0;
            }
        }

        private void btnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            WinAltaClienteMini oWinAltaClienteMini = new WinAltaClienteMini();

            bool respuesta = (bool)oWinAltaClienteMini.ShowDialog();
            if (respuesta)//si guardó nuevo cliente, tengo que actualizar el combo Clientes
            {
                cmbCliente.ItemsSource = TrabajarClientes.TraerClientesCombo().DefaultView;
                cmbCliente.SelectedIndex = 0;
            }
        }

        private void cmbProyeccion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CrearButacas();
            if (cmbProyeccion.SelectedValue != null)
            {
                txtPrecio.Text = TrabajarProyecciones.TraerPrecioProyeccionPorID((int)cmbProyeccion.SelectedValue).ToString();                
            }
            
        }

        private void CrearButacas()
        {
            if (cmbProyeccion.SelectedValue != null)
            {
                string tipoSala = ((((DataRowView)cmbProyeccion.SelectedItem)).Row.ItemArray[1]).ToString().Split('-')[0];
                int? idSala = TrabajarSalas.TraerIdSalaSegunIdProyeccion((int)cmbProyeccion.SelectedValue);
                List<Butaca> listaButacasOcupadas = TrabajarButacas.TraerButacasOcupadasSegunIdSala(idSala);
                CrearButacasSegunTipo(tipoSala, listaButacasOcupadas);    
            }            
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
            else
            {//3D                
                lblPantalla.Content = "PANTALLA 3D";
                maxFilas = 5;
                lblFilaF.Visibility = Visibility.Hidden;
                lblFilaG.Visibility = Visibility.Hidden;
            }
            //CREACION DINAMICA DE BUTACAS
            gridButacas.Children.Clear();
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

                    Grid.SetColumn(MyButton, j);
                    Grid.SetRow(MyButton, i);
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
                    //PasarSeleccionAventanaPadre(botonButaca.Name);
                    botonButaca.Background = Brushes.Green;
                    listaButacasSeleccionadas.Add(botonButaca.Name);                                            
                }
                else
                {
                    botonButaca.Background = Brushes.LightGray;                   
                    listaButacasSeleccionadas.Remove(botonButaca.Name);
                }
            }
            else
            {
                MessageBox.Show("La Butaca " + botonButaca.Name + " se encuentra Ocupada\n Por favor seleccione una diferente");
            }
            //
            PasarSeleccionDeButacas();    
        }

        private void PasarSeleccionDeButacas()
        {
            if (listaButacasSeleccionadas.Count == 0)
            {
                txtFila.Clear();
                txtNumero.Clear();
                btnConfirmar.IsEnabled = false;
            }
            else
            {
                if (listaButacasSeleccionadas.Count == 1)
                {
                    txtFila.Text = listaButacasSeleccionadas[0].Substring(0, 1);
                    txtNumero.Text = listaButacasSeleccionadas[0].Substring(1);
                    btnConfirmar.IsEnabled = true;
                }
                else
                {
                    txtFila.Text = "-";
                    txtNumero.Text = "-";
                    btnConfirmar.IsEnabled = true;
                }
            }
        }

        private void ButacasToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            listaButacasSeleccionadas.Clear();
            CrearButacas();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            listaButacasSeleccionadas.Clear();
            PasarSeleccionDeButacas();
            ButacasToggleButton.IsChecked = false;
        }

        private void ButacasToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
