using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace SistemaLosYuyitos.OracleDriver
{
    public class OracleDriver : IDBDriver
    {
        string str_conexion = "User ID={0};Password={1}";
        OracleConnection conn;
        OracleCommand cmd = new OracleCommand();

        public OracleDriver(string username, string password)
        {
            str_conexion = String.Format(str_conexion, username, password);
        }

        public bool Conectar(string str_conexion)
        {
            throw new NotImplementedException();
        }

        public IDbCommand CrearComando()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int EjecutarNonQuery()
        {
            throw new NotImplementedException();
        }

        public IDataReader EjecutarReader()
        {
            throw new NotImplementedException();
        }

        public bool InsertarComando(string comando, CommandType tipo = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public bool InsertarComando(IDbCommand comando)
        {
            throw new NotImplementedException();
        }

        public bool InsertarParametro(object param, IDbDataParameter tipo_parametro)
        {
            throw new NotImplementedException();
        }
    }
}
