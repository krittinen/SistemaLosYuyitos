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
    public class WSClientes : System.Web.Services.WebService
    {
        static MantenedorClientes mantenedor = new MantenedorClientes();
        [WebMethod]
        public bool IngresarCliente(Cliente cliente)
        {
            return mantenedor.Create(cliente);
        }
        [WebMethod]
        public Cliente ObtenerCliente(string rut)
        {
            return mantenedor.Read(rut);
        }
        [WebMethod]
        public bool Actualizarcliente(Cliente cliente)
        {
            return mantenedor.Update(cliente);
        }
        [WebMethod]
        public List<Cliente> ListarClientes()
        {
            return mantenedor.List();
        }
    }
}
