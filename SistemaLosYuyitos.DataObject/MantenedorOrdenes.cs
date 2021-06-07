using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaLosYuyitos.Entidad;
using SistemaLosYuyitos.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace SistemaLosYuyitos.Controlador
{
    public class MantenedorOrdenes
    {
        DataAccess.DataAccess da;

        //crear orden pedido
        public bool Create(Orden orden)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into orden(num_orden, fecha_orden, total_orden, fecha_recepcion, recibida, anulada, id_usuario)
                                    values (seq_id_orden.nextval, :fecha_orden, :total_orden, null, 'n', 'n', :usuario)
                                    returning num_orden into :nuevo_num");
                da.AgregarParametro(":fecha_orden", orden.FechaOrden, DbType.Date);
                da.AgregarParametro(":total_orden", orden.TotalOrden, DbType.Int64);
                da.AgregarParametro(":usuario", orden.IdUsuario, DbType.String);
                da.AgregarParametroOut(":nuevo_num", DbType.Int64);
                int resultado = da.ExecuteNonQuery();
                decimal nuevo_num = Convert.ToDecimal(da.GetValorParametro(":nuevo_num"));
                foreach (var item in orden.ProductosEnOrden)
                {
                    AsociarProductoOrden(item, nuevo_num);
                }
                res = resultado > 0;
            }
            return res;
        }

        //crear listado productos de la orden 
        private bool AsociarProductoOrden(Pedido pedido, decimal id_orden)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into pedido_producto(codigo_barra, num_orden, cant_producto, subtotal)
                                    values (:cod_barra, :num_orden, :cant_producto, :subtotal");
                da.AgregarParametro(":cod_barra", pedido.CodigoBarra);
                da.AgregarParametro(":num_orden", id_orden);
                da.AgregarParametro(":cant_producto", pedido.Cantidad, DbType.Int32);
                da.AgregarParametro(":subtotal", pedido.SubTotal, DbType.Int64);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }
        //Buscar una orden
        public Orden Read(decimal id_orden)
        {
            Orden orden = null;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select num_orden, fecha_orden, total_orden, fecha_recepcion, recibida, anulada, id_usuario from orden where num_orden = :orden");
                da.AgregarParametro(":orden", id_orden);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    string fecha_recepcion = reader["fecha_recepcion"].ToString();
                    orden = new Orden
                    {
                        IdOrden = Convert.ToDecimal(reader["num_orden"].ToString()),
                        FechaOrden = Convert.ToDateTime(reader["fecha_orden"].ToString()),
                        TotalOrden = Convert.ToDecimal(reader["total_orden"].ToString()),
                        FechaRecepcion = String.IsNullOrEmpty(fecha_recepcion) ? null : (DateTime?)Convert.ToDateTime(fecha_recepcion),
                        Recibida = reader["recibida"].ToString() == "y",
                        Anulada = reader["anulada"].ToString() == "y",
                        IdUsuario = reader["id_usuario"].ToString(),
                        ProductosEnOrden = ObtenerProductosEnOrden(id_orden)
                    };
                }
            }
            return orden;
        }
        //Buscar lista de productos de la orden a buscar
        private List<Pedido> ObtenerProductosEnOrden(decimal num_orden)
        {
            List<Pedido> lista = new List<Pedido>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select codigo_barra, num_orden, cant_producto, subtotal from pedido_producto
                                    where num_orden = :numorden");
                da.AgregarParametro(":numorden", num_orden);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    Pedido pedido = new Pedido
                    {
                        CodigoBarra = reader["codigo_barra"].ToString(),
                        IdOrden = Convert.ToInt32(reader["num_orden"].ToString()),
                        Cantidad = Convert.ToInt32(reader["cant_producto"].ToString()),
                        SubTotal = Convert.ToInt32(reader["subtotal"].ToString())
                    };
                    lista.Add(pedido);
                }
            }
            return lista;
        }
        public List<Orden> List()
        {
            List<Orden> lista = new List<Orden>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select num_orden, fecha_orden, total_orden, fecha_recepcion, recibida, anulada, id_usuario from orden");
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    string fecha_recepcion = reader["fecha_recepcion"].ToString();
                    decimal id_orden = Convert.ToDecimal(reader["num_orden"].ToString());
                    Orden orden = new Orden
                    {
                        IdOrden = id_orden,
                        FechaOrden = Convert.ToDateTime(reader["fecha_orden"].ToString()),
                        TotalOrden = Convert.ToDecimal(reader["total_orden"].ToString()),
                        FechaRecepcion = String.IsNullOrEmpty(fecha_recepcion) ? null : (DateTime?)Convert.ToDateTime(fecha_recepcion),
                        Recibida = reader["recibida"].ToString() == "y",
                        Anulada = reader["anulada"].ToString() == "y",
                        IdUsuario = reader["id_usuario"].ToString(),
                        ProductosEnOrden = ObtenerProductosEnOrden(id_orden)
                    };
                    lista.Add(orden);
                }
            }
            return lista;
        }
        public bool AnularOrden(int num_orden)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("update pedido_producto set anulada = 'y' where num_orden = :num_orden");
                da.AgregarParametro(":num_orden", num_orden);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }
        public bool RecibirOrden(int num_orden)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("update pedido_producto set recibida = 'y' where num_orden = :num_orden");
                da.AgregarParametro(":num_orden", num_orden);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }
    }
}
