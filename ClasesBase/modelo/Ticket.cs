using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Ticket
    {
        private string but_Fila;

        public string But_Fila
        {
            get { return but_Fila; }
            set { but_Fila = value; }
        }
        private int but_Numero;

        public int But_Numero
        {
            get { return but_Numero; }
            set { but_Numero = value; }
        }
        private int cli_Id;

        public int Cli_Id
        {
            get { return cli_Id; }
            set { cli_Id = value; }
        }
        private int pro_Id;

        public int Pro_Id
        {
            get { return pro_Id; }
            set { pro_Id = value; }
        }
        private int tic_Id;

        public int Tic_Id
        {
            get { return tic_Id; }
            set { tic_Id = value; }
        }
        private DateTime tic_FechaVenta;

        public DateTime Tic_FechaVenta
        {
            get { return tic_FechaVenta; }
            set { tic_FechaVenta = value; }
        }
    }
}
