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
    /// Summary description for WSProveedores
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSProveedores : System.Web.Services.WebService
    {
        MantenedorProveedor mantenenedor = new MantenedorProveedor();

        [WebMethod]
        public bool IngresarProveedor(Proveedor proveedor)
        {
            return mantenenedor.Create(proveedor);
        }
        [WebMethod]
        public Proveedor ObtenerProveedor(int id_proveedor)
        {
            return mantenenedor.Read(id_proveedor);
        }
        [WebMethod]
        public bool ActualizarProveedor(Proveedor proveedor)
        {
            return mantenenedor.Update(proveedor);
        }
        [WebMethod]
        public bool EliminarProveedor(int id_proveedor)
        {
            return mantenenedor.Delete(id_proveedor);
        }
    }
}
