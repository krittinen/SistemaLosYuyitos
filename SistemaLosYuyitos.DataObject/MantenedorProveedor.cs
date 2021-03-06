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
    public class MantenedorProveedor
    {
        DataAccess.DataAccess da;

        public bool Create(Proveedor proveedor)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into proveedor(id_proveedor, nombre_proveedor, telefono, email, direccion, id_comuna, id_rubro, vigencia)
                                    values (seq_id_proveedor.nextval, :nombre, :telefono, :email, :direccion, :comuna, :rubro, :vigencia)");
                da.AgregarParametro(":nombre", proveedor.NombreProveedor);
                da.AgregarParametro(":telefono", proveedor.Telefono);
                da.AgregarParametro(":email", proveedor.Email);
                da.AgregarParametro(":direccion", proveedor.Direccion);
                da.AgregarParametro(":comuna", proveedor.Comuna, DbType.Int32);
                da.AgregarParametro(":rubro", proveedor.Rubro, DbType.Int32);
                da.AgregarParametro(":vigencia", proveedor.Vigencia ? "y" : "n", DbType.String);
                int resultado = da.ExecuteNonQuery();
                res = resultado >= 0;
            }
            return res;
        }

        public Proveedor Read(int id_proveedor)
        {
            Proveedor proveedor = new Proveedor();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_proveedor, nombre_proveedor, telefono, email, direccion, id_comuna, id_rubro, vigencia from proveedor where id_proveedor = :proveedor");
                da.AgregarParametro(":proveedor", id_proveedor);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    proveedor.IdProveedor = Convert.ToInt32(reader["id_proveedor"].ToString());
                    proveedor.NombreProveedor = reader["nombre_proveedor"].ToString();
                    proveedor.Telefono = reader["telefono"].ToString();
                    proveedor.Email = reader["email"].ToString();
                    proveedor.Direccion = reader["direccion"].ToString();
                    proveedor.Comuna = Convert.ToInt32(reader["COMUNA_id_comuna"].ToString());
                    proveedor.Rubro = Convert.ToInt32(reader["RUBRO_id_rubro"].ToString());
                    proveedor.Vigencia = reader["vigencia"].ToString() == "y";
               
                }
            }
            return proveedor;
        }

        public bool Update(Proveedor proveedor)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("update proveedor set nombre_proveedor = :nombre_proveedor, telefono = :telefono, email = :email, id_comuna = :comuna, id_rubro = :rubro, vigencia = :vigencia where id_proveedor = :id_proveedor");
                da.AgregarParametro(":nombre_proveedor", proveedor.NombreProveedor);
                da.AgregarParametro(":telefono", proveedor.Telefono);
                da.AgregarParametro(":email", proveedor.Email);
                da.AgregarParametro(":direccion", proveedor.Direccion);
                da.AgregarParametro(":comuna", proveedor.Comuna, DbType.Int32);
                da.AgregarParametro(":rubro", proveedor.Rubro, DbType.Int32);
                da.AgregarParametro(":vigencia", proveedor.Vigencia ? "y" : "n");
                int resultado = da.ExecuteNonQuery();
                res = resultado >= 0;
            }
            return res;
        }

        public bool Delete(int id_proveedor)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("delete from proveedor where id_proveedor = :proveedor");
                da.AgregarParametro(":proveedor", id_proveedor, DbType.Int32);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }

    }
}