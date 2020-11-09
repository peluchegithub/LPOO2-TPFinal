using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClasesBase
{
    public class TrabajarTickets
    {
        public static int AgregarTicket(Ticket oTicket)
        {
            int id = 0;
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_agregar_ticket";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@fechaVenta", oTicket.Tic_FechaVenta);
            cmd.Parameters.AddWithValue("@fila", oTicket.But_Fila);
            cmd.Parameters.AddWithValue("@numero", oTicket.But_Numero);
            cmd.Parameters.AddWithValue("@clienteId", oTicket.Cli_Id);
            cmd.Parameters.AddWithValue("@proyeccionId", oTicket.Pro_Id);
            cnn.Open();
            id = (int)cmd.ExecuteScalar();
            cnn.Close();

            return id;
        }
    }
}
