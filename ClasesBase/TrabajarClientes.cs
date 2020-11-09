using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarClientes
    {
        public TrabajarClientes()
        { }

        public Cliente TraerCliente()
        {
            Cliente oCliente = new Cliente();
            oCliente.Cli_Apellido = "";
            oCliente.Cli_Email = "";
            oCliente.Cli_Nombre = "";
            oCliente.Cli_Telefono = "";
            return oCliente;
        }

        public static Cliente BuscarCliente(int dniBuscado)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_buscar_cliente_dni";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@pattern", dniBuscado);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cliente oCliente = new Cliente();
            if (dt.Rows.Count != 0)
            {
                oCliente.Cli_Apellido = dt.Rows[0]["Apellido"].ToString();
                oCliente.Cli_Nombre = dt.Rows[0]["Nombre"].ToString();
                oCliente.Cli_Email = dt.Rows[0]["Email"].ToString();
                oCliente.Cli_Telefono = dt.Rows[0]["Telefono"].ToString();
            }
            return oCliente;
        }

        public static void EditarCliente(Cliente cliente)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_editar_cliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@dni", cliente.Cli_Dni);
            cmd.Parameters.AddWithValue("@apellido", cliente.Cli_Apellido);
            cmd.Parameters.AddWithValue("@nombre", cliente.Cli_Nombre);
            cmd.Parameters.AddWithValue("@email", cliente.Cli_Email);
            cmd.Parameters.AddWithValue("@telefono", cliente.Cli_Telefono);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void AgregarCliente(Cliente cliente)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_agregar_cliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@dni", cliente.Cli_Dni);
            cmd.Parameters.AddWithValue("@nombre", cliente.Cli_Nombre);
            cmd.Parameters.AddWithValue("@apellido", cliente.Cli_Apellido);
            cmd.Parameters.AddWithValue("@email", cliente.Cli_Email);
            cmd.Parameters.AddWithValue("@telefono", cliente.Cli_Telefono);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        // Metodo para listar los clientes cargadas en la BD
        public static DataTable listarClientes()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_listar_clientes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public ObservableCollection<Cliente> TraerClientes()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Cliente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            ObservableCollection<Cliente> listaClientes = new ObservableCollection<Cliente>();
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cliente oCliente = new Cliente();
                oCliente.Cli_Id = (int)reader["CLI_id"];
                oCliente.Cli_Apellido = (string)reader["CLI_apellido"];
                oCliente.Cli_Nombre = (string)reader["CLI_nombre"];
                oCliente.Cli_Dni = (int)reader["CLI_dni"];
                oCliente.Cli_Email = (string)reader["CLI_email"];
                oCliente.Cli_Telefono = (string)reader["CLI_telefono"];
                listaClientes.Add(oCliente);
            }
            return listaClientes;
        }

        public static bool EliminarCliente(int id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE Cliente where CLI_id = @id";
                cmd.Connection = cnn;
                cmd.Parameters.AddWithValue("@id", id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        //TRAE TODOS LOS CLIENTES PARA LLENAR EL COMBO CLIENTES DE WIN ALTA TICKETS (VENTA DE TICKETS)    
        public static DataTable TraerClientesCombo()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CLI_id, CAST(CLI_dni AS varchar) + ' - ' + CLI_apellido + ', ' + CLI_nombre + ' - ' + CLI_email AS APENOM FROM Cliente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

    }
}










