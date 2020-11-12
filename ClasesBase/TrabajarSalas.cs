using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarSalas
    {
        //TRAE IdSala SEGUN ID Proyeccion
        public static int? TraerIdSalaSegunIdProyeccion(int idProyeccion)
        {
            //int idSala;
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SAL_id FROM Proyeccion WHERE PRO_id=@idProyeccion";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@idProyeccion", idProyeccion);

            cnn.Open();
            int? idSala1 = (int?)cmd.ExecuteScalar();
            cnn.Close();

            return idSala1;

        }
    }
}
