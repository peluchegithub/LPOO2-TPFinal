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
    public partial class WinLogin : Window
    {
        public WinLogin()
        {
            InitializeComponent();
        }

        Usuario usuLogin = new Usuario();

        TrabajarUsuarios tUsuario = new TrabajarUsuarios();

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {


            if (tUsuario.obtenerUsuario_BD(login.Username, login.Password) != null)
            {

                usuLogin = tUsuario.obtenerUsuario_BD(login.Username, login.Password);
                if (usuLogin.Rol_Id == 1)
                {
                    //usuLogin = tUsuario.obtenerUsuario_BD(login.Username, login.Password);
                    MessageBox.Show("Bienvenido al Sistema", "Bienvenido al Sistema", MessageBoxButton.OK, MessageBoxImage.Information);

                    WinMenuAdmin menuAdmin = new WinMenuAdmin();
                    menuAdmin.Show();
                    this.Close();
                }
                else
                {
                    //usuLogin = tUsuario.obtenerUsuario_BD(login.Username, login.Password);
                    MessageBox.Show("Bienvenido al Sistema", "Bienvenido al Sistema", MessageBoxButton.OK, MessageBoxImage.Information);

                    WinMenuVendedor menuVendedor = new WinMenuVendedor();
                    menuVendedor.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Usuario o Constraseña INCORRECTA", "ATENCION", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                LimpiarCampos();
            }
        }
        private void LimpiarCampos()
        {
            
            login.txtUsuario.Clear();
            login.pwdPassword.Clear();

        }

       
    }
}
