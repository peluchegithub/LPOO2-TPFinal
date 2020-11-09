using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Sala
    {
        private int sal_Id;

        public int Sal_Id
        {
            get { return sal_Id; }
            set { sal_Id = value; }
        }
        private string sal_Descripcion;

        public string Sal_Descripcion
        {
            get { return sal_Descripcion; }
            set { sal_Descripcion = value; }
        }
        private int sal_Capacidad;

        public int Sal_Capacidad
        {
            get { return sal_Capacidad; }
            set { sal_Capacidad = value; }
        }
        private bool sal_Estado;

        public bool Sal_Estado
        {
            get { return sal_Estado; }
            set { sal_Estado = value; }
        }
    }
}
