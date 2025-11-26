using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace S4_Lab1_RegistroDeEstudiantes
{
    internal static class conexionDB
    {
        // Cadena de conexión a la base de datos SQL Server
        public static readonly string connectionString =
            "Server=.\\SQLEXPRESS;Database=DBAlumnos;Trusted_Connection=True;TrustServerCertificate=True;";

        // Crear y devolver una nueva conexión
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }

        //Verificar la conexión a la base de datos
        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true; // Se conectó correctamente
                }
            }
            catch
            {
                return false; // Hubo un error
            }
        }
    }
}
