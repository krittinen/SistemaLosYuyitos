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
    /// Summary description for WSOrdenes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSOrdenes : System.Web.Services.WebService
    {
        MantenedorOrdenes mantenedor = new MantenedorOrdenes();
        [WebMethod]
        public bool GenerarOrden(Orden orden)
        {
            return mantenedor.Create(orden);
        }
        [WebMethod]
        public Orden ObtenerOrden(decimal id_orden)
        {
            return mantenedor.Read(id_orden);
        }
        [WebMethod]
        public List<Orden> ListarOrdenes()
        {
            return mantenedor.List();
        }
        [WebMethod]
        public bool AnularOrden(int num_orden)
        {
            return mantenedor.AnularOrden(num_orden);
        }
        [WebMethod]
        public bool RecibirOrden(int num_orden)
        {
            return mantenedor.RecibirOrden(num_orden);
        }
    }
}
