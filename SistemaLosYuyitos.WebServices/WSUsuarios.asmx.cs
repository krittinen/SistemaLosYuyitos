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
    /// Summary description for WSUsuarios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSUsuarios : WebService
    {
        static readonly MantenedorUsuarios mantenedor = new MantenedorUsuarios();
        [WebMethod]
        public bool IngresarUsuario(Usuario usuario)
        {
            return mantenedor.Create(usuario);
        }
        [WebMethod]
        public Usuario ObtenerUsuario(string id_usuario)
        {
            return mantenedor.Read(id_usuario);
        }
        [WebMethod]
        public bool ActualizarUsuario(Usuario usuario)
        {
            return mantenedor.Update(usuario);
        }
        [WebMethod]
        public bool EliminarUsuario(string id_usuario)
        {
            return mantenedor.Delete(id_usuario);
        }
        [WebMethod]
        public List<Usuario> ListarUsuarios()
        {
            return mantenedor.List();
        }
        public bool Login(string id_usuario, string hash)
        {
            return mantenedor.Login(id_usuario, hash);
        }
    }
}
