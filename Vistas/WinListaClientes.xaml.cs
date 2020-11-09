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
using System.Collections.ObjectModel;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinListaClientes.xaml
    /// </summary>
    public partial class WinListaClientes : Window
    {
        private CollectionViewSource vistaColeccionFiltrada;

        public WinListaClientes()
        {
            InitializeComponent();
            vistaColeccionFiltrada = Resources["VISTA_CLIENTE"] as CollectionViewSource;
        }

        private void eventVistaCliente_Filter(object sender, FilterEventArgs e)
        {
            Cliente oCliente = e.Item as Cliente;
            if (oCliente.Cli_Apellido.StartsWith(txtFiltrarApellido.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void txtFiltrarApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
            }
        }
    }
}
