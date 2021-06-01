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
	public class ControladorRegiones
	{
        DataAccess.DataAccess da;

        public List<Region> ObtenerRegiones()
        {
            List<Region> list = new List<Region>();
            //Region region = new Region();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_region, nombre_region from region");
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    Region reg = new Region
                    {
                        IdRegion = Convert.ToInt32(reader["id_region"].ToString()),
                        NombreRegion = reader["nombre_region"].ToString()
                    };
                    list.Add(reg);
                }
            }
            return list;

        }

        public List<Provincia> ObtenerProvinciasDeRegion(string id_region)
        {
            List<Provincia> list = new List<Provincia>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_provincia, nombre_provincia from provincia where id_region = :id_region ");
                da.AgregarParametro(":id_region", id_region);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    Provincia prov = new Provincia
                    {
                        IdProvincia = Convert.ToInt32(reader["id_provincia"].ToString()),
                        NombreProvincia = reader["nombre_provincia"].ToString()
                    };
                    list.Add(prov);

                }
            }
            return list;

        }

        public List<Comuna> ObtenerComunasDeProvincia(string id_provincia)
        {
            List<Comuna> list = new List<Comuna>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_comuna, nombre_comuna from comuna where id_provincia = :id_provincia");
                da.AgregarParametro(":id_provincia", id_provincia);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    Comuna com = new Comuna
                    {
                        IdComuna = Convert.ToInt32(reader["id_comuna"].ToString()),
                        NombreComuna = reader["nombre_comuna"].ToString()
                    };
                    list.Add(com);
                }
            }
            return list;

        }

    }
}
