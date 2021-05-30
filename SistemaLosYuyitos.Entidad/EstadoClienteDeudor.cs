using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class EstadoClienteDeudor
    {
        public Cliente ClienteDeudor { get; set; }
        public Usuario UsuarioAutorizador { get; set; }
        public DateTime FechaAutorizado { get; set; }
        public DateTime? FechaBloqueo { get; set; }
        public Usuario UsuarioBloqueo { get; set; }

    }
}
