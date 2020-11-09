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
    /// Lógica de interacción para VistaPreviaImpresion.xaml
    /// </summary>
    public partial class VistaPreviaImpresion : Window
    {
        public string txtFiltrarUserName;
        private CollectionViewSource vistaColeccionFiltrada;
        public VistaPreviaImpresion()
        {
            InitializeComponent();
            vistaColeccionFiltrada = Resources["VIEW_LIST_USERS"] as CollectionViewSource;
        }

        private void eventVistaUsuario_Filter(object sender, FilterEventArgs e)
        {         
            Usuario oUsuario = e.Item as Usuario;
            if (txtFiltrarUserName == null)
            {
                txtFiltrarUserName = "";
            }
            if (oUsuario.Usu_Username.StartsWith(txtFiltrarUserName, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            if (pdlg.ShowDialog() == true)
            {
                pdlg.PrintDocument(((IDocumentPaginatorSource)DocMain).DocumentPaginator, "Imprimir");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaUsuario_Filter;
            }
        }
    }
}
