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
                da.GenerarComando(@"insert into producto (codigo_barra, fecha_vencimiento, descripcion, precio_compra, precio_venta, stock, stock_critico, vigencia, id_tipo_producto, id_proveedor)
                                    values (:codigo_barra, :fecha_vencimiento, :descripcion, :precio_compra, :precio_venta, :stock, :stock_critico, :vigencia, :id_tipo_producto, :id_proveedor)");
                da.AgregarParametro(":codigo_barra", producto.CodigoBarra);
                da.AgregarParametro(":fecha_vencimiento", producto.FechaVencimiento);
                da.AgregarParametro(":descripcion", producto.Descripcion);
                da.AgregarParametro(":precio_compra", producto.PrecioCompra, DbType.Int32);
                da.AgregarParametro(":precio_venta", producto.PrecioVenta, DbType.Int32);
                da.AgregarParametro(":stock", producto.Stock, DbType.Int32);
                da.AgregarParametro(":stock_critico", producto.StockCritico, DbType.Int32);
                da.AgregarParametro(":vigencia", producto.Vigencia ? "y" : "n");
                da.AgregarParametro(":id_tipo_producto", producto.IdTipo, DbType.Int32);
                da.AgregarParametro(":id_proveedor", producto.IdProveedor, DbType.Int32);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }
        public Producto Read(string codigo_barra)
        {
            Producto producto = null;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select codigo_barra, fecha_vencimiento, descripcion, precio_compra, precio_venta, stock, stock_critico, vigencia, pr.id_tipo_producto, fpr.id_familia, id_proveedor
                                    from producto pr
                                    inner join tipo_producto tpr on (pr.id_tipo_producto = tpr.id_tipo_producto)
                                    inner join familia_producto fpr on (tpr.id_familia = fpr.id_familia)
                                    where codigo_barra = :cod");
                da.AgregarParametro(":cod", codigo_barra);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    string fecha_vencimiento = reader["fecha_vencimiento"].ToString();
                    producto = new Producto
                    {
                        CodigoBarra = reader["codigo_barra"].ToString(),
                        FechaVencimiento = String.IsNullOrEmpty(fecha_vencimiento) ? null : (DateTime?)Convert.ToDateTime(fecha_vencimiento),
                        Descripcion = reader["descripcion"].ToString(),
                        PrecioCompra = Convert.ToInt32(reader["precio_compra"].ToString()),
                        PrecioVenta = Convert.ToInt32(reader["precio_venta"].ToString()),
                        Stock = Convert.ToInt32(reader["stock"].ToString()),
                        StockCritico = Convert.ToInt32(reader["stock_critico"].ToString()),
                        Vigencia = reader["vigencia"].ToString() == "y",
                        IdTipo = Convert.ToInt32(reader["id_tipo_producto"].ToString()),
                        IdFamilia = Convert.ToInt32(reader["id_familia"].ToString()),
                        IdProveedor = Convert.ToInt32(reader["id_proveedor"].ToString())
                    };
                }
            }
            return producto;
        }
        public bool Update(Producto producto)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"update producto set 
                                        fecha_vencimiento = :fecha_vencimiento,
                                        descripcion = :descripcion,
                                        precio_compra = :precio_compra,
                                        precio_venta = :precio_venta,
                                        stock = :stock,
                                        stock_critico = :stock_critico,
                                        vigencia = :vigencia,
                                        id_tipo_producto = :id_tipo_producto,
                                        id_proveedor = :id_proveedor
                                    where codigo_barra = :codigo_barra");
                da.AgregarParametro(":fecha_vencimiento", producto.FechaVencimiento);
                da.AgregarParametro(":descripcion", producto.Descripcion);
                da.AgregarParametro(":precio_compra", producto.PrecioCompra, DbType.Int32);
                da.AgregarParametro(":precio_venta", producto.PrecioVenta, DbType.Int32);
                da.AgregarParametro(":stock", producto.Stock, DbType.Int32);
                da.AgregarParametro(":stock_critico", producto.StockCritico, DbType.Int32);
                da.AgregarParametro(":vigencia", producto.Vigencia ? "y" : "n");
                da.AgregarParametro(":id_tipo_producto", producto.IdTipo, DbType.Int32);
                da.AgregarParametro(":id_proveedor", producto.IdProveedor, DbType.Int32);
                da.AgregarParametro(":codigo_barra", producto.CodigoBarra);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }
        public bool Delete(string codigo_barra)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("delete from producto where codigo_barra = :cod");
                da.AgregarParametro(":cod", codigo_barra);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select codigo_barra, fecha_vencimiento, descripcion, precio_compra, precio_venta, stock, stock_critico, vigencia, pr.id_tipo_producto, fpr.id_familia, id_proveedor
                                    from producto pr
                                    inner join tipo_producto tpr on (pr.id_tipo_producto = tpr.id_tipo_producto)
                                    inner join familia_producto fpr on (tpr.id_familia = fpr.id_familia)");
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    string fecha_vencimiento = reader["fecha_vencimiento"].ToString();
                    Producto producto = new Producto
                    {
                        CodigoBarra = reader["codigo_barra"].ToString(),
                        FechaVencimiento = String.IsNullOrEmpty(fecha_vencimiento) ? null : (DateTime?)Convert.ToDateTime(fecha_vencimiento),
                        Descripcion = reader["descripcion"].ToString(),
                        PrecioCompra = Convert.ToInt32(reader["precio_compra"].ToString()),
                        PrecioVenta = Convert.ToInt32(reader["precio_venta"].ToString()),
                        Stock = Convert.ToInt32(reader["stock"].ToString()),
                        StockCritico = Convert.ToInt32(reader["stock_critico"].ToString()),
                        Vigencia = reader["vigencia"].ToString() == "y",
                        IdTipo = Convert.ToInt32(reader["id_tipo_producto"].ToString()),
                        IdFamilia = Convert.ToInt32(reader["id_familia"].ToString()),
                        IdProveedor = Convert.ToInt32(reader["id_proveedor"].ToString())
                    };
                    lista.Add(producto);
                }
            }
            return lista;
        }
        public List<Producto> ListarPorProveedor(decimal id_proveedor)
        {
            List<Producto> lista = new List<Producto>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select codigo_barra, fecha_vencimiento, descripcion, precio_compra, precio_venta, stock, stock_critico, vigencia, pr.id_tipo_producto, fpr.id_familia, id_proveedor
                                    from producto pr
                                    inner join tipo_producto tpr on (pr.id_tipo_producto = tpr.id_tipo_producto)
                                    inner join familia_producto fpr on (tpr.id_familia = fpr.id_familia)
                                    where id_proveedor = :id_proveedor");
                da.AgregarParametro(":id_proveedor", id_proveedor, DbType.Int32);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    string fecha_vencimiento = reader["fecha_vencimiento"].ToString();
                    Producto producto = new Producto
                    {
                        CodigoBarra = reader["codigo_barra"].ToString(),
                        FechaVencimiento = String.IsNullOrEmpty(fecha_vencimiento) ? null : (DateTime?)Convert.ToDateTime(fecha_vencimiento),
                        Descripcion = reader["descripcion"].ToString(),
                        PrecioCompra = Convert.ToInt32(reader["precio_compra"].ToString()),
                        PrecioVenta = Convert.ToInt32(reader["precio_venta"].ToString()),
                        Stock = Convert.ToInt32(reader["stock"].ToString()),
                        StockCritico = Convert.ToInt32(reader["stock_critico"].ToString()),
                        Vigencia = reader["vigencia"].ToString() == "y",
                        IdTipo = Convert.ToInt32(reader["id_tipo_producto"].ToString()),
                        IdFamilia = Convert.ToInt32(reader["id_familia"].ToString()),
                        IdProveedor = Convert.ToInt32(reader["id_proveedor"].ToString())
                    };
                    lista.Add(producto);
                }
            }
            return lista;
        }
    }
}
