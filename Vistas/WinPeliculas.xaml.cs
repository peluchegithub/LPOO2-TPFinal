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
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinPeliculas.xaml
    /// </summary>
    public partial class WinPeliculas : Window
    {
        private CollectionViewSource vistaColeccionFiltrada;        
        ObservableCollection<Pelicula> listaPeliculas;
        string ruta, rutaVideo;
        Pelicula peliculaSeleccionada = new Pelicula();
        public WinPeliculas()

        {
            InitializeComponent();
            vistaColeccionFiltrada = Resources["VISTA_PELICULA"] as CollectionViewSource;
        }
        
        //FILTRO
        private void eventVistaPelicula_Filter(object sender, FilterEventArgs e)
        {
            Pelicula oPelicula = e.Item as Pelicula;
            if (oPelicula.Pel_Titulo.ToLower().Contains(txtFiltrarTitulo.Text.ToLower()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        //CUANDO MODIFICO EL TEXTO DEL FILTRO 
        private void txtFiltrarTitulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFiltrarTitulo.Text))
            {
                btnClear.Visibility = Visibility.Hidden;                
            }
            else
            {
                btnClear.Visibility = Visibility.Visible;
            }
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaPelicula_Filter;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFiltrarTitulo.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["listar_peliculas"];
            listaPeliculas = odp.Data as ObservableCollection<Pelicula>;
            
            AddToggleButton.IsChecked = true;
            AddToggleButton.IsChecked = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            AddToggleButton.IsChecked = false;
        }

        //BOTON AGREGAR PELICULA
        private void AddToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            LimpiarCamposPelicula();
            lblTituloLateral.Content = "AGREGAR PELICULA";
        }

        private void LimpiarCamposPelicula()
        {
            txtTitulo.Clear();
            txtDuracion.Clear();
            cbxClasificacion.SelectedValue = "ATP";
            cbxGenero.SelectedValue = "Accion";
            image1.Source = null;
        }

        //VALIDACION SOLO NUMEROS (campo duracion)
        private void txtDuracion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        //BOTON GUARDAR (AGREGAR Y MODIFICAR)
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            int Id;
            Pelicula oPelicula = new Pelicula();
            
            oPelicula.Pel_Titulo = txtTitulo.Text;
            oPelicula.Pel_Duracion = int.Parse(txtDuracion.Text);
            oPelicula.Pel_Genero = cbxGenero.SelectedValue.ToString();
            oPelicula.Pel_Clasificacion = cbxClasificacion.SelectedValue.ToString();
            oPelicula.Pel_imagen = ruta;
            oPelicula.Pel_avance = rutaVideo;
            
          if (lblTituloLateral.Content.ToString() == "AGREGAR PELICULA")

            // if(TrabajarPeliculas.BuscarPelicula(oPelicula.Pel_Titulo) == false)
            {
                //Agregar en DB
                Id = TrabajarPeliculas.AgregarPelicula(oPelicula);
                //Agregar en Collection
                oPelicula.Pel_Id = Id;
                listaPeliculas.Add(oPelicula);
            }
            else { //MODIFICAR PELICULA
                //Guardo en DB
                Pelicula peliculaSeleccionada = grillaPeliculas.SelectedItem as Pelicula;
                Id = peliculaSeleccionada.Pel_Id;
                oPelicula.Pel_Id = Id;
                TrabajarPeliculas.ModificarPelicula(oPelicula);
                //Modifico en Collection
                int indexBuscado = listaPeliculas.IndexOf(peliculaSeleccionada);
                listaPeliculas[indexBuscado] = oPelicula;
            }
            
            LimpiarCamposPelicula();

            //Mejorar esta ventana:
            MessageBox.Show("Se guardaron los siguiente datos: \n" +
                "\n TITULO: " + oPelicula.Pel_Titulo +
                "\n DURACION: " + oPelicula.Pel_Duracion +
                "\n GENERO: " + oPelicula.Pel_Genero +
                "\n CLASIFICACION: " + oPelicula.Pel_Clasificacion);
            
            AddToggleButton.IsChecked = false;
            btnEliminar.Visibility = Visibility.Hidden;
        }

        //CUANDO CAMBIO DE PELICULA SELECCIONADA
        private void grillaPeliculas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
             peliculaSeleccionada = grillaPeliculas.SelectedItem as Pelicula;
            if (peliculaSeleccionada != null)
            {
                btnEliminar.Visibility = Visibility.Visible;

                txtTitulo.Text = peliculaSeleccionada.Pel_Titulo;
                txtDuracion.Text = peliculaSeleccionada.Pel_Duracion.ToString();
                cbxClasificacion.SelectedValue = peliculaSeleccionada.Pel_Clasificacion;
                cbxGenero.SelectedValue = peliculaSeleccionada.Pel_Genero;
               // Uri resourceUri = new Uri(peliculaSeleccionada.Pel_imagen, UriKind.Absolute);
                // image1.Source = new BitmapImage(resourceUri);
                ruta = peliculaSeleccionada.Pel_imagen;
                rutaVideo = peliculaSeleccionada.Pel_avance;
            }

        }

        //BOTON ELIMINAR PELICULA
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Pelicula peliculaSeleccionada = grillaPeliculas.SelectedItem as Pelicula;
            if (peliculaSeleccionada != null)
            {
                if (MessageBox.Show("Desea Eliminar Pelicula "+peliculaSeleccionada.Pel_Titulo+" ?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)

                    if (TrabajarPeliculas.EliminarPelicula(peliculaSeleccionada.Pel_Id))
                    {
                        //Elimino de la Collection
                        listaPeliculas.Remove(peliculaSeleccionada);

                        MessageBox.Show("Pelicula Eliminada correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                        btnEliminar.Visibility = Visibility.Hidden;                        
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
            }
        }

        //DOBLE CLICK EN GRILLA PELICULA
        private void grillaPeliculas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddToggleButton.IsChecked = true;//Muestro lateral
            btnEliminar.Visibility = Visibility.Hidden;

            Pelicula peliculaSeleccionada = grillaPeliculas.SelectedItem as Pelicula;
            if (peliculaSeleccionada != null)
            {
                lblTituloLateral.Content = "EDITAR PELICULA";

                txtTitulo.Text = peliculaSeleccionada.Pel_Titulo;
                txtDuracion.Text = peliculaSeleccionada.Pel_Duracion.ToString();
                cbxClasificacion.SelectedValue = peliculaSeleccionada.Pel_Clasificacion;
                cbxGenero.SelectedValue = peliculaSeleccionada.Pel_Genero;
               // Uri resourceUri = new Uri(peliculaSeleccionada.Pel_imagen, UriKind.Absolute);
               // image1.Source = new BitmapImage(resourceUri);
                ruta = peliculaSeleccionada.Pel_imagen;
            }
        }

        private void btnAbrirImg_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                //textBox_Dire.Text = dlg.FileName;
                Uri resourceUri = new Uri(dlg.FileName, UriKind.Absolute);
                image1.Source = new BitmapImage(resourceUri);
                ruta = dlg.FileName;
            }
        }

        private void btnVerTrailer_Click(object sender, RoutedEventArgs e)
        {
            WinTrailer wTrailer = new WinTrailer(peliculaSeleccionada);

            wTrailer.Show();
        }

        private void btnAbrirTrailer_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Mp4 Files|*.mp4";
            openFileDialog.ShowDialog();

            rutaVideo = openFileDialog.FileName;
        }

    }
}