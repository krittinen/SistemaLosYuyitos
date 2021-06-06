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
    public class WSProveedor : System.Web.Services.WebService
    {

        static MantenedorProveedor mantenedor = new MantenedorProveedor();
        [WebMethod]
        public bool  create(Proveedor proveedor)
        {
            return mantenedor.Create(Proveedor proveedor);
        }

        [WebMethod]
        public Proveedor read(int id_proveedor)
        {
            return mantenedor.Read(id_proveedor);
        }

        [WebMethod]
        public bool  update(Proveedor proveedor) 
        {
            return mantenedor.Update(proveedor);
        }

        [WebMethod]
        public bool delete(int id_proveedor)
        {
            return mantenedor.Delete(id_proveedor);
        }

      
    }


}



