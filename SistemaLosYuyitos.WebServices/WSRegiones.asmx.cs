using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SistemaLosYuyitos.Controlador;
using SistemaLosYuyitos.Entidad;

namespace SistemaLosYuyitos.WebServices
{
    /// <summary>
    /// Summary description for WSRegiones
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSRegiones : System.Web.Services.WebService
    {
        MantenedorRegiones mantenedor = new MantenedorRegiones();
        [WebMethod]
        public List<Region> ListarRegiones()
        {
            return mantenedor.ListarRegiones();
        }
        [WebMethod]
        public List<Provincia> ListarProvincias(int id_region)
        {
            return mantenedor.ListarProvincias(id_region);
        }
        [WebMethod]
        public List<Comuna> ListarComunas(int id_provincia)
        {
            return mantenedor.ListarComunas(id_provincia);
        }
        [WebMethod]
        public Provincia ObtenerProvincia(int id_comuna)
        {
            return mantenedor.obtenerProvincia(id_comuna);
        }
        [WebMethod]
        public Region ObtenerRegion(int id_provincia)
        {
            return mantenedor.obtenerRegion(id_provincia);
        }
    }
}
