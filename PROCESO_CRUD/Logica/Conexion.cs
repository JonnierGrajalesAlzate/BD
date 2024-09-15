using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PROCESO_CRUD.Logica
{
    public class Conexion
    {
        private string BaseDatos;
        private static Conexion Con = null;

        private Conexion()
        {
            this.BaseDatos = "./mydatabse.db";
        }
        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Data Source"+ this.BaseDatos;
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }
        public static Conexion getInstacia()
        {
            if (Con==null)
            {
                Con = new Conexion();
            }
            return Con;
        }
    }
}
