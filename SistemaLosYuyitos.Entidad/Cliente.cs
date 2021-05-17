using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class Cliente
    {
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool AutorizadoParaFiar { get; set; }
        public string Direccion { get; set; }
        public int IdComuna { get; set; }
        public int IdProvincia { get; set; }
        public int IdRegion { get; set; }
    }
}
