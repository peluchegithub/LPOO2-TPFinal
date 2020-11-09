using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarUsuarios
    {
        public Usuario TraerUsuario()
        {
            Usuario oUsuario = new Usuario();
            oUsuario.Usu_Username = "";
            oUsuario.Usu_Password = "";
            return oUsuario;
        }

        public TrabajarUsuarios()
        {
        }

        public ObservableCollection<Usuario> TraerUsuarios()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Usuario";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            ObservableCollection<Usuario> listaUsuarios = new ObservableCollection<Usuario>();
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Usuario oUsuario = new Usuario();
                Rol oRol = new Rol();
                oUsuario.Usu_Id = (int)reader["USU_id"];
                oUsuario.Usu_ApellidoNombre = (string)reader["USU_apellidoNombre"];
                oUsuario.Usu_Username = (string)reader["USU_username"];
                oUsuario.Usu_Password = (string)reader["USU_password"];
                oRol.Rol_Id = (int)reader["ROL_id"];
                if (oRol.Rol_Id == 1)
                {
                    oRol.Rol_Descripcion = "administrador";
                }
                else oRol.Rol_Descripcion = "vendedor";
                oUsuario.Usu_Rol = oRol;
                listaUsuarios.Add(oUsuario);
            }
            return listaUsuarios;
        }

        public static void AgregarUsuario(Usuario usuario)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_agregar_usuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@apellido", usuario.Usu_ApellidoNombre);
            cmd.Parameters.AddWithValue("@rol", usuario.Usu_Rol.Rol_Id);
            cmd.Parameters.AddWithValue("@username", usuario.Usu_Username);
            cmd.Parameters.AddWithValue("@password", usuario.Usu_Password);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static bool EliminarUsuario(int id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE Usuario where USU_id = @id";
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

        public static void ModificarUsuario(Usuario usuario)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_editar_usuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@id", usuario.Usu_Id);
            cmd.Parameters.AddWithValue("@nomUsu", usuario.Usu_Username);
            cmd.Parameters.AddWithValue("@pass", usuario.Usu_Password);
            cmd.Parameters.AddWithValue("@ayn", usuario.Usu_ApellidoNombre);
            cmd.Parameters.AddWithValue("@rol", usuario.Usu_Rol.Rol_Id);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
