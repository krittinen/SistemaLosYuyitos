using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaLosYuyitos.DataAccess;
using SistemaLosYuyitos.Entidad;
using System.Data;

namespace SistemaLosYuyitos.Controlador
{
    public class MantenedorClientes
    {
        DataAccess.DataAccess da;
        MantenedorUsuarios mantenedorUsuarios = new MantenedorUsuarios();
        public bool Create(Cliente cliente)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into cliente(rut_cliente, nombre_cliente, telefono, correo, direccion, autorizado_para_fiar, id_comuna)
                                    values (:rut, :nombre, :telefono, :correo, :direccion, :autorizado, :comuna)");
                da.AgregarParametro(":rut", cliente.RutCliente);
                da.AgregarParametro(":nombre_cliente", cliente.NombreCliente);
                da.AgregarParametro(":telefono", cliente.Telefono);
                da.AgregarParametro(":correo", cliente.Correo);
                da.AgregarParametro(":direccion", cliente.Direccion);
                da.AgregarParametro(":autorizado", cliente.AutorizadoParaFiar ? "y" : "n");
                da.AgregarParametro(":comuna", cliente.IdComuna, DbType.Int32);
                int resultado = da.ExecuteNonQuery();
                res = resultado > 0;
            }
            return res;
        }

        public Cliente Read(string rut)
        {
            Cliente cliente = null;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select rut_cliente, nombre_cliente, telefono, correo, direccion, autorizado_para_fiar, cl.id_comuna, pr.id_provincia, re.id_region
                                    from cliente cl
                                    inner join comuna co on (cl.id_comuna = co.id_comuna)
                                    inner join provincia pr on (co.id_provincia = pr.id_provincia)
                                    inner join region re on (pr.id_region = re.id_region)
                                    where rut_cliente = :rut");
                da.AgregarParametro(":rut", rut);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    cliente = new Cliente
                    {
                        RutCliente = reader["rut_cliente"].ToString(),
                        NombreCliente = reader["nombre_cliente"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Correo = reader["correo"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        AutorizadoParaFiar = reader["autorizado_para_fiar"].ToString() == "y",
                        IdComuna = Convert.ToInt32(reader["id_comuna"].ToString()),
                        IdProvincia = Convert.ToInt32(reader["id_provincia"].ToString()),
                        IdRegion = Convert.ToInt32(reader["id_region"].ToString())
                    };
                }
            }
            return cliente;
        }

        public bool Update(Cliente cliente)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("update cliente set nombre_cliente = :nombre, telefono = :telefono, correo = :correo, direccion = :direccion, id_comuna = :comuna, autorizado_para_fiar = :autorizado where rut_cliente = :rut");
                da.AgregarParametro(":nombre_cliente", cliente.NombreCliente);
                da.AgregarParametro(":telefono", cliente.Telefono);
                da.AgregarParametro(":correo", cliente.Correo);
                da.AgregarParametro(":direccion", cliente.Direccion);
                da.AgregarParametro(":comuna", cliente.IdComuna, DbType.Int32);
                da.AgregarParametro(":autorizado", cliente.AutorizadoParaFiar ? "y" : "n");
                da.AgregarParametro(":rut", cliente.RutCliente);
                int resultado = da.ExecuteNonQuery();
                res = resultado > 0;
            }
            return res;
        }
        public List<Cliente> List()
        {
            List <Cliente> lista = new List<Cliente>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select rut_cliente, nombre_cliente, telefono, correo, direccion, autorizado_para_fiar, c.id_comuna, p.id_provincia, r.id_region
                                    from cliente c
                                    inner join comuna co on (c.id_comuna = co.id_comuna)
                                    inner join provincia p on (co.id_provincia = p.id_provincia)
                                    inner join region r on (p.id_region = r.id_region)");
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        RutCliente = reader["rut_cliente"].ToString(),
                        NombreCliente = reader["nombre_cliente"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Correo = reader["correo"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        AutorizadoParaFiar = reader["autorizado_para_fiar"].ToString() == "y",
                        IdComuna = Convert.ToInt32(reader["id_comuna"].ToString()),
                        IdProvincia = Convert.ToInt32(reader["id_provincia"].ToString()),
                        IdRegion = Convert.ToInt32(reader["id_region"].ToString())
                    };
                    lista.Add(cliente);
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
                        ClienteDeudor = Read(reader["rut_cliente"].ToString()),
                        UsuarioAutorizador = mantenedorUsuarios.Read(reader["usuario_autorizador"].ToString()),
                        FechaAutorizado = Convert.ToDateTime(reader["fecha_autorizacion"].ToString()),
                        UsuarioBloqueo = String.IsNullOrEmpty(usuario_bloqueo) ? null : mantenedorUsuarios.Read(usuario_bloqueo),
                        FechaBloqueo = String.IsNullOrEmpty(fecha_bloqueo) ? null : (DateTime?) Convert.ToDateTime(fecha_bloqueo)
                    };
                }
            }
            return estado;
        }
        public static void BorrarClientePrueba(string rut)
        {
            using (DataAccess.DataAccess da = new DataAccess.DataAccess())
            {
                da.GenerarComando("delete from cliente where rut_cliente = :rut");
                da.AgregarParametro(":rut", rut);
                da.ExecuteNonQuery();
            }
        }
    }
}
