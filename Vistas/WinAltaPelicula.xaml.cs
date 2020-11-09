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
    public partial class WinAltaPelicula : Window
    {
        public WinAltaPelicula()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea guardar los datos ingresador?", "CONFIRMACION", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Pelicula oPelicula = new Pelicula();
                oPelicula.Pel_Titulo = txtTitulo.Text;
                oPelicula.Pel_Duracion = int.Parse(txtDuracion.Text);
                String genero = cbxGenero.SelectedValue.ToString();
                String clasificacion = cbxClasificacion.SelectedValue.ToString();

                MessageBox.Show("Se guardaron los siguiente datos: \n" +
                    "\n TITULO: " + oPelicula.Pel_Titulo +
                    "\n DURACION: " + oPelicula.Pel_Duracion +
                    "\n GENERO: " + genero +
                    "\n CLASIFICACION: " + clasificacion);

                LimpiarCamposPelicula();
            }
            else
            {
                LimpiarCamposPelicula();
            }
        }

        private void LimpiarCamposPelicula()
        {
            txtTitulo.Clear();
            txtDuracion.Clear();
            cbxGenero.SelectedItem = null;
            cbxClasificacion.SelectedItem = null;
        }
    
    }
}
