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
    public partial class WinAltaProyeccion : Window
    {
        public WinAltaProyeccion()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea guardar los datos ingresador?", "CONFIRMACION", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Proyeccion oProyeccion = new Proyeccion();
                oProyeccion.Pro_Fecha = dtFecha.Text;
                oProyeccion.Pro_Hora = txtHorario.Text;
                String pelicula = txtPelicula.Text;
                String sala = txtSala.Text;

                MessageBox.Show("Se guardaron los siguiente datos: \n" +
                    "\n PELICULA: " + pelicula +
                    "\n FECHA: " + oProyeccion.Pro_Fecha +
                    "\n HORARIO: " + oProyeccion.Pro_Hora +
                    "\n SALA: " + sala);

                LimpiarCamposPelicula();
            }
            else
            {
                LimpiarCamposPelicula();
            }
        }

        private void LimpiarCamposPelicula()
        {
            txtPelicula.Clear();
            txtHorario.Clear();
            txtSala.Clear();
        }

    }
}
