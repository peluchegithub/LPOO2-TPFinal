using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Rol
    {
        private int rol_Id;

        public int Rol_Id
        {
            get { return rol_Id; }
            set { rol_Id = value; }
        }
        private string rol_Descripcion;

        public string Rol_Descripcion
        {
            get { return rol_Descripcion; }
            set { rol_Descripcion = value; }
        }
    }
}
