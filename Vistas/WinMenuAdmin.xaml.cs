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
using System.Windows.Threading;
using Microsoft.Win32;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinMenuAdmin.xaml
    /// </summary>
    public partial class WinMenuAdmin : Window
    {
        DispatcherTimer dis = new DispatcherTimer();
        public WinMenuAdmin()
        {
            InitializeComponent();
            mostrarTiempo();
        }

        //mostrar fecha y hs//
        private void mostrarTiempo()
        {
            dis.Interval = new TimeSpan(0, 0, 1);
            dis.Tick += (s, a) =>
            {
                lbl_Hora.Content = DateTime.Now.ToLongTimeString();
                lbl_Fecha.Content = DateTime.Now.ToLongDateString();
            };
            dis.Start();
        }

        private void btnLoadSong_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myMp3 = new OpenFileDialog();
            myMp3.Filter = "Musica mp3 (*.mp3)|*.mp3";
            string pathMp3;
            if (myMp3.ShowDialog() == true) {
                pathMp3 = myMp3.FileName;
                lblName.Content = pathMp3;
                meAudio.LoadedBehavior = MediaState.Manual;
                meAudio.Source = new Uri(pathMp3);
                }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            meAudio.Play();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            meAudio.Stop();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            meAudio.Pause();
        }

       
    }
}
