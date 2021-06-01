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
                da.GenerarComando(@"insert into usuario(id_usuario, nombre_usuario, hash_contraseña, fecha_creacion, ultimo_cambio_contraseña, administrador, vigencia)
                                    values (:id_usuario, :nombre_usuario, :hash, sysdate, sysdate, :administrador, :vigencia)");
                da.AgregarParametro(":id_usuario", usuario.IdUsuario, DbType.String);
                da.AgregarParametro(":nombre_usuario", usuario.NombreUsuario, DbType.String);
                da.AgregarParametro(":hash", usuario.HashContraseña, DbType.String);
                da.AgregarParametro(":administrador", usuario.Administrador ? "y" : "n", DbType.String);
                da.AgregarParametro(":vigencia", usuario.Vigente ? "y" : "n", DbType.String);
                int resultado = da.ExecuteNonQuery();
                res = resultado > 0;
            }
            return res;
        }
        public Usuario Read(string id_usuario)
        {
            Usuario usuario = null;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_usuario, nombre_usuario, administrador, vigencia, ultimo_cambio_contraseña, fecha_creacion from usuario where id_usuario = :usuario");
                da.AgregarParametro(":usuario", id_usuario);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = reader["id_usuario"].ToString(),
                        NombreUsuario = reader["nombre_usuario"].ToString(),
                        Administrador = reader["administrador"].ToString() == "y",
                        Vigente = reader["vigencia"].ToString() == "y",
                        FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"].ToString()),
                        UltimoCambioContraseña = Convert.ToDateTime(reader["ultimo_cambio_contraseña"].ToString())
                    };
                }
            }
            return usuario;
        }

        public bool Update(Usuario usuario)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("update usuario set nombre_usuario = :nombre_usuario, hash_contraseña = :hash, ultimo_cambio_contraseña = :ultimo_cambio, administrador = :admin, vigencia = :vigencia where id_usuario = :id_usuario");
                da.AgregarParametro(":nombre_usuario", usuario.NombreUsuario);
                da.AgregarParametro(":hash", usuario.HashContraseña);
                da.AgregarParametro(":ultimo_cambio", usuario.UltimoCambioContraseña, DbType.Date);
                da.AgregarParametro(":admin", usuario.Administrador ? "y" : "n");
                da.AgregarParametro(":vigencia", usuario.Vigente ? "y" : "n");
                da.AgregarParametro(":id_usuario", usuario.IdUsuario);
                int resultado = da.ExecuteNonQuery();
                res = resultado > 0;
            }
            return res;
        }

        public bool Delete(string id_usuario)
        {
            bool res = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("delete from usuario where id_usuario = :usuario");
                da.AgregarParametro(":usuario", id_usuario);
                res = da.ExecuteNonQuery() > 0;
            }
            return res;
        }
        public List<Usuario> List()
        {
            List<Usuario> lista = new List<Usuario>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_usuario, nombre_usuario, administrador, vigencia, ultimo_cambio_contraseña, fecha_creacion from usuario");
                var reader = da.ExecuteReader();
                while (reader.Read())
                {
                    string ultimo_cambio_contraseña = reader["ultimo_cambio_contraseña"].ToString();
                    string fecha_creacion = reader["fecha_creacion"].ToString();
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = reader["id_usuario"].ToString(),
                        NombreUsuario = reader["nombre_usuario"].ToString(),
                        Administrador = reader["administrador"].ToString() == "y",
                        Vigente = reader["vigencia"].ToString() == "y",
                        UltimoCambioContraseña = Convert.ToDateTime(ultimo_cambio_contraseña),
                        FechaCreacion = Convert.ToDateTime(fecha_creacion)
                    };
                    lista.Add(usuario);
                }
            }
            return lista;
        }
        public bool Login(string id_usuario, string hash)
        {
            bool logueado = false;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_usuario, nombre_usuario, administrador, vigencia from usuario where id_usuario = :usuario and hash_contraseña = :hash and vigencia = 'y'");
                da.AgregarParametro(":usuario", id_usuario);
                da.AgregarParametro(":hash", hash);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    logueado = true;
                }
            }
            return logueado;
        }

        public static void BorrarUsuariosPrueba(List<string> ids)
        {
            foreach (string id in ids) 
            {
                using (DataAccess.DataAccess da = new DataAccess.DataAccess())
                {
                    da.GenerarComando("delete from usuario where id_usuario = :id");
                    da.AgregarParametro(":id", id);
                    da.ExecuteNonQuery();
                }
            }
        }
    }
}
