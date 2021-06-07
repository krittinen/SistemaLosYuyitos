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
    /// Summary description for WSProductos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSProductos : System.Web.Services.WebService
    {
        MantenedorProducto mantenedor = new MantenedorProducto();
        [WebMethod]
        public bool IngresarProducto(Producto producto)
        {
            return mantenedor.Create(producto);
        }
        [WebMethod]
        public Producto ObtenerProducto(string codigo_barra)
        {
            return mantenedor.Read(codigo_barra);
        }
        [WebMethod]
        public bool ActualizarProducto(Producto producto)
        {
            return mantenedor.Update(producto);
        }
        [WebMethod]
        public bool EliminarProducto(string codigo_barra)
        {
            return mantenedor.Delete(codigo_barra);
        }
        [WebMethod]
        public List<Producto> ListarProductos()
        {
            return mantenedor.Listar();
        }
        [WebMethod]
        public List<Producto> ListarPorProveedor(decimal id_proveedor)
        {
            return mantenedor.ListarPorProveedor(id_proveedor);
        }
    }
}
