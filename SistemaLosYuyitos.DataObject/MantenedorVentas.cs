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
    public class MantenedorVentas
    {
        DataAccess.DataAccess da;

        public bool Create(Boleta boleta)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into boleta(nro_boleta, fecha_venta, total_boleta, es_fiado, USUARIO_id_usuario, anulada)
                                    values (:nro_boleta, sysdate, :total_boleta, :es_fiado, :usuario, null)");
                da.AgregarParametro(":nro_boleta", boleta.NroBoleta, DbType.String);
                da.AgregarParametro(":total_boleta", boleta.TotalBoleta, DbType.Int32);
                da.AgregarParametro(":es_fiado", boleta.BoletaFiada, DbType.String);
                da.AgregarParametro(":usuario", boleta.UsuarioVendedor, DbType.String);
                int resultado = da.ExecuteNonQuery();
                res = resultado >= 0;




            }
            return res;
        }
        public Boleta Read(string nro_boleta)
        {
            Boleta boleta = new Boleta();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select nro_boleta, fecha_venta, total_boleta, es_fiado, USUARIO_id_usuario from boleta where nro_boleta = :nro_boleta");
                da.AgregarParametro(":nro_boleta", nro_boleta);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    /*
                    boleta.NroBoleta = reader["nro_boleta"].ToString();
                    boleta.FechaVenta = reader["fecha_venta"].ToString();
                    boleta.TotalBoleta = reader["total_boleta"].ToString();
                    boleta.BoletaFiada = reader["es_fiado"].ToString();
                    boleta.UsuarioVendedor = reader["USUARIO_id_usuario"].ToString();
                    */
                }
            }
            return boleta;
        }

        public bool Anular(string nro_boleta)
{
            bool res = false;
            using (da = new DataAccess.DataAccess())
	{
                da.GenerarComando("update boleta set anulada = 'y' where nro_boleta = :nro_boleta");
                da.AgregarParametro(":nro_boleta", nro_boleta);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }

	}
}
