using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Cliente : IDataErrorInfo, INotifyPropertyChanged
    {
        private string cli_Apellido;

        public string Cli_Apellido
        {
            get { return cli_Apellido; }
            set { cli_Apellido = value;
            Notificador(cli_Apellido);
            }
        }
        private int? cli_Dni;

        public int? Cli_Dni
        {
            get { return cli_Dni; }
            set { cli_Dni = value;
            Notificador(cli_Dni.ToString());
            }
        }
        private string cli_Email;

        public string Cli_Email
        {
            get { return cli_Email; }
            set { cli_Email = value;
            Notificador(cli_Email);
            }
        }
        private int cli_Id;

        public int Cli_Id
        {
            get { return cli_Id; }
            set { cli_Id = value;
            Notificador(cli_Id.ToString());
            }
        }
        private string cli_Nombre;

        public string Cli_Nombre
        {
            get { return cli_Nombre; }
            set { cli_Nombre = value;
            Notificador(cli_Nombre);
            }
        }
        private string cli_Telefono;

        public string Cli_Telefono
        {
            get { return cli_Telefono; }
            set { cli_Telefono = value;
            Notificador(cli_Telefono);
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
                    case "Cli_Apellido":
                        msg_error = validar_Apellido();
                        break;
                    case "Cli_Dni":
                        msg_error = validar_Dni();
                        break;
                    case "Cli_Email":
                        msg_error = validar_Email();
                        break;
                    case "Cli_Nombre":
                        msg_error = validar_Nombre();
                        break;
                    case "Cli_Telefono":
                        msg_error = validar_Telefono();
                        break;
                }
                return msg_error;
            }
        }

        private string validar_Apellido()
        {
            if (String.IsNullOrEmpty(Cli_Apellido))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validar_Dni()
        {
            if (Cli_Dni==null)
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validar_Email()
        {
            if (String.IsNullOrEmpty(Cli_Email))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validar_Nombre()
        {
            if (String.IsNullOrEmpty(Cli_Nombre))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validar_Telefono()
        {
            if (String.IsNullOrEmpty(Cli_Telefono))
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
