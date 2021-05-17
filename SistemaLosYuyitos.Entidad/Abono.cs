using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Entidad
{
    public class Abono
    {
        public Decimal IdAbono { get; set; }
        public Decimal MontoAbono
        {
            get
            {
                return MontoAbono;
            }
            set
            {
                if (value > 0)
                {
                    MontoAbono = value;
                }
            }
        }
        public DateTime FechaAbono { get; set; }

    }
}
