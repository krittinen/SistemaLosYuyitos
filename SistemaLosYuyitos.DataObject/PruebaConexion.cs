using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Controlador
{
    public class PruebaConexion
    {
        DataAccess.DataAccess da;

        public bool Prueba()
        {
            using (da = new DataAccess.DataAccess())
            {
            }
            return true;
        }
    }
}
