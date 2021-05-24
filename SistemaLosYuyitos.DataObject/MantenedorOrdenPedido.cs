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
        //hablar en reunion
        public bool Create(Orden orden)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into orden_pedido(id_orden, fecha_orden, total_orden, fecha_recepcion, recibida, USUARIO_id_usuario)
                                    values (:id_orden, :fecha_orden, :total_orden, null, 'n', :usuario)");
                da.AgregarParametro(":id_orden", orden.IdOrden, DbType.String);
                da.AgregarParametro(":fecha_orden", orden.FechaOrden, DbType.Date);
                da.AgregarParametro(":total_orden", orden.TotalOrden, DbType.int);
                da.AgregarParametro(":usuario", orden.Usuario, DbType.String);
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
                da.GenerarComando("select id_orden, fecha_orden, total_orden, USUARIO_id_usuario from orden_pedido where id_orden = :orden");
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
