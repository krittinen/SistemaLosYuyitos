using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SistemaLosYuyitos.Controlador;

namespace SistemaLosYuyitos.WebServices
{
    /// <summary>
    /// Summary description for WSPruebaConexion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSPruebaConexion : System.Web.Services.WebService
    {
        PruebaConexion ps = new PruebaConexion();

        [WebMethod]
        public bool ProbarConexion()
        {
            
            return ps.Prueba();
        }
    }
}
