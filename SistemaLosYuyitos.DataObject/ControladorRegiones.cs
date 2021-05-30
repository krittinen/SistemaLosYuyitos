using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaLosYuyitos.Entidad;
using SistemaLosYuyitos.DataAccess;
using System.Data;
using System.Data.SqlClient;

public SistemaLosYuyitos.Controlador
{
	public class ControladorRegiones
	{
        DataAccess.DataAccess da;

        public List<Region> llenarRegion()
        {
            List<Region> list = new List<Region>();
            //Region region = new Region();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_region, nombre_region from region");
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["id_region"].ToString(), reader["nombre_region"].ToString());
                    
                }
            }
            return list;

        }

        public List<Provincia> llenarProvincia(string id_region)
        {
            List<Provincia> list = new List<Provincia>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_provincia, nombre_provincia, REGION_id_region from provincia where REGION_id_region = :id_region ");
                da.AgregarParametro(":id_region", id_region);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["id_provincia"].ToString(), reader["nombre_provincia"].ToString(), reader["REGION_id_region"].ToString());

                }
            }
            return list;

        }

        public List<Comuna> llenarComuna(string id_provincia)
        {
            List<Comuna> list = new List<Comuna>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando("select id_comuna, nombre_comuna, PROVINCIA_id_provincia from comuna where PROVINCIA_id_provincia = :id_provincia");
                da.AgregarParametro(":id_provincia", id_provincia);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["id_comuna"].ToString(), reader["nombre_comuna"].ToString(), reader["PROVINCIA_id_provincia"].ToString());

                }
            }
            return list;

        }

    }
}
