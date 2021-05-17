using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class Fiado
    {
        public Decimal IdFiado { get { return IdFiado; } set { if (value > 0) IdFiado = value; } }
        public DateTime FechaFiado { get; set; }
        public DateTime FechaVencimiento { get { return FechaVencimiento; } set { if (value >= FechaFiado) FechaVencimiento = value; } }
        public Boleta Boleta { get; set; }
        public Decimal TotalAbonos { get { return TotalAbonos;} set { if (value >= 0) TotalAbonos = value; } }
        public Decimal TotalPago { get { return TotalPago; } set { if (value >= 0) TotalPago = value; } }
        public bool Vencido { get; set; }
        public Cliente ClienteFiado { get; set; }
        public List<Abono> AbonosRealizados { get; set; }

    }
}
