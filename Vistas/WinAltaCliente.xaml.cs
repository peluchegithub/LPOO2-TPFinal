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
    public partial class WinAltaCliente : Window
    {
        public WinAltaCliente()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea guardar los datos ingresador?", "CONFIRMACION", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Cliente oCliente = new Cliente();
                oCliente.Cli_Apellido = txtApellido.Text;
                oCliente.Cli_Nombre = txtNombre.Text;
                oCliente.Cli_Dni = int.Parse(txtDni.Text);
                oCliente.Cli_Email = txtEmail.Text;
                oCliente.Cli_Telefono = txtTelefono.Text;
                TrabajarClientes.AgregarCliente(oCliente);
                WinAbmCliente oAbmCliente = new WinAbmCliente();
                oAbmCliente.Show();
                this.Close();
            }
            else
            {
                LimpiarCamposCliente();
            }
        }

        private void LimpiarCamposCliente()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
        }
    }
}
