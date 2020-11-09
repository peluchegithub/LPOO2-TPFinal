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
using System.Text.RegularExpressions;

namespace Vistas.controlUsuario
{
    /// <summary>
    /// Interaction logic for UCTextBox.xaml
    /// </summary>
    public partial class UCTextBox : UserControl
    {
        public UCTextBox()
        {
            InitializeComponent();
        }

        Boolean _SoloNumeroEntero = false;
        public Boolean SoloNumeroEntero
        {
            get { return _SoloNumeroEntero; }
            set
            {
                _SoloNumeroEntero = value;
                myTexbox.ToolTip = "Este textbox solo permite numeros";
            }
        }

        private void myTexbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (_SoloNumeroEntero)
            {
                e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
            }
        } 
    }
}
