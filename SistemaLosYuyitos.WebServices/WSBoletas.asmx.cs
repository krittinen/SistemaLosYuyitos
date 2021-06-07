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
    /// Summary description for WSBoletas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSBoletas : System.Web.Services.WebService
    {
        MantenedorBoletas mantenedor = new MantenedorBoletas();

        [WebMethod]
        public bool IngresarBoleta(Boleta boleta)
        {
            return mantenedor.Create(boleta);
        }
        [WebMethod]
        public Boleta ObtenerBoleta(string nro_boleta)
        {
            return mantenedor.Read(nro_boleta);
        }
        [WebMethod]
        public bool AnularBoleta(string nro_boleta)
        {
            return mantenedor.Anular(nro_boleta);
        }
    }
}
