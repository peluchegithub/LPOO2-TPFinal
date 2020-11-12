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
using System.Text.RegularExpressions;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinAltaClienteMini.xaml
    /// </summary>
    public partial class WinAltaClienteMini : Window
    {
        public WinAltaClienteMini()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (txtApellido.Text != "" && txtNombre.Text != "" && txtDni.Text != "" && txtEmail.Text != "" && txtTelefono.Text != "")
            {
                Cliente oCliente = new Cliente();
                oCliente.Cli_Apellido = txtApellido.Text;
                oCliente.Cli_Nombre = txtNombre.Text;
                oCliente.Cli_Dni = int.Parse(txtDni.Text);
                oCliente.Cli_Email = txtEmail.Text;
                oCliente.Cli_Telefono = txtTelefono.Text;

                TrabajarClientes.AgregarCliente(oCliente);

                MessageBox.Show("Cliente Guardado Exitosamente, Gracias");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor complete todos los campos");
            }
        }

        private void txtDni_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
