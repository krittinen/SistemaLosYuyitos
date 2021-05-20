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
        public bool Create(Cliente cliente)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into cliente(rut_cliente, nombre_cliente, telefono, correo, direccion, autorizado_para_fiar, comuna_id_comuna)
                                    values (:rut, :nombre, :telefono, :correo, :direccion, :autorizado, :comuna)");
                da.AgregarParametro(":rut", cliente.RutCliente);
                da.AgregarParametro(":nombre_cliente", cliente.NombreCliente);
                da.AgregarParametro(":telefono", cliente.Telefono);
                da.AgregarParametro(":correo", cliente.Correo);
                da.AgregarParametro(":direccion", cliente.Direccion);
                da.AgregarParametro(":autorizado", cliente.AutorizadoParaFiar ? "y" : "n");
                da.AgregarParametro(":comuna", cliente.IdComuna, DbType.Int32);
                int resultado = da.ExecuteNonQuery();
                res = resultado >= 0;
            }
            return res;
        }

        public Cliente Read(string rut)
        {
            Cliente cliente = new Cliente();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select rut_cliente, nombre_cliente, telefono, correo, direccion, autorizado_para_fiar, comuna_id_comuna from cliente where rut_cliente = :rut");
                da.AgregarParametro(":rut", rut);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    cliente.RutCliente = reader["rut_cliente"].ToString();
                    cliente.NombreCliente = reader["nombre_cliente"].ToString();
                    cliente.Telefono = reader["telefono"].ToString();
                    cliente.Correo = reader["correo"].ToString();
                    cliente.Direccion = reader["direccion"].ToString();
                    cliente.AutorizadoParaFiar = reader["autorizado_para_fiar"].ToString() == "y";
                    cliente.IdComuna = Convert.ToInt32(reader["comuna_id_comuna"].ToString());
                }
            }
            return cliente;
        }

        public bool Update(Cliente cliente)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("update cliente set nombre_cliente = :nombre, telefono = :telefono, correo = :correo, direccion = :direccion, autorizado_para_fiar = :autorizado, comuna_id_comuna = :comuna where rut_cliente = :rut");
                da.AgregarParametro(":rut", cliente.RutCliente);
                da.AgregarParametro(":nombre_cliente", cliente.NombreCliente);
                da.AgregarParametro(":telefono", cliente.Telefono);
                da.AgregarParametro(":correo", cliente.Correo);
                da.AgregarParametro(":direccion", cliente.Direccion);
                da.AgregarParametro(":autorizado", cliente.AutorizadoParaFiar ? "y" : "n");
                da.AgregarParametro(":comuna", cliente.IdComuna, DbType.Int32);
                int resultado = da.ExecuteNonQuery();
                res = resultado >= 0;
            }
            return res;
        }
        public List<Cliente> List()
        {
            List <Cliente> lista = new List<Cliente>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select rut_cliente, nombre_cliente, telefono, correo, direccion, autorizado_para_fiar, comuna_id_comuna from cliente");
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.RutCliente = reader["rut_cliente"].ToString();
                    cliente.NombreCliente = reader["nombre_cliente"].ToString();
                    cliente.Telefono = reader["telefono"].ToString();
                    cliente.Correo = reader["correo"].ToString();
                    cliente.Direccion = reader["direccion"].ToString();
                    cliente.AutorizadoParaFiar = reader["autorizado_para_fiar"].ToString() == "y";
                    cliente.IdComuna = Convert.ToInt32(reader["comuna_id_comuna"].ToString());
                    lista.Add(cliente);
                }
            }
            return lista;
        }
    }
}
