using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarProyecciones
    {
        //TRAE TODAS LAS PROYECCIONES PARA LLENAR EL COMBO PROYECCIONES DE WIN ALTA TICKETS (VENTA DE TICKETS)    
        public static DataTable TraerProyeccionesCombo()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Proyeccion.PRO_id, CASE SAL_capacidad WHEN 35 THEN '2D' ELSE '3D' END +' - '+ CONVERT(varchar(10),Proyeccion.PRO_fecha,103)+' - '+ CONVERT(varchar(5),Proyeccion.PRO_hora)+'hs. - '+ Pelicula.PEL_titulo AS InfoProyeccion" +
                                " FROM Pelicula INNER JOIN Proyeccion ON Pelicula.PEL_id = Proyeccion.PEL_id "+
                                "INNER JOIN Sala ON Sala.SAL_id=Proyeccion.SAL_id "+
                                "WHERE PRO_fecha>=@today";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@today", DateTime.Today.Date);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        //COMBO FILTRO POR SALA (2D o 3D)
        public static DataTable TraerProyeccionesComboFiltroSala(string sala)
        {
            int capacidad;
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Proyeccion.PRO_id, CASE SAL_capacidad WHEN 35 THEN '2D' ELSE '3D' END +' - '+ CONVERT(varchar(10),Proyeccion.PRO_fecha,103)+' - '+ CONVERT(varchar(5),Proyeccion.PRO_hora)+'hs. - '+ Pelicula.PEL_titulo AS InfoProyeccion" +
                                " FROM Pelicula INNER JOIN Proyeccion ON Pelicula.PEL_id = Proyeccion.PEL_id " +
                                "INNER JOIN Sala ON Sala.SAL_id=Proyeccion.SAL_id " +
                                "WHERE PRO_fecha>=@today AND SAL_capacidad=@capacidad";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@today", DateTime.Today.Date);
            capacidad = sala.Contains("2D")?35:25;
            cmd.Parameters.AddWithValue("@capacidad", capacidad);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        //COMBO FILTRO POR FECHA DE PROYECCION
        public static DataTable TraerProyeccionesComboFiltroFecha(DateTime? fechaSeleccionada)
        {            
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Proyeccion.PRO_id, CASE SAL_capacidad WHEN 35 THEN '2D' ELSE '3D' END +' - '+ CONVERT(varchar(10),Proyeccion.PRO_fecha,103)+' - '+ CONVERT(varchar(5),Proyeccion.PRO_hora)+'hs. - '+ Pelicula.PEL_titulo AS InfoProyeccion" +
                                " FROM Pelicula INNER JOIN Proyeccion ON Pelicula.PEL_id = Proyeccion.PEL_id " +
                                "INNER JOIN Sala ON Sala.SAL_id=Proyeccion.SAL_id " +
                                "WHERE PRO_fecha=@fecha";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@fecha", ((DateTime)fechaSeleccionada).Date);
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        //TRAE el PRECIO de una PROYECCION SEGUN IDPRoyeccion DE WIN ALTA TICKETS (VENTA DE TICKETS)    
        public static decimal TraerPrecioProyeccionPorID(int idProyeccion)
        {
            decimal precio=0;//default

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.cinesConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRO_precio FROM Proyeccion WHERE PRO_id=@id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@id", idProyeccion);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                precio = (decimal)reader["PRO_precio"];                  
            }
            cnn.Close();

            return precio;
        }
    }
}
