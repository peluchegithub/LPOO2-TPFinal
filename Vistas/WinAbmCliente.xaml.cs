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
    /// Interaction logic for WinAbmCliente.xaml
    /// </summary>
    public partial class WinAbmCliente : Window
    {
        public WinAbmCliente()
        {
            InitializeComponent();
        }

        CollectionView Vista;
        ObservableCollection<Cliente> listaClientes;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_CLIENTE"];
            listaClientes = odp.Data as ObservableCollection<Cliente>;
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(stack_content.DataContext);
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToFirst();
            btnEdit.IsEnabled = false;
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst)
            {
                Vista.MoveCurrentToLast();
            }
            btnEdit.IsEnabled = false;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToNext();
            if (Vista.IsCurrentAfterLast)
            {
                Vista.MoveCurrentToFirst();
            }
            btnEdit.IsEnabled = false;
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToLast();
            btnEdit.IsEnabled = false;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            WinAltaCliente oAltaCliente = new WinAltaCliente();
            oAltaCliente.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentCliente = Vista.CurrentItem as Cliente;
            if (MessageBox.Show("Desea Eliminar Cliente?", "Eliminar Cliente", MessageBoxButton.YesNo) == MessageBoxResult.Yes)

                if (TrabajarClientes.EliminarCliente(currentCliente.Cli_Id))
                {
                    MessageBox.Show("Cliente Eliminado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    Vista.MoveCurrentToLast();
                    Vista.MoveCurrentToPosition(0);
                    WinAbmCliente oAbmCliente = new WinAbmCliente();
                    oAbmCliente.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Cliente oCliente = new Cliente();
            oCliente.Cli_Id = Convert.ToInt32(this.txtId.Text);
            oCliente.Cli_Apellido = txtApellido.Text;
            oCliente.Cli_Nombre = txtNombre.Text;
            oCliente.Cli_Dni = Convert.ToInt32(this.txtDni.Text);
            oCliente.Cli_Email = txtEmail.Text;
            oCliente.Cli_Telefono = txtTelefono.Text;
            TrabajarClientes.EditarCliente(oCliente);
            MessageBox.Show("Cliente Modificado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            WinAbmCliente oAbmCliente = new WinAbmCliente();
            oAbmCliente.Show();
            this.Close();
        }

        private void txtApellido_KeyDown(object sender, KeyEventArgs e)
        {
            btnEdit.IsEnabled = true;
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            btnEdit.IsEnabled = true;
        }

        private void txtDni_KeyDown(object sender, KeyEventArgs e)
        {
            btnEdit.IsEnabled = true;
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            btnEdit.IsEnabled = true;
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            btnEdit.IsEnabled = true;
        }
    }
}
