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
    /// Summary description for WSOrdenPedido
    /// </summary>

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSOrdenPedido : System.Web.Services.WebService
    {

        static MantenedorOrdenPedido mantenedor = new MantenedorOrdenPedido();
        [WebMethod]
        public bool CreateOrden(Orden orden)
        {
            return mantenedor.Create(orden);
        }

        [WebMethod]
        public bool CreatePedido(List<Pedido> pedido)
        {
            return mantenedor.CreatePedido(pedido);
        }

        [WebMethod]
        public Orden ReadOrden(int num_orden)
        {
            return mantenedor.Read(num_orden);
        }

        [WebMethod]
        public List<Pedido> ReadPedido(int num_orden)
        {
            return mantenedor.ListarPedido(num_orden);
        }


    }


}
