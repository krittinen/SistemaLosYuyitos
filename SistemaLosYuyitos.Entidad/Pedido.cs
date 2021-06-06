using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class Pedido
    {
        public string CodigoBarra { get; set; }
        public int NumeroOrden { get; set; }
        public int Cantidad { get; set; }
        public int SubTotal { get; set; }
    }
}
