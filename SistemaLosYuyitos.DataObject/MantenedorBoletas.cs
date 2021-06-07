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
    public class MantenedorBoletas
    {
        DataAccess.DataAccess da;
        MantenedorProducto mantenedorProductos = new MantenedorProducto();
        MantenedorUsuarios mantenedorUsuarios = new MantenedorUsuarios();
        public bool Create(Boleta boleta)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into boleta(nro_boleta, fecha_venta, total_boleta, es_fiado, id_usuario, anulada)
                                    values (seq_nro_boleta.nextval, sysdate, :total_boleta, :es_fiado, :usuario, 'n')");
                da.AgregarParametro(":total_boleta", boleta.TotalBoleta, DbType.Int32);
                da.AgregarParametro(":es_fiado", boleta.BoletaFiada, DbType.String);
                da.AgregarParametro(":usuario", boleta.UsuarioVendedor, DbType.String);
                int resultado = da.ExecuteNonQuery();
                res = resultado >= 0;
                foreach(string cod_producto in boleta.Productos.Select(x => x.CodigoBarra))
                {
                    AsignarProductoABoleta(cod_producto, boleta.NroBoleta);
                }
            }
            return res;
        }

        private bool AsignarProductoABoleta(string codigo_barra, decimal nro_boleta)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("@insert into boleta_producto(nro_boleta, codigo_barra) values (:nro_boleta, :cod_barra)");
                da.AgregarParametro(":nro_boleta", nro_boleta, DbType.Int64);
                da.AgregarParametro(":cod_barra", codigo_barra);
                res = da.ExecuteNonQuery() >= 0;
            }
            return res;
        }
        public Boleta Read(decimal nro_boleta)
        {
            Boleta boleta = null;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select nro_boleta, fecha_venta, total_boleta, es_fiado, id_usuario, anulada from boleta where nro_boleta = :nro_boleta");
                da.AgregarParametro(":nro_boleta", nro_boleta);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    string fecha_venta = reader["fecha_venta"].ToString();
                    boleta = new Boleta
                    {
                        NroBoleta = Convert.ToDecimal(reader["nro_boleta"].ToString()),
                        FechaVenta = Convert.ToDateTime(fecha_venta),
                        TotalBoleta = Convert.ToDecimal(reader["total_boleta"].ToString()),
                        BoletaFiada = reader["es_fiado"].ToString() == "y",
                        Anulada = reader["anulada"].ToString() == "y",
                        UsuarioVendedor = mantenedorUsuarios.Read(reader["id_usuario"].ToString()),
                        Productos = ObtenerProductosDeBoleta(nro_boleta).Select(cod_producto => mantenedorProductos.Read(cod_producto)).ToList()
                    };
                }
            }
            return boleta;
        }
        private List<string> ObtenerProductosDeBoleta(string nro_boleta)
        {
            List<string> lista = new List<string>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select codigo_barra from boleta_producto where nro_boleta = :nro_boleta");
                da.AgregarParametro(":nro_boleta", nro_boleta);
                var reader = da.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(reader["codigo_barra"].ToString());
                }
            }
            return lista;
        }
        public bool Anular(decimal nro_boleta)
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
