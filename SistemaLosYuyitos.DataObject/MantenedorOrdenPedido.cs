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

        public List<string> llenarListProd()
        {
            List<string> list = new List<string>();
            //Region region = new Region();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select codigo_barra, descripcion, precio_compra from producto");
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["codigo_barra"].ToString(), reader["descripcion"].ToString(), reader["precio_compra"].ToString());

                }
            }
            return list;

        }

        //crear orden pedido
        public bool Create(Orden orden)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into orden(num_orden, fecha_orden, total_orden, fecha_recepcion, recibida, anulada, id_usuario)
                                    values (:id_orden, :fecha_orden, :total_orden, null, 'n', 'n',:usuario)");
                da.AgregarParametro(":id_orden", orden.IdOrden, DbType.String);
                da.AgregarParametro(":fecha_orden", orden.FechaOrden, DbType.Date);
                da.AgregarParametro(":total_orden", orden.TotalOrden, DbType.Int32);
                da.AgregarParametro(":fecha_recepcion", orden.FechaRecepcion, DbType.Date);
                da.AgregarParametro(":usuario", orden.IdUsuario, DbType.String);
                int resultado = da.ExecuteNonQuery();
                res = resultado >= 0;
            }
            return res;
        }

        


        public Orden Read(string id_orden)
        {
            Orden orden = new Orden();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select num_orden, fecha_orden, total_orden, fecha_recepcion, recibida, anulada, id_usuario from orden where num_orden = :orden");
                da.AgregarParametro(":orden", id_orden);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    
                    orden.IdOrden = reader["id_orden"].ToString();
                    orden.FechaOrden = reader["fecha_orden"].ToString();
                    orden.TotalOrden = reader["total_orden"].ToInt;
                    orden.Recibida = reader["USUARIO_id_usuario"].ToString();
                    orden.FechaRecepcion
                   
                }
            }
            return orden;
        }

        

       

        
    }
}
