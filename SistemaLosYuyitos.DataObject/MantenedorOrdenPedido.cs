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
    public class MantenedorOrdenPedido
    {
        DataAccess.DataAccess da;

        //crear orden pedido
        public bool Create(Orden orden)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into orden(num_orden, fecha_orden, total_orden, fecha_recepcion, recibida, anulada, id_usuario)
                                    values (seq_id_orden, :fecha_orden, :total_orden, null, 'n', 'n',:usuario)");
                da.AgregarParametro(":fecha_orden", orden.FechaOrden, DbType.Date);
                da.AgregarParametro(":total_orden", orden.TotalOrden, DbType.Int32);
                da.AgregarParametro(":fecha_recepcion", orden.FechaRecepcion, DbType.Date);
                da.AgregarParametro(":usuario", orden.IdUsuario, DbType.String);
                int resultado = da.ExecuteNonQuery();
                res = resultado >= 0;
            }
            return res;
        }

        //crear listado productos de la orden 
        public bool CreatePedido(List<Pedido> pedido)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                while (pedido.Read())
                {
                    da.GenerarComando(@"insert into pedido_producto(codigo_barra, num_orden, cant_producto, subtotal)
                                    values (:codigobarra, :numorden, :cantidad, :subtotal)");
                    da.AgregarParametro(":codigobarra", pedido.CodigoBarra, DbType.String);
                    da.AgregarParametro(":numorden", pedido.NumeroOrden, DbType.Int32);
                    da.AgregarParametro(":cantidad", pedido.cantidad, DbType.Int32);
                    da.AgregarParametro(":subtotal", pedido.SubTotal, DbType.Int32);
                    int resultado = da.ExecuteNonQuery();
                    res = resultado >= 0;
                }
            }
            return res;

        }

        

        //Buscar una orden
        public Orden Read(int id_orden)
        {
            Orden orden = new Orden();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select num_orden, fecha_orden, total_orden, fecha_recepcion, recibida, anulada, id_usuario from orden where num_orden = :orden");
                da.AgregarParametro(":orden", id_orden);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                { 
                    orden.IdOrden = Convert.ToInt32(reader["num_orden"].ToString());
                    orden.FechaOrden = reader["fecha_orden"].ToString();
                    orden.TotalOrden = Convert.ToDecimal(reader["total_orden"].ToString());
                    orden.FechaRecepcion = reader["fecha_recepcion"].ToString();
                    orden.Recibida = reader["recibida"].ToString();
                    orden.Anulada = reader["anulada"].ToString();
                    orden.IdUsuario = reader["id_usuario"].ToString();

                }
            }
            return orden;
        }


        //Buscar lista de productos de la orden a buscar
        public List<Pedido> ListarPedido(int num_orden)
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
                        NumeroOrden = Convert.ToInt32(reader["num_orden"].ToString()),
                        Cantidad = Convert.ToInt32(reader["cant_producto"].ToString()),
                        Subtotal = Convert.ToInt32(reader["subtotal"].ToString())
                    };
                    lista.Add(pedido);
                }
            }
            return lista;
        }






    }
}
