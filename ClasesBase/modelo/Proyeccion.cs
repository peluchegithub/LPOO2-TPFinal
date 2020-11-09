using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Proyeccion
    {
        private int pel_Id;

        public int Pel_Id
        {
            get { return pel_Id; }
            set { pel_Id = value; }
        }
        private string pro_Fecha;

        public string Pro_Fecha
        {
            get { return pro_Fecha; }
            set { pro_Fecha = value; }
        }
        private string pro_Hora;

        public string Pro_Hora
        {
            get { return pro_Hora; }
            set { pro_Hora = value; }
        }
        private int pro_Id;

        public int Pro_Id
        {
            get { return pro_Id; }
            set { pro_Id = value; }
        }
        private int sal_Id;

        public int Sal_Id
        {
            get { return sal_Id; }
            set { sal_Id = value; }
        }
    }
}
