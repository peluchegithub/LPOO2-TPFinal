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

            cmbProyeccion.ItemsSource = TrabajarProyecciones.TraerProyeccionesCombo().DefaultView;
            cmbProyeccion.SelectedIndex = 0;

            lblVendedor.Content = "vendedor";
        }

        private void btnSeleccionButaca_Click(object sender, RoutedEventArgs e)
        {
            WinButacasMini oWinButacasMini = new WinButacasMini();
            oWinButacasMini.ShowDialog();
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

    }
}
