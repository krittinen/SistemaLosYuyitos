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
    public class MantenedorUsuarios
    {
        DataAccess.DataAccess da;

        public bool Create(Usuario usuario)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"insert into usuario(id_usuario, nombre_usuario, hash_contraseña, ultimo_cambio_contraseña, administrador, vigencia
                                    values (:id_usuario, :nombre_usuario, :hash, NOW(), :administrador, :vigencia)");
                da.AgregarParametro(":id_usuario", usuario.IdUsuario, DbType.String);
                da.AgregarParametro(":nombre_usuario", usuario.NombreUsuario, DbType.String);
                da.AgregarParametro(":hash", usuario.HashContraseña, DbType.String);
                da.AgregarParametro(":administrador", usuario.Administrador, DbType.Boolean);
                da.AgregarParametro(":vigencia", usuario.Vigente, DbType.Boolean);
                int resultado = da.ExecuteNonQuery();
                res = resultado == 0;
            }
            return res;
        }
        public Usuario Read(string id_usuario)
        {
            Usuario usuario = new Usuario();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_usuario, nombre_usuario, administrador, vigencia from usuario where id_usuario = :usuario");
                da.AgregarParametro(":usuario", id_usuario);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    usuario.IdUsuario = reader["id_usuario"].ToString();
                    usuario.NombreUsuario = reader["nombre_usuario"].ToString();
                    usuario.Administrador = reader["administrador"].ToString() == "y";
                    usuario.Vigente = reader["vigencia"].ToString() == "y";
                }
            }
            return usuario;
        }

        public bool Update(Usuario usuario)
        {
            bool res = false;
            return res;
        }

        public bool Delete(string id_usuario)
        {
            bool res = false;
            return res;
        }

        public bool Login(string id_usuario, string hash)
        {
            bool logueado = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_usuario, nombre_usuario, administrador, vigencia from usuario where id_usuario = :usuario and hash_contraseña = :hash");
                da.AgregarParametro(":usuario", id_usuario);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    logueado = true;
                }
            }
            return logueado;
        }
    }
}
