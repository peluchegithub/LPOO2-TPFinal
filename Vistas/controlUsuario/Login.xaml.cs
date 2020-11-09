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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        public String Username
        {
            get { return txtUsuario.Text; }
        }

        public String Password
        {
            get { return pwbPassword.Text; }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                pwbPassword.Focus();
            }
        }

        private void pwbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Window parentWindow = Window.GetWindow(this);   //obtengo la ventada padre(winLogin)
                Button btnIniciar=(Button)parentWindow.FindName("btnIniciar");   //busco en la ventana el boton iniciar
                btnIniciar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));    //hago click en el boton iniciar
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsuario.Focus();
        }
    }
}
