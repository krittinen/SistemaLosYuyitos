using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class Orden
    {
        public decimal IdOrden { get { return IdOrden; } set { if (value >= 0) IdOrden = value; } }
        public DateTime FechaOrden { get; set; }
        public decimal TotalOrden { get { return TotalOrden; } set { if (value >= 0) TotalOrden = value; } }
        public bool Recibida { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public Usuario Usuario { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}
