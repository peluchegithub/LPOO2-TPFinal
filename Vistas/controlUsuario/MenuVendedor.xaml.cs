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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vistas.controlUsuario
{
    /// <summary>
    /// Interaction logic for MenuVendedor.xaml
    /// </summary>
    public partial class MenuVendedor : UserControl
    {
        public string vendedor;
        public MenuVendedor()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            WinLogin menuLogin = new WinLogin();
            menuLogin.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btnAbmClientes_Click(object sender, RoutedEventArgs e)
        {
            WinAbmCliente oAbmCliente = new WinAbmCliente();
            oAbmCliente.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btnListaCLientes_Click(object sender, RoutedEventArgs e)
        {
            WinListaClientes oListaClientes = new WinListaClientes();
            oListaClientes.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btnTickets_Click(object sender, RoutedEventArgs e)
        {
            WinAltaTicket oWinAltaTicket = new WinAltaTicket();
            oWinAltaTicket.vendedor = vendedor;
            oWinAltaTicket.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}
