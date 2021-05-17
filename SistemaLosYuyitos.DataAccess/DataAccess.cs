using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;

namespace SistemaLosYuyitos.DataAccess
{
    public class DataAccess : IDisposable
    {
        private OracleConnection conexion;
        private OracleCommand comando;
        private static string ruta_conexion;
        public DataAccess()
        {
            conexion = new OracleConnection();
            LeerConfiguracion();
            conexion.ConnectionString = ruta_conexion;
            conexion.Open();
        }

        public void Open()
        {
            conexion.Open();
        }

        public void Close()
        {
            conexion.Close();
        }

        public void GenerarComando(string texto)
        {
            this.comando = conexion.CreateCommand();
            this.comando.CommandText = texto;
        }

        public void AgregarParametro(string nombre, object valor)
        {
            IDbDataParameter param = comando.CreateParameter();
            param.ParameterName = nombre;
            param.Value = valor;
            this.comando.Parameters.Add(param);
        }

        public void AgregarParametro(string nombre, object valor, DbType tipo)
        {
            OracleParameter param = comando.CreateParameter();
            param.ParameterName = nombre;
            param.Value = valor;
            param.DbType = tipo;
            this.comando.Parameters.Add(param);
        }

        public IDataReader ExecuteReader()
        {
            IDataReader reader;
            reader = comando.ExecuteReader();
            return reader;
        }

        public int ExecuteNonQuery()
        {
            int result;
            result = comando.ExecuteNonQuery();
            return result;
        }

        private static void LeerConfiguracion()
        {
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = @".\Conexion.config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            ruta_conexion = config.AppSettings.Settings["ruta_conexion"].Value;
        }

        public static void EscribirConfiguracion(string ruta_conexion)
        {
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = @".\Conexion.config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("ruta_conexion");
            config.Save(ConfigurationSaveMode.Modified);
            config.AppSettings.Settings.Add("ruta_conexion", ruta_conexion);
            config.Save(ConfigurationSaveMode.Modified);
        }

        public void Dispose()
        {
            conexion.Close();
        }
    }
}
