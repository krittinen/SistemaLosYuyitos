using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SistemaLosYuyitos.Entidad;

namespace SistemaLosYuyitos.Controlador
{
    public class MantenedorFiadosAbonos
    {
        DataAccess.DataAccess da;
        MantenedorClientes mantenedorClientes = new MantenedorClientes();
        //MantenedorBoletas mantenedorBoletas = new MantenedorBoletas();
        public bool Create(Fiado fiado)
        {
            bool resFiado = false;
            using (da = new DataAccess.DataAccess())
            {
                decimal total_abono = fiado.AbonosRealizados.Sum(x => x.MontoAbono);
                da.GenerarComando(@"insert into fiado (id_fiado, nro_boleta, rut_cliente, fecha_fiado, fecha_vencimiento, total_abonos, total_pago, esta_vencido)
                                    values (seq_id_fiado.nextval, :nro_boleta, :rut_cliente, :fecha_fiado, :fecha_vencimiento, :total_abono, :total_pago, :vencido)
                                    returning id_fiado into :nuevo_id");
                da.AgregarParametro(":nro_boleta", fiado.Boleta.NroBoleta, DbType.Int32);
                da.AgregarParametro(":rut_cliente", fiado.ClienteFiado.RutCliente);
                da.AgregarParametro(":fecha_fiado", fiado.FechaFiado, DbType.Date);
                da.AgregarParametro(":fecha_vencimiento", fiado.FechaVencimiento, DbType.Date);
                da.AgregarParametro(":total_abono", total_abono, DbType.Int32);
                da.AgregarParametro(":total_pago", fiado.TotalPago, DbType.Int32);
                da.AgregarParametro(":vencido", fiado.Vencido ? "y" : "n");
                da.AgregarParametroOut(":nuevo_id", DbType.Decimal);
                resFiado = da.ExecuteNonQuery() > 0;
                decimal nuevo_id = Convert.ToDecimal(da.GetValorParametro(":nuevo_id"));
                if (nuevo_id > 0 && fiado.AbonosRealizados.Count > 0)
                {
                    foreach (Abono abono in fiado.AbonosRealizados)
                    {
                        Abonar(abono, nuevo_id);
                    }
                }
            }
            return resFiado;
        }

        public Fiado Read(decimal id_fiado)
        {
            Fiado fiado = null;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_fiado, nro_boleta, rut_cliente, fecha_fiado, fecha_vencimiento, total_abonos, total_pago, esta_vencido from fiado where id_fiado = :id_fiado");
                da.AgregarParametro(":id_fiado", id_fiado);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    decimal nro_boleta = Convert.ToDecimal(reader["nro_boleta"].ToString());
                    string rut_cliente = reader["rut_cliente"].ToString();
                    fiado = new Fiado
                    {
                        IdFiado = Convert.ToDecimal(reader["id_fiado"].ToString()),
                        FechaFiado = Convert.ToDateTime(reader["fecha_fiado"].ToString()),
                        FechaVencimiento = Convert.ToDateTime(reader["fecha_vencimiento"].ToString()),
                        //Boleta = mantenedorBoletas.Read(nro_boleta),
                        TotalAbonos = Convert.ToDecimal(reader["total_abonos"].ToString()),
                        TotalPago = Convert.ToDecimal(reader["total_pago"].ToString()),
                        Vencido = reader["esta_vencido"].ToString() == "y",
                        ClienteFiado = mantenedorClientes.Read(rut_cliente),
                        AbonosRealizados = ListarAbonos(id_fiado)
                    };
                }
            }
            return fiado;
        }
        public bool Abonar(Abono abono, decimal id_fiado)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into abono_fiado (id_fiado, id_abono, monto_abono, fecha_abono)
                                                values (:id_fiado, seq.id_abono.nextval, :monto, :fecha_abono");
                da.AgregarParametro(":id_fiado", id_fiado);
                da.AgregarParametro(":monto", abono.MontoAbono);
                da.AgregarParametro(":fecha_abono", abono.FechaAbono);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }
        public List<Fiado> ListarFiados()
        {
            List<Fiado> lista = new List<Fiado>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_fiado, nro_boleta, rut_cliente, fecha_fiado, fecha_vencimiento, total_abonos, total_pago, esta_vencido from fiado");
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    decimal id_fiado = Convert.ToDecimal(reader["id_fiado"].ToString());
                    decimal nro_boleta = Convert.ToDecimal(reader["nro_boleta"].ToString());
                    string rut_cliente = reader["rut_cliente"].ToString();
                    lista.Add(new Fiado
                    {
                        IdFiado = id_fiado,
                        FechaFiado = Convert.ToDateTime(reader["fecha_fiado"].ToString()),
                        FechaVencimiento = Convert.ToDateTime(reader["fecha_vencimiento"].ToString()),
                        //Boleta = mantenedorBoletas.Read(nro_boleta),
                        TotalAbonos = Convert.ToDecimal(reader["total_abonos"].ToString()),
                        TotalPago = Convert.ToDecimal(reader["total_pago"].ToString()),
                        Vencido = reader["esta_vencido"].ToString() == "y",
                        ClienteFiado = mantenedorClientes.Read(rut_cliente),
                        AbonosRealizados = ListarAbonos(id_fiado)
                    });
                }
            }
            return lista;
        }
        public List<Fiado> ListarFiadosPorCliente(string rut)
        {
            List<Fiado> lista = new List<Fiado>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_fiado, nro_boleta, rut_cliente, fecha_fiado, fecha_vencimiento, total_abonos, total_pago, esta_vencido from fiado where rut_cliente = :rut");
                da.AgregarParametro(":rut", rut);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    decimal id_fiado = Convert.ToDecimal(reader["id_fiado"].ToString());
                    decimal nro_boleta = Convert.ToDecimal(reader["nro_boleta"].ToString());
                    string rut_cliente = reader["rut_cliente"].ToString();
                    lista.Add(new Fiado
                    {
                        IdFiado = id_fiado,
                        FechaFiado = Convert.ToDateTime(reader["fecha_fiado"].ToString()),
                        FechaVencimiento = Convert.ToDateTime(reader["fecha_vencimiento"].ToString()),
                        //Boleta = mantenedorBoletas.Read(nro_boleta),
                        TotalAbonos = Convert.ToDecimal(reader["total_abonos"].ToString()),
                        TotalPago = Convert.ToDecimal(reader["total_pago"].ToString()),
                        Vencido = reader["esta_vencido"].ToString() == "y",
                        ClienteFiado = mantenedorClientes.Read(rut_cliente),
                        AbonosRealizados = ListarAbonos(id_fiado)
                    });
                }
            }
            return lista;
        }
        public List<Abono> ListarAbonos(decimal id_fiado)
        {
            List<Abono> lista = new List<Abono>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_fiado, id_abono, monto_abono, fecha_abono from abono_fiado where id_fiado = :id_fiado");
                da.AgregarParametro(":id_fiado", id_fiado);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Abono
                    {
                        IdFiado = Convert.ToDecimal(reader["id_fiado"].ToString()),
                        IdAbono = Convert.ToDecimal(reader["id_abono"].ToString()),
                        FechaAbono = Convert.ToDateTime(reader["monto_abono"].ToString()),
                        MontoAbono = Convert.ToDecimal(reader["fecha_abono"].ToString())
                    });
                }
            }
            return lista;
        }
        public List<Abono> ListarAbonosPorCliente(string rut)
        {
            List<Abono> lista = new List<Abono>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select ab.id_fiado, ab.id_abono, ab.monto_abono, ab.fecha_abono
                                    from abono_fiado ab
                                    inner join fiado fi on (fi.id_fiado = ab.id_fiado)
                                    where fi.rut_cliente = :rut");
                da.AgregarParametro(":rut", rut);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Abono
                    {
                        IdFiado = Convert.ToDecimal(reader["id_fiado"].ToString()),
                        IdAbono = Convert.ToDecimal(reader["id_abono"].ToString()),
                        FechaAbono = Convert.ToDateTime(reader["monto_abono"].ToString()),
                        MontoAbono = Convert.ToDecimal(reader["fecha_abono"].ToString())
                    });
                }
            }
            return lista;
        }
    }
}
