using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaLosYuyitos.DataAccess;
using SistemaLosYuyitos.Controlador;
using SistemaLosYuyitos.Entidad;
using System.Data.SqlClient;

namespace SistemaLosYuyitos.ConsoleApp
{
    public class Program
    {
        static MantenedorClientes mantenedorClientes = new MantenedorClientes();
        static MantenedorUsuarios mantenedorUsuarios = new MantenedorUsuarios();
        static MantenedorFiadosAbonos mantenedorFiados = new MantenedorFiadosAbonos();
        static MantenedorProducto mantenedorProducto = new MantenedorProducto();
        static Cliente cliente;
        static Usuario usuario;
        static Fiado fiado;
        static Abono abono;
        static Producto producto;
        static bool resultado;
        static int correctos;
        static int errores;
        static void Main(string[] args)
        {
            MantenedorClientes.BorrarClientePrueba(DatosPruebas.clientePrueba.RutCliente);
            MantenedorUsuarios.BorrarUsuariosPrueba(new List<string>() { DatosPruebas.usuarioPrueba.IdUsuario, DatosPruebas.usuarioNoVigente.IdUsuario});
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            Console.Clear();
            ConsoleKeyInfo key;
            Console.WriteLine("------ Sistema de Pruebas Los Yuyitos -----\n\n");
            Console.WriteLine("Opciones:\n");
            Console.WriteLine("1. Pruebas Mantenedor de Usuarios");
            Console.WriteLine("2. Pruebas Mantenedor de Clientes");
            Console.WriteLine("3. Pruebas Mantenedor de Fiados y Abonos");
            Console.WriteLine("0. Salir");
            Console.Write("Escoja una opcion: ");
            do
            {
                key = Console.ReadKey();
                Console.WriteLine();
                switch (key.Key)
                {
                    case (ConsoleKey.D1):
                        MenuMantenedorUsuarios();
                        break;
                    case (ConsoleKey.D2):
                        MenuMantenedorClientes();
                        break;
                    case (ConsoleKey.D3):
                        MenuMantenedorFiados();
                        break;
                    case (ConsoleKey.D0):
                        break;
                    default:
                        MenuPrincipal();
                        break;
                }
            }
            while (key.Key >= ConsoleKey.D1 && key.Key <= ConsoleKey.D9);
        }
        static void MenuMantenedorUsuarios()
        {
            errores = 0;
            correctos = 0;
            Console.Clear();
            Console.WriteLine("------ Admin Consola para Sistema Los Yuyitos -----\n\n");
            Console.WriteLine("Pruebas Mantenedor de Usuarios\n\n");

            Console.WriteLine("\nEjecutando prueba de insercion");
            try
            {
                if (mantenedorUsuarios.Create(DatosPruebas.usuarioPrueba) && mantenedorUsuarios.Create(DatosPruebas.usuarioNoVigente))
                {
                    Console.WriteLine("Prueba satisfactoria.");
                    correctos++;
                }
                else
                {
                    Console.WriteLine("Prueba no satisfactoria.");
                    errores++;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            Console.WriteLine("\nEjecutando prueba de lectura");
            usuario = mantenedorUsuarios.Read(DatosPruebas.usuarioPrueba.IdUsuario);
            if (usuario.IdUsuario != null && usuario.IdUsuario == DatosPruebas.usuarioPrueba.IdUsuario)
            {
                Console.WriteLine("\nId Usuario: {0}", usuario.IdUsuario);
                Console.WriteLine("Nombre Usuario: {0}", usuario.NombreUsuario);
                Console.WriteLine("Hash Contraseña: {0}", usuario.HashContraseña);
                Console.WriteLine("Administrador: {0}", usuario.Administrador);
                Console.WriteLine("Vigente: {0}", usuario.Vigente);
                Console.WriteLine("Ultimo Cambio de Contraseña: {0}", usuario.UltimoCambioContraseña);
                Console.WriteLine("Fecha de Creacion: {0}", usuario.FechaCreacion);
                Console.WriteLine("\nPrueba satisfactoria");
                correctos++;
            }
            else
            {
                Console.WriteLine("Prueba no satisfactoria");
                errores++;
            }
            usuario = mantenedorUsuarios.Read(DatosPruebas.usuarioNoVigente.IdUsuario);
            if (usuario.IdUsuario != null && usuario.IdUsuario == DatosPruebas.usuarioNoVigente.IdUsuario)
            {
                Console.WriteLine("\nId Usuario: {0}", usuario.IdUsuario);
                Console.WriteLine("Nombre Usuario: {0}", usuario.NombreUsuario);
                Console.WriteLine("Hash Contraseña: {0}", usuario.HashContraseña);
                Console.WriteLine("Administrador: {0}", usuario.Administrador);
                Console.WriteLine("Vigente: {0}", usuario.Vigente);
                Console.WriteLine("Ultimo Cambio de Contraseña: {0}", usuario.UltimoCambioContraseña);
                Console.WriteLine("Fecha de Creacion: {0}", usuario.FechaCreacion);
                Console.WriteLine("\nPrueba satisfactoria");
                correctos++;
            }
            else
            {
                Console.WriteLine("Prueba no satisfactoria");
                errores++;
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            Console.WriteLine("\nEjecutando prueba de modificacion.");
            if (mantenedorUsuarios.Update(DatosPruebas.usuarioPruebaModificado))
            {
                usuario = mantenedorUsuarios.Read(DatosPruebas.usuarioPrueba.IdUsuario);
                if (usuario != null && usuario.NombreUsuario == DatosPruebas.usuarioPruebaModificado.NombreUsuario)
                {
                    Console.WriteLine("\nId Usuario: {0}", usuario.IdUsuario);
                    Console.WriteLine("Nombre Usuario: {0}", usuario.NombreUsuario);
                    Console.WriteLine("Hash Contraseña: {0}", usuario.HashContraseña);
                    Console.WriteLine("Administrador: {0}", usuario.Administrador);
                    Console.WriteLine("Vigente: {0}", usuario.Vigente);
                    Console.WriteLine("Ultimo Cambio de Contraseña: {0}", usuario.UltimoCambioContraseña);
                    Console.WriteLine("Fecha de Creacion: {0}", usuario.FechaCreacion);
                    Console.WriteLine("\nPrueba satisfactoria");
                    correctos++;
                }
                else
                {
                    Console.WriteLine("Prueba no satisfactoria");
                    errores++;
                }
            }
            else
            {
                Console.WriteLine("Prueba no satisfactoria");
                errores++;
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            Console.WriteLine("\nEjecutando pruebas de logueo\n");

            resultado = mantenedorUsuarios.Login(DatosPruebas.usuarioPrueba.IdUsuario, DatosPruebas.ContraseñaUsuarioPrueba);
            if (resultado)
            {
                correctos++;
            }
            else
            {
                errores++;
            }
            Console.WriteLine("Prueba de logueo con cuenta vigente y contraseña correcta: {0}", resultado ? "Satisfactorio" : "No satisfactorio");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            resultado = mantenedorUsuarios.Login(DatosPruebas.usuarioPrueba.IdUsuario, DatosPruebas.ContraseñaUsuarioPruebaIncorrecta);
            if (!resultado)
            {
                correctos++;
            }
            else
            {
                errores++;
            }
            Console.WriteLine("Prueba de logueo con cuenta vigente y contraseña incorrecta: {0}", !resultado ? "Satisfactorio" : "No satisfactorio");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            resultado = mantenedorUsuarios.Login(DatosPruebas.usuarioNoVigente.IdUsuario, DatosPruebas.ContraseñaUsuarioPrueba);
            if (!resultado)
            {
                correctos++;
            }
            else
            {
                errores++;
            }
            Console.WriteLine("Prueba de logueo con cuenta no vigente: {0}", !resultado ? "Satisfactorio" : "No satisfactorio");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            resultado = mantenedorUsuarios.Login(DatosPruebas.IdUsuarioInexistente, DatosPruebas.ContraseñaUsuarioPrueba);
            if (!resultado)
            {
                correctos++;
            }
            else
            {
                errores++;
            }
            Console.WriteLine("Prueba de logueo con cuenta erronea: {0}", !resultado ? "Satisfactorio" : "No satisfactorio");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            Console.WriteLine("\nPrueba de eliminacion\n");
            if (mantenedorUsuarios.Delete(DatosPruebas.usuarioNoVigente.IdUsuario) && mantenedorUsuarios.Delete(DatosPruebas.usuarioPrueba.IdUsuario))
            {
                Usuario u1 = mantenedorUsuarios.Read(DatosPruebas.usuarioPrueba.IdUsuario);
                Usuario u2 = mantenedorUsuarios.Read(DatosPruebas.usuarioNoVigente.IdUsuario);
                if (u1 == null && u2 == null)
                {
                    Console.WriteLine("Prueba satisfactoria");
                    correctos++;
                }
                else
                {
                    Console.WriteLine("Prueba no satisfactoria");
                    errores++;
                }
            }
            else
            {
                Console.WriteLine("Prubea no satisfactoria");
                errores++;
            }

            Console.WriteLine("\nPruebas correctas: {0}\nPruebas erroneas: {1}\n", correctos, errores);
            Console.WriteLine("Presione cualquier tecla para continuar...");
        }
        static void MenuMantenedorClientes()
        {
            errores = 0;
            correctos = 0;
            Console.Clear();
            Console.WriteLine("------ Admin Consola para Sistema Los Yuyitos -----\n\n");
            Console.WriteLine("Pruebas Mantenedor de Clientes\n\n");

            Console.WriteLine("\nEjecutando prueba de insercion");
            try
            {
                if (mantenedorClientes.Create(DatosPruebas.clientePrueba))
                {
                    Console.WriteLine("Prueba satisfactoria.");
                    correctos++;
                }
                else
                {
                    Console.WriteLine("Prueba no satisfactoria.");
                    errores++;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            Console.WriteLine("\nEjecutando prueba de lectura");
            cliente = mantenedorClientes.Read(DatosPruebas.clientePrueba.RutCliente);
            if (cliente.RutCliente != null && cliente.RutCliente == DatosPruebas.clientePrueba.RutCliente)
            {
                Console.WriteLine("\nRUT Cliente: {0}", cliente.RutCliente);
                Console.WriteLine("Nombre Cliente: {0}", cliente.NombreCliente);
                Console.WriteLine("Telefono Cliente: {0}", cliente.Telefono);
                Console.WriteLine("Correo Cliente: {0}", cliente.Correo);
                Console.WriteLine("Direccion Cliente: {0}", cliente.Direccion);
                Console.WriteLine("Autorizado para fiar: {0}", cliente.AutorizadoParaFiar);
                Console.WriteLine("ID Comuna: {0}", cliente.IdComuna);
                Console.WriteLine("ID Provincia: {0}", cliente.IdProvincia);
                Console.WriteLine("ID Region: {0}", cliente.IdRegion); 
                Console.WriteLine("\nPrueba satisfactoria");
                correctos++;
            }
            else
            {
                Console.WriteLine("Prueba no satisfactoria");
                errores++;
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            Console.WriteLine("\nEjecutando prueba de modificacion.");
            if (mantenedorClientes.Update(DatosPruebas.clientePruebaModificado))
            {
                cliente = mantenedorClientes.Read(DatosPruebas.clientePrueba.RutCliente);
                if (cliente != null && cliente.NombreCliente == DatosPruebas.clientePruebaModificado.NombreCliente)
                {
                    Console.WriteLine("\nRUT Cliente: {0}", cliente.RutCliente);
                    Console.WriteLine("Nombre Cliente: {0}", cliente.NombreCliente);
                    Console.WriteLine("Telefono Cliente: {0}", cliente.Telefono);
                    Console.WriteLine("Correo Cliente: {0}", cliente.Correo);
                    Console.WriteLine("Direccion Cliente: {0}", cliente.Direccion);
                    Console.WriteLine("Autorizado para fiar: {0}", cliente.AutorizadoParaFiar);
                    Console.WriteLine("ID Comuna: {0}", cliente.IdComuna);
                    Console.WriteLine("ID Provincia: {0}", cliente.IdProvincia);
                    Console.WriteLine("ID Region: {0}", cliente.IdRegion);
                    Console.WriteLine("\nPrueba satisfactoria");
                    correctos++;
                }
                else
                {
                    Console.WriteLine("Prueba no satisfactoria");
                    errores++;
                }
            }
            else
            {
                Console.WriteLine("Prueba no satisfactoria");
                errores++;
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            Console.WriteLine("\nEjecutando prueba de lista");
            List<Cliente> lista = mantenedorClientes.List();
            if (lista.Count > 0)
            {
                Console.WriteLine("Prueba satisfactoria\n");
                correctos++;
                foreach (var item in lista)
                {
                    Console.WriteLine(item.RutCliente);
                }
            }
            else
            {
                Console.WriteLine("Prueba no satisfactoria");
                errores++;
            }

            Console.WriteLine("\nPruebas correctas: {0}\nPruebas erroneas: {1}\n", correctos, errores);
            Console.WriteLine("Presione cualquier tecla para continuar...");
        }

        static void MenuMantenedorFiados()
        {
            Console.Clear();
            Console.WriteLine("------ Admin Consola para Sistema Los Yuyitos -----\n\n");
            Console.WriteLine("Mantenedor de Fiados y Abonos\n");
            Console.WriteLine("Opciones:\n");
            Console.WriteLine("1. Generar Fiado");
            Console.WriteLine("2. Abono Fiado");
        }
    }
}
