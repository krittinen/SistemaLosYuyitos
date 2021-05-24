using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string HashContraseña { get; set; }
        public bool Administrador { get; set; }
        public bool Vigente { get; set; }
        public DateTime UltimoCambioContraseña { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
