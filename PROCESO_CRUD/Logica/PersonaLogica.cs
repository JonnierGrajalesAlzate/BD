using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using PROCESO_CRUD.Modelo;

namespace PROCESO_CRUD.Logica
{
    public class PersonaLogica
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static PersonaLogica instancia = null; 
        private PersonaLogica() { } 
        public static PersonaLogica Instacia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PersonaLogica();
                }
                return instancia;
            }
        } 
        public bool Guardar(Persona obj)
        {
            bool respuesta = true;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = "INSERT INTO Persona (nombre, sueldo) VALUES (@nombre, @sueldo)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@nombre", obj.pcNombrePersona));
                    cmd.Parameters.Add(new SQLiteParameter("@sueldo", obj.pnSueldoPersona));
                    cmd.CommandType = System.Data.CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        respuesta = false;
                    }
                }
                catch (Exception ex)
                { 
                    Console.WriteLine($"Error al guardar persona: {ex.Message}");
                    respuesta = false;
                }
            }

            return respuesta;
        } 
        public List<Persona> Listar()
        {
            List<Persona> oLista = new List<Persona>();

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT cedula, nombre, sueldo FROM Persona";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Persona persona = new Persona(); 
                            if (dr["cedula"] != DBNull.Value && int.TryParse(dr["cedula"].ToString(), out int cedula))
                            {
                                persona.pnCedulaPersona = cedula;
                            }
                            else
                            {
                                persona.pnCedulaPersona = 0;  
                            }
                             
                            persona.pcNombrePersona = dr["nombre"] != DBNull.Value ? dr["nombre"].ToString() : string.Empty;
                             
                            if (dr["sueldo"] != DBNull.Value && double.TryParse(dr["sueldo"].ToString(), out double sueldo))
                            {
                                persona.pnSueldoPersona = sueldo;
                            }
                            else
                            {
                                persona.pnSueldoPersona = 0.0;  
                            }

                            oLista.Add(persona);
                        }
                    }
                }
                catch (Exception ex)
                { 
                    Console.WriteLine($"Error al listar personas: {ex.Message}");
                }
            }

            return oLista;
        }
    }
}
