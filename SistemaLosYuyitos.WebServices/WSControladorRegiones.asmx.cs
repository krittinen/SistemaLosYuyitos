using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SistemaLosYuyitos.Entidad;
using SistemaLosYuyitos.Controlador;


namespace SistemaLosYuyitos.WebServices
{
    /// <summary>
    /// Summary description for WSClientes
    /// </summary>

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSControladorRegiones : System.Web.Services.WebService
    {

        static ControladorRegiones mantenedor = new ControladorRegiones();
        [WebMethod]
        public List<Region> llenarRegiones()
        {
            return mantenedor.llenarRegiones();
        }

        [WebMethod]
        public List<Provincia> llenarProvincia(int id_region)
        {
            return mantenedor.llenarProvincia(id_region);
        }

        [WebMethod]
        public List<Comuna> llenarComunas(int id_provincia)
        {
            return mantenedor.llenarComunas(id_provincia);
        }

        [WebMethod]
        public Provincia obtenerProvincia(int id_comuna)
        {
            return mantenedor.obtenerProvincia(id_comuna);
        }

        [WebMethod]
        public Region obtenerRegion(int id_provincia)
        {
            return mantenedor.obtenerRegion(id_provincia);
        }
    }


}




