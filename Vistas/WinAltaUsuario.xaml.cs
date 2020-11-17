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
    /// Interaction logic for WinAltaUsuario.xaml
    /// </summary>
    public partial class WinAltaUsuario : Window
    {
        public WinAltaUsuario()
        {
            InitializeComponent();
        }

        private void btnAbmUsuario_Click(object sender, RoutedEventArgs e)
        {
            WinAbmUsuario oAbmUsuario = new WinAbmUsuario();
            oAbmUsuario.Show();
            this.Close();
        }

        private void btnPeliculas_Click(object sender, RoutedEventArgs e)
        {
            WinPeliculas oPeliculas = new WinPeliculas();
            oPeliculas.Show();
            this.Close();
        }

        private void btnButacas_Click(object sender, RoutedEventArgs e)
        {
            WinElegirButaca oElegirButaca = new WinElegirButaca();
            oElegirButaca.Show();
            this.Close();
        }

        private void btnProyecciones_Click(object sender, RoutedEventArgs e)
        {
            WinAltaProyeccion oAltaProyeccion = new WinAltaProyeccion();
            oAltaProyeccion.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            WinLogin menuLogin = new WinLogin();
            menuLogin.Show();
            this.Close();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {   

            Usuario oUsuario = new Usuario();
            Rol oRol = new Rol();
            oUsuario.Usu_ApellidoNombre = txtApellido.Text;
            if (cbxRol.SelectedValue.ToString() == "Administrador")
            {
                oRol.Rol_Id = 1;
                oRol.Rol_Descripcion = "administrador";
            }
            else
            {
                oRol.Rol_Id = 2;
                oRol.Rol_Descripcion = "vendedor";
            }
            oUsuario.Usu_Rol = oRol;
            oUsuario.Usu_Username = txtUsername.Text;
            oUsuario.Usu_Password = txtPassword.Text;
            TrabajarUsuarios.AgregarUsuario(oUsuario);

            WinAbmUsuario oAbmUsuario = new WinAbmUsuario();
            oAbmUsuario.Show();
            this.Close();
        }

        private void btnListadoUsuarios_Click(object sender, RoutedEventArgs e)
        {
            WinListaUsuarios oListaUsuarios = new WinListaUsuarios();
            oListaUsuarios.Show();
            this.Close();
        }

    }
}
