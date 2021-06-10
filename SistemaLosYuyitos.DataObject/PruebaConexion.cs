using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLosYuyitos.Controlador
{
    public class PruebaConexion
    {
        public PruebaConexion()
        {
            DataAccess.DataAccess da = new DataAccess.DataAccess();
        }

        public bool Prueba()
        {
            return true;
        }
    }
}
