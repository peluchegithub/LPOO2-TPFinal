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

        Usuario oAdmin = new Usuario();
        Usuario oVendedor = new Usuario();

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            oAdmin.Usu_Username = "admin";
            oAdmin.Usu_Password = "admin";
            oVendedor.Usu_Username = "vendedor";
            oVendedor.Usu_Password = "vendedor";

            String pass = login.Username;

            if(login.Username.Equals(oAdmin.Usu_Username) && pass.Equals(oAdmin.Usu_Password))
            {
                WinMenuAdmin menuAdmin = new WinMenuAdmin();
                menuAdmin.Show();
                this.Close();
            }
            else
            {
                if (login.Username.Equals(oVendedor.Usu_Username) && pass.Equals(oVendedor.Usu_Password))
                {
                    WinMenuVendedor menuVendedor = new WinMenuVendedor();
                    menuVendedor.vendedor = "vendedor";//luego se pasara de la base de datos                    
                    menuVendedor.Show();
                    
                    this.Close();
                }
                else
                {
                    lblError.Content = "Datos incorrectos";
                }
            }
        }


    }
}
