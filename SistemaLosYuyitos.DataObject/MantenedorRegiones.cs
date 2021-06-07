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
	public class MantenedorRegiones
	{
        DataAccess.DataAccess da;


        //llenar cbx de region
        public List<Region> ListarRegiones()
        {
            List<Region> list = new List<Region>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select id_region, nombre_region from region");
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

        //llenar cbx de provincia
        public List<Provincia> ListarProvincias(int id_region)
        {
            List<Provincia> list = new List<Provincia>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select id_provincia, nombre_provincia from provincia where id_region = :id_region ");
                da.AgregarParametro(":id_region", id_region, DbType.Int32);
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

        //llenar cbx comuna
        public List<Comuna> ListarComunas(int id_provincia)
        {
            List<Comuna> list = new List<Comuna>();
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select id_comuna, nombre_comuna from comuna where id_provincia = :id_provincia");
                da.AgregarParametro(":id_provincia", id_provincia, DbType.Int32);
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


        //obtener provincia por id de comuna
        public Provincia obtenerProvincia (int id_comuna)
        {
            Provincia provincia = null;
            using (da = new DataAccess.DataAccess()) 
            {
                da.GenerarComando(@"select p.id_provincia, p.nombre_provincia, p.id_region
                                    from provincia p
                                    inner join comuna c on (p.id_comuna = c.id_comuna)
                                    where c.id_comuna = :id_comuna");
                da.AgregarParametro(":id_comuna", id_comuna);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    provincia = new Provincia
                    {
                        IdProvincia = Convert.ToInt32(reader["id_provincia"].ToString()),
                        NombreProvincia = reader["nombre_provincia"].ToString(),
                        IdRegion = Convert.ToInt32(reader["id_region"].ToString())
                    };

                }


            }
            return provincia;
        }


        //obtener region por id de provincia
        public Region obtenerRegion(int id_provincia)
        {
            Region region = null;
            using (da = new DataAccess.DataAccess())
            {
                da.GenerarComando(@"select r.id_region, r.nombre_region
                                    from region r
                                    inner join provincia p on (r.id_region = p.id_region)
                                    where p.id_provincia = :id_provincia");
                da.AgregarParametro(":id_provincia", id_provincia);
                IDataReader reader = da.ExecuteReader();
                while (reader.Read())
                {
                    region = new Region
                    {
                        IdRegion = Convert.ToInt32(reader["id_region"].ToString()),
                        NombreRegion = reader["nombre_region"].ToString()
                    };

                }


            }
            return region;
        }

    }
}
