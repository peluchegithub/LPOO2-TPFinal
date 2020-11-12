using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Butaca
    {
        private int but_Codigo;
        public int But_Codigo
        {
            get { return but_Codigo; }
            set { but_Codigo = value; }
        }
        
        private char but_Fila;
        public char But_Fila
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

        private int sal_Id;
        public int Sal_Id
        {
            get { return sal_Id; }
            set { sal_Id = value; }
        }

        private string but_Estado;
        public string But_Estado
        {
            get { return but_Estado; }
            set { but_Estado = value; }
        }
    }
}
