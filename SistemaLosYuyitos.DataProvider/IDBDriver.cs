using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SistemaLosYuyitos.OracleDriver
{
    public interface IDBDriver
    {
        bool Conectar(string str_conexion);
        IDbCommand CrearComando();
        bool InsertarComando(string comando, CommandType tipo = CommandType.Text);
        bool InsertarComando(IDbCommand comando);
        bool InsertarParametro(object param, IDbDataParameter tipo_parametro);
        IDataReader EjecutarReader();
        int EjecutarNonQuery();
        void Dispose();
    }
}
