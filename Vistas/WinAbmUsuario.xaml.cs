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
    /// Interaction logic for WinAbmUsuario.xaml
    /// </summary>
    public partial class WinAbmUsuario : Window
    {
        public WinAbmUsuario()
        {
            InitializeComponent();
        }

        bool winloaded;
        CollectionView Vista;
        ObservableCollection<Usuario> listaUsuarios;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_USER"];
            listaUsuarios = odp.Data as ObservableCollection<Usuario>;
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(stack_content.DataContext);
            winloaded = true;
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToFirst();
            btnEdit.IsEnabled = false;
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToLast();
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

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            WinAltaUsuario oAltaUsuario = new WinAltaUsuario();
            oAltaUsuario.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = Vista.CurrentItem as Usuario;
            if (MessageBox.Show("Desea Eliminar Usuario?", "Eliminar Usuario", MessageBoxButton.YesNo) == MessageBoxResult.Yes)

                if (TrabajarUsuarios.EliminarUsuario(currentUser.Usu_Id))
                {
                    MessageBox.Show("Usuario Eliminado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    Vista.MoveCurrentToLast();
                    Vista.MoveCurrentToPosition(0);
                    WinAbmUsuario oAbmUsuario = new WinAbmUsuario();
                    oAbmUsuario.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al modificar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            Rol oRol = new Rol();
            usuario.Usu_Id = Convert.ToInt32(this.txtId.Text);
            usuario.Usu_Username = txtusername.Text;
            usuario.Usu_Password = txtPassword.Text;
            usuario.Usu_ApellidoNombre = txtNombre.Text;
            if (cbxRol.SelectedValue.ToString() == "administrador")
            {
                oRol.Rol_Id = 1;
                oRol.Rol_Descripcion = "administrador";
            }
            else
            {
                oRol.Rol_Id = 2;
                oRol.Rol_Descripcion = "vendedor";
            }

            usuario.Usu_Rol = oRol;
            TrabajarUsuarios.ModificarUsuario(usuario);
            MessageBox.Show("Usuario Modificado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            WinAbmUsuario oAbmUsuario = new WinAbmUsuario();
            oAbmUsuario.Show();
            this.Close();
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            btnEdit.IsEnabled = true;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            btnEdit.IsEnabled = true;
        }

        private void cbxRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!winloaded)
            {
                btnEdit.IsEnabled = true;
            }
            winloaded = false;
        }

    }
}
