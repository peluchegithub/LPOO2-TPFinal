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
    /// Interaction logic for WinListaUsuarios.xaml
    /// </summary>
    public partial class WinListaUsuarios : Window
    {
        private CollectionViewSource vistaColeccionFiltrada;

        public WinListaUsuarios()
        {
            InitializeComponent();
            vistaColeccionFiltrada = Resources["VISTA_USER"] as CollectionViewSource;
        }

        private void eventVistaUsuario_Filter(object sender, FilterEventArgs e)
        {
            Usuario oUsuario = e.Item as Usuario;
            if (oUsuario.Usu_Username.StartsWith(txtFiltrarUsername.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void txtFiltrarUsername_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaUsuario_Filter;
            }
        }

        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {
            VistaPreviaImpresion vistaP = new VistaPreviaImpresion();                        
            vistaP.txtFiltrarUserName = txtFiltrarUsername.Text;
            vistaP.ShowDialog();            
        }
    }
}
