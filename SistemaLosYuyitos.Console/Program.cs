using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaLosYuyitos.DataAccess;
using SistemaLosYuyitos.Controlador;
using SistemaLosYuyitos.Entidad;

namespace SistemaLosYuyitos.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //DataAccess.DataAccess da = new SistemaLosYuyitos.DataAccess.DataAccess();
            MantenedorUsuarios mantenedor = new MantenedorUsuarios();
            Usuario usuario = mantenedor.Read("JUANITA");
            Console.WriteLine("IdUsuario: {0} - NombreUsuario: {1} - Administrador: {2} - Vigente: {3}", usuario.IdUsuario, usuario.NombreUsuario, usuario.Administrador, usuario.Vigente);
            Console.WriteLine();
            Usuario usuario2 = new Usuario()
            {
                IdUsuario = "RSEGURA",
                NombreUsuario = "RAFAEL SEGURA",
                HashContraseña = "PRUEBA",
                Administrador = true,
                Vigente = true
            };
            mantenedor.Create(usuario2);
            Console.ReadLine();
        }
    }
}
