using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4_Lab1_RegistroDeEstudiantes
{
    internal class Carreras
    {
        public string universidad { get; set; }
        public List<string> carreras_unificadas { get; set; }
    }

    internal class Estudiante
    {
        public string nombre { get; set; }
        public string cedula { get; set; }
        public string carrera { get; set; }
        public int semestre { get; set; }
        public string jornada { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
    }

    internal class ListaDeEstudiantes
    {
        public List<Estudiante> estudiantes { get; set; }
    }
}
