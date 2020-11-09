using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Pelicula : IDataErrorInfo, INotifyPropertyChanged
    {
        private string pel_Clasificacion;

        public string Pel_Clasificacion
        {
            get { return pel_Clasificacion; }
            set { pel_Clasificacion = value; }
        }

        private string pel_Genero;

        public string Pel_Genero
        {
            get { return pel_Genero; }
            set { pel_Genero = value; }
        }

        private int pel_Id;

        public int Pel_Id
        {
            get { return pel_Id; }
            set { pel_Id = value; }
        }
        private string pel_Titulo;

        public string Pel_Titulo
        {
            get { return pel_Titulo; }
            set { pel_Titulo = value;
            Notificador("pel_Titulo");
            }
        }
        private int pel_Duracion;

        public int Pel_Duracion
        {
            get { return pel_Duracion; }
            set { pel_Duracion = value;
            Notificador("pel_Duracion");
            }
        }
        //Implementacion de la interface IDataErrorInfo
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string msg_error = null;
                switch (columnName)
                {
                    case "Pel_Titulo":
                        msg_error = validar_Titulo();
                        break;
                    case "Pel_Duracion":
                        msg_error = validar_Duracion();
                        break;
                }
                return msg_error;
            }
        }

        private string validar_Duracion()
        {
            if (String.IsNullOrEmpty(Pel_Duracion.ToString()))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validar_Titulo()
        {
            if (String.IsNullOrEmpty(Pel_Titulo))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        //Implementacion de la interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notificador(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
