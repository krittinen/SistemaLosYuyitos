using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class Boleta
    {
        public Decimal NroBoleta { get { return NroBoleta; } set { if (value > 0) NroBoleta = value; } }
        public DateTime FechaVenta { get; set; }
        public Decimal TotalBoleta { get { return TotalBoleta; } set { if (value > 0) TotalBoleta = value; } }
        public bool BoletaFiada { get; set; }
        public List<Producto> Productos { get; set; }
        public Usuario UsuarioVendedor { get; set; }
        public bool Anulada { get; set; }

    }
}
