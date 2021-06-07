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
        MantenedorUsuarios mantenedorUsuarios = new MantenedorUsuarios();
        MantenedorClientes mantenedorClientes = new MantenedorClientes();
        MantenedorBoletas mantenedorBoletas = new MantenedorBoletas();
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
                        Boleta = mantenedorBoletas.Read(nro_boleta.ToString()),
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
                        Boleta = mantenedorBoletas.Read(nro_boleta.ToString()),
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
                        Boleta = mantenedorBoletas.Read(nro_boleta.ToString()),
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
        public bool AutorizarClienteDeudor(string rut_cliente, string id_usuario)
        {
            return CambiarEstadoClienteDeudor(rut_cliente, id_usuario, nuevo_estado: true);
        }
        public bool RevocarAutorizacionClienteDeudor(string rut_cliente, string id_usuario)
        {
            return CambiarEstadoClienteDeudor(rut_cliente, id_usuario, nuevo_estado: false);
        }
        private bool CambiarEstadoClienteDeudor(string rut_cliente, string id_usuario, bool nuevo_estado)
        {
            bool res = false;
            EstadoClienteDeudor estadoActual = ObtenerEstadoClienteDeudor(rut_cliente);

            #region Caso Autorizar
            if (nuevo_estado)
            {
                if (estadoActual == null) // Cliente no se encuentra registrado como deudor, se autoriza para deudas
                {
                    using (da = new DataAccess.DataAccess())
                    {
                        da.GenerarComando(@"insert into cliente_fiado(rut_cliente, usuario_autorizador, fecha_autorizacion)
                                            values (:rut, :usuario, :fecha)");
                        da.AgregarParametro(":rut", rut_cliente);
                        da.AgregarParametro(":usuario", id_usuario);
                        da.AgregarParametro(":fecha", DateTime.Now, DbType.Date);
                        res = da.ExecuteNonQuery() > 0;
                    }
                }
                else if (estadoActual.FechaBloqueo == null) // Cliente ya se encuentra registrado como deudor habilitado
                {
                    res = true;
                }
                else // Cliente se encuentra registrado como deudor no habilitado, no es posible desbloquear
                {
                    res = false;
                }
            }
            #endregion

            #region Caso Revocar
            else
            {
                if (estadoActual == null) // Cliente no se encuentra registrado como deudor, no se puede bloquear en forma preventiva
                {
                    res = false;
                }
                else if (estadoActual.FechaBloqueo != null) // Cliente se encuentra registrado como deudor habilitado, se revoca autorizacion
                {
                    using (da = new DataAccess.DataAccess())
                    {
                        da.GenerarComando(@"update cliente_fiado set usuario_bloqueador = :usuario, fecha_bloqueo = :fecha where rut_cliente = :rut)");
                        da.AgregarParametro(":usuario", id_usuario);
                        da.AgregarParametro(":fecha", DateTime.Now, DbType.Date);
                        da.AgregarParametro(":rut", rut_cliente);
                        res = da.ExecuteNonQuery() > 0;
                    }
                }
                else // Cliente se encuentra registrado como deudor no habilitado, ya fue bloqueado
                {
                    res = true;
                }
            }
            #endregion

            return res;
        }
        public EstadoClienteDeudor ObtenerEstadoClienteDeudor(string rut_cliente)
        {
            EstadoClienteDeudor estado = null;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select rut_cliente, usuario_autorizador, fecha_autorizacion, usuario_bloqueador, fecha_bloqueo from cliente_fiado where rut_cliente = :rut");
                da.AgregarParametro(":rut", rut_cliente);
                var reader = da.ExecuteReader();
                while (reader.Read())
                {
                    string fecha_bloqueo = reader["fecha_bloqueo"].ToString();
                    string usuario_bloqueo = reader["usuario_bloqueador"].ToString();
                    estado = new EstadoClienteDeudor
                    {
                        ClienteDeudor = mantenedorClientes.Read(reader["rut_cliente"].ToString()),
                        UsuarioAutorizador = mantenedorUsuarios.Read(reader["usuario_autorizador"].ToString()),
                        FechaAutorizado = Convert.ToDateTime(reader["fecha_autorizacion"].ToString()),
                        UsuarioBloqueo = String.IsNullOrEmpty(usuario_bloqueo) ? null : mantenedorUsuarios.Read(usuario_bloqueo),
                        FechaBloqueo = String.IsNullOrEmpty(fecha_bloqueo) ? null : (DateTime?)Convert.ToDateTime(fecha_bloqueo)
                    };
                }
            }
            return estado;
        }
    }
}
