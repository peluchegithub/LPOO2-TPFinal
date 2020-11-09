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
            cmd.CommandText = "SELECT Proyeccion.PRO_id, CONVERT(varchar(10),Proyeccion.PRO_fecha,103)+' - '+ CONVERT(varchar(5),Proyeccion.PRO_hora)+'hs. - '+ Pelicula.PEL_titulo AS InfoProyeccion" +
                                " FROM Pelicula INNER JOIN Proyeccion ON Pelicula.PEL_id = Proyeccion.PEL_id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}
