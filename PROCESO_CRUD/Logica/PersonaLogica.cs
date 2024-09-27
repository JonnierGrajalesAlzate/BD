using PROCESO_CRUD.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PROCESO_CRUD.Logica
{
    public class PersonaLogica
    {
        private static string lcCadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
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
            bool llRespuesta = true;

            using (SQLiteConnection Oconexion = new SQLiteConnection(lcCadena))
            {
                try
                {
                    Oconexion.Open();
                    string lcQuery = "insert into Persona (nombre, sueldo) values (@nombre, @sueldo)";
                    SQLiteCommand Ocmd = new SQLiteCommand(lcQuery, Oconexion);
                    Ocmd.Parameters.Add(new SQLiteParameter("@nombre", obj.pcNombrePersona));
                    Ocmd.Parameters.Add(new SQLiteParameter("@sueldo", obj.pnSueldoPersona));
                    Ocmd.CommandType = System.Data.CommandType.Text;

                    if (Ocmd.ExecuteNonQuery() < 1)
                    {
                        llRespuesta = false;
                    }
                }
                catch (Exception ex)
                { 
                    MessageBox.Show($"Error al guardar persona: {ex.Message}");
                    llRespuesta = false;
                }
            }

            return llRespuesta;
        }

        public bool Actualizar(Persona obj)
        {
            bool llRespuesta = true;

            using (SQLiteConnection Oconexion = new SQLiteConnection(lcCadena))
            {
                try
                {
                    Oconexion.Open(); 
                    string lcQuery = "update Persona set nombre=@nombre, sueldo=@sueldo where id=@pnIdPersona";
                    SQLiteCommand Ocmd = new SQLiteCommand(lcQuery, Oconexion); 
                    Ocmd.Parameters.Add(new SQLiteParameter("@pnIdPersona", obj.pnIdPersona));
                    Ocmd.Parameters.Add(new SQLiteParameter("@nombre", obj.pcNombrePersona));
                    Ocmd.Parameters.Add(new SQLiteParameter("@sueldo", obj.pnSueldoPersona));
                    Ocmd.CommandType = System.Data.CommandType.Text; 
                    if (Ocmd.ExecuteNonQuery() < 1)
                    {
                        llRespuesta = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar persona: {ex.Message}");
                    llRespuesta = false;
                }
            }

            return llRespuesta;
        }

        public bool Eliminar(Persona obj)
        {
            bool llRespuesta = true;

            using (SQLiteConnection Oconexion = new SQLiteConnection(lcCadena))
            {
                try
                {
                    Oconexion.Open();
                    string lcQuery = "delete from Persona where id=@pnIdPersona";
                    SQLiteCommand Ocmd = new SQLiteCommand(lcQuery, Oconexion);
                    Ocmd.Parameters.Add(new SQLiteParameter("@pnIdPersona", obj.pnIdPersona)); 
                    Ocmd.CommandType = System.Data.CommandType.Text;

                    if (Ocmd.ExecuteNonQuery() < 1)
                    {
                        llRespuesta = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar persona: {ex.Message}");
                    llRespuesta = false;
                }
            }

            return llRespuesta;
        }
        public List<Persona> Listar()
        {
            List<Persona> oLista = new List<Persona>();

            using (SQLiteConnection Oconexion = new SQLiteConnection(lcCadena))
            {
                try
                {
                    Oconexion.Open();
                    string lcQuery = "SELECT id, nombre, sueldo FROM Persona";
                    SQLiteCommand Ocmd = new SQLiteCommand(lcQuery, Oconexion);
                    Ocmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = Ocmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Persona persona = new Persona(); 
                            if (dr["id"] != DBNull.Value && int.TryParse(dr["id"].ToString(), out int lnId))
                            {
                                persona.pnIdPersona = lnId;
                            }
                            else
                            {
                                persona.pnIdPersona = 0;  
                            }
                             
                            persona.pcNombrePersona = dr["nombre"] != DBNull.Value ? dr["nombre"].ToString() : string.Empty;
                             
                            if (dr["sueldo"] != DBNull.Value && double.TryParse(dr["sueldo"].ToString(), out double lnSueldo))
                            {
                                persona.pnSueldoPersona = lnSueldo;
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
                    MessageBox.Show($"Error al listar personas: {ex.Message}");
                }
            }

            return oLista;
        }
    }
}
