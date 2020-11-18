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
    /// Lógica de interacción para WinVentaTicket.xaml
    /// </summary>
    public partial class WinVentaTicket : Window
    {
        public string vendedor;
        public Ticket ticket;        
        public string InfoProyeccion;
        public string InfoCliente;
        public WinVentaTicket()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            //Seccion Ticket
            txbNumeroTicket.Text = ticket.Tic_Id.ToString();
            txbFechaHoraTicket.Text = ticket.Tic_FechaVenta.ToShortDateString();

            //Seccion Cliente
            string apellidoNombre = InfoCliente.Split('-')[1];
            txbApellidoCliente.Text = apellidoNombre.Split(',')[0];
            txbNombreCliente.Text = apellidoNombre.Split(',')[1];
            txbDNIcliente.Text = InfoCliente.Split('-')[0];

            //Seccion Proyeccion "2D - fecha - hora - pelicula            
            txbPelicula.Text = InfoProyeccion.Split('-')[3];
            txbHoraProyeccion.Text = InfoProyeccion.Split('-')[2];
            txbFechaProyeccion.Text = InfoProyeccion.Split('-')[1];
            txbUbicacionProyeccion.Text = "Butaca " + ticket.But_Fila + ticket.But_Numero.ToString();
            txbPrecio.Text = ticket.Tic_Precio.ToString();
            //Seccion Vendedor
            txbVendedor.Text = vendedor;
        }
    }
}
