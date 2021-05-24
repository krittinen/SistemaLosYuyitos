using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class Producto
    {
        public string CodigoBarra { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Stock { get; set; }
        public decimal StockCritico { get; set; }
        public bool Vigencia { get; set; }
        public int IdProveedor { get; set; }
        public int IdTipo { get; set; }
        public int IdFamilia { get; set; }
    }
}
