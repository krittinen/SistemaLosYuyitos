using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SistemaLosYuyitos.Entidad;

namespace SistemaLosYuyitos.Controlador
{
    public class MantenedorProducto
    {
        DataAccess.DataAccess da;
        
        public bool Create(Producto producto)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into producto (codigo_barra, fecha_vencimiento, descripcion, precio_compra, precio_venta, stock, stock_critico, vigencia, id_tipo_producto)
                                    values (:codigo_barra, :fecha_vencimiento, :descripcion, :precio_compra, :precio_venta, :stock, :stock_critico, :vigencia, :id_tipo_producto)");
            }
            return res;
        }
        public Producto Read(decimal id_producto)
        {
            Producto producto = null;

            return producto;
        }
        public bool Update(Producto producto)
        {
            bool res = false;

            return res;
        }
        public bool Delete(Producto producto)
        {
            bool res = false;
            return res;
        }
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            return lista;
        }
        public List<Producto> ListarPorProveedor(decimal id_proveedor)
        {
            List<Producto> lista = new List<Producto>();

            return lista;
        }
    }
}
