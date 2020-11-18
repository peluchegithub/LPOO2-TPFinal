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
                oUsuario.Usu_Estado = (string)reader["USU_estado"];
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
            cmd.Parameters.AddWithValue("@estado", usuario.Usu_Estado);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
         public Usuario obtenerUsuario_BD(String usu, String contra)
        {
            Usuario usuario = new Usuario();
            Rol oRol = new Rol();
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE USU_username=@u AND USU_password=@c", cnn);
            cmd.Parameters.AddWithValue("u", usu);
            cmd.Parameters.AddWithValue("c", contra);
               cnn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cnn.Close();

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                usuario.Usu_Id = (int)dt.Rows[0]["USU_id"];
               
                usuario.Usu_Password = dt.Rows[0]["USU_password"].ToString();
                usuario.Usu_ApellidoNombre = dt.Rows[0]["USU_apellidoNombre"].ToString();
                    usuario.Usu_Username = dt.Rows[0]["USU_username"].ToString();
                usuario.Rol_Id = (int)dt.Rows[0]["ROL_id"];
               // usuario.Rol_Id =  dt.Rows[0]["ROL_id"];
             
                return usuario;
            }
        }
            

        public static bool EliminarUsuario(int id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE Usuario SET USU_estado='Inactivo' WHERE USU_id = @id";
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
            cmd.Parameters.AddWithValue("@estado", usuario.Usu_Estado);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
