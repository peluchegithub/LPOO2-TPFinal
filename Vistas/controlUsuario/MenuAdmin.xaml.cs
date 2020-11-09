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
using ClasesBase;

namespace Vistas.controlUsuario
{
    /// <summary>
    /// Interaction logic for MenuAdmin.xaml
    /// </summary>
    public partial class MenuAdmin : UserControl
    {
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void btnAbmUsuarios_Click(object sender, RoutedEventArgs e)
        {
            WinAbmUsuario oAbmUsuario = new WinAbmUsuario();
            oAbmUsuario.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btnListaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            WinListaUsuarios oListaUsuarios = new WinListaUsuarios();
            oListaUsuarios.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btnPeliculas_Click(object sender, RoutedEventArgs e)
        {
            WinPeliculas oPeliculas = new WinPeliculas();
            oPeliculas.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btnButacas_Click(object sender, RoutedEventArgs e)
        {
            WinElegirButaca oElegirButaca = new WinElegirButaca();
            oElegirButaca.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btnProyecciones_Click(object sender, RoutedEventArgs e)
        {
            WinAltaProyeccion oAltaProyeccion = new WinAltaProyeccion();
            oAltaProyeccion.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            WinLogin menuLogin = new WinLogin();
            menuLogin.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}
