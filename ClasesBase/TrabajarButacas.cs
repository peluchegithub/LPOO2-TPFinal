using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClasesBase
{
    public class TrabajarButacas
    {
        public static List<Butaca> TraerButacasOcupadasSegunIdSala(int? idSala)
        {
            List<Butaca> listaButacasOcupadas = new List<Butaca>();

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Butaca WHERE BUT_estado='Ocupada' AND SAL_id = @idSala";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@idSala", idSala);
            
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Butaca oCliente = new Butaca();
                oCliente.But_Codigo = (int)reader["BUT_codigo"];
                oCliente.But_Fila = char.Parse(reader["BUT_fila"].ToString());
                oCliente.But_Numero = (int)reader["BUT_numero"];
                oCliente.Sal_Id = (int)reader["SAL_id"];                
                oCliente.But_Estado = (string)reader["BUT_estado"];
                listaButacasOcupadas.Add(oCliente);
            }
            return listaButacasOcupadas;
        }

        public static void CambiarEstadoButaca(string fila, int numero, int? idSala)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Butaca SET BUT_estado='Ocupada' "+
                "WHERE BUT_fila=@fila AND BUT_numero=@numero AND SAL_id=@idSala";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@fila", fila);
            cmd.Parameters.AddWithValue("@numero", numero);
            cmd.Parameters.AddWithValue("@idSala", idSala);
            
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
