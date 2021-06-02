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
    /// Summary description for WSFiadosAbonos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSFiadosAbonos : System.Web.Services.WebService
    {
        static MantenedorFiadosAbonos mantenedor = new MantenedorFiadosAbonos();
        [WebMethod]
        public bool IngresarDeuda(Fiado fiado)
        {
            return mantenedor.Create(fiado);
        }
        [WebMethod]
        public Fiado ObtenerFiado(decimal id_fiado)
        {
            return mantenedor.Read(id_fiado);
        }
        [WebMethod]
        public bool Abonar(Abono abono, decimal id_fiado)
        {
            return mantenedor.Abonar(abono, id_fiado);
        }
        [WebMethod]
        public List<Fiado> ListarFiados()
        {
            return mantenedor.ListarFiados();
        }
        [WebMethod]
        public List<Fiado> ListarFiadosPorCliente(string rut)
        {
            return mantenedor.ListarFiadosPorCliente(rut);
        }
        [WebMethod]
        public List<Abono> ListarAbonos(decimal id_fiado)
        {
            return mantenedor.ListarAbonos(id_fiado);
        }
        [WebMethod]
        public List<Abono> ListarAbonosPorCliente(string rut)
        {
            return mantenedor.ListarAbonosPorCliente(rut);
        }
        [WebMethod]
        public bool AutorizarClienteDeudor(string rut_cliente, string id_usuario)
        {
            return mantenedor.AutorizarClienteDeudor(rut_cliente, id_usuario);
        }
        [WebMethod]
        public bool RevocarAutorizacionClienteDeudor(string rut_cliente, string id_usuario)
        {
            return mantenedor.RevocarAutorizacionClienteDeudor(rut_cliente, id_usuario);
        }
        [WebMethod]
        public EstadoClienteDeudor ObtenerEstadoClienteDeudor(string rut)
        {
            return mantenedor.ObtenerEstadoClienteDeudor(rut);
        }
    }
}
