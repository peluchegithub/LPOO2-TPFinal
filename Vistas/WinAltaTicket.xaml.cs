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
    /// Interaction logic for WinAltaTicket.xaml
    /// </summary>
    public partial class WinAltaTicket : Window
    {
        public string vendedor;
        public WinAltaTicket()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCliente.ItemsSource = TrabajarClientes.TraerClientesCombo().DefaultView;
            cmbCliente.SelectedIndex=0;

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
            if (txtFila.Text!="" && txtNumero.Text!="")
            {
                Ticket oTicket = new Ticket();
                oTicket.Tic_FechaVenta = DateTime.Now;
                oTicket.But_Fila = txtFila.Text;
                oTicket.But_Numero = int.Parse(txtNumero.Text);
                oTicket.Cli_Id = int.Parse(cmbCliente.SelectedValue.ToString());
                oTicket.Pro_Id = int.Parse(cmbProyeccion.SelectedValue.ToString());
                //Butaca Ocupada
                int? idSala = TrabajarSalas.TraerIdSalaSegunIdProyeccion(oTicket.Pro_Id);
                TrabajarButacas.CambiarEstadoButaca(oTicket.But_Fila, oTicket.But_Numero, idSala);
                //Guardar en BASE
                IdTicket=TrabajarTickets.AgregarTicket(oTicket);
                oTicket.Tic_Id = IdTicket;
                //Mostrar ticket
                WinVentaTicket oWinVentaTicket = new WinVentaTicket();
                oWinVentaTicket.vendedor=lblVendedor.Content.ToString();
                oWinVentaTicket.ticket = oTicket;
                oWinVentaTicket.InfoProyeccion=cmbProyeccion.Text;
                oWinVentaTicket.InfoCliente=cmbCliente.Text;                
                
                oWinVentaTicket.ShowDialog();
                
                txtFila.Clear();
                txtNumero.Clear();
                btnSeleccionButaca.IsEnabled = true;
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
                }else{
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


    }
}
