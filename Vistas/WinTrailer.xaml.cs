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
    /// Lógica de interacción para WinTrailer.xaml
    /// </summary>
    public partial class WinTrailer : Window
    {
        public WinTrailer(Pelicula peliculaSeleccionada)
        {
            InitializeComponent();
            TransformGroup tg = new TransformGroup();
            meMovie.RenderTransform = tg;
            meMovie.Source = new Uri(peliculaSeleccionada.Pel_avance, UriKind.Relative);
        }


        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            meMovie.Play();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            meMovie.Stop();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            meMovie.Pause();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

            
                
            
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }


       
    }
}
