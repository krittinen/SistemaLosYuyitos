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
        static MantenedorClientes mantenedorClientes = new MantenedorClientes();
        static MantenedorUsuarios mantenedorUsuarios = new MantenedorUsuarios();
        static MantenedorFiadosAbonos mantenedorFiados = new MantenedorFiadosAbonos();
        static MantenedorProducto mantenedorProducto = new MantenedorProducto();
        static Cliente cliente;
        static Usuario usuario;
        static Fiado fiado;
        static Abono abono;
        static Producto producto;
        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            ConsoleKeyInfo key;
            Console.WriteLine("------ Admin Consola para Sistema Los Yuyitos -----\n\n");
            Console.WriteLine("Opciones:\n");
            Console.WriteLine("1. Mantenedor de Usuarios");
            Console.WriteLine("2. Mantenedor de Clientes");
            Console.WriteLine("3. Mantenedor de Fiados y Abonos");
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

        }
        static void MenuMantenedorClientes()
        {
            Console.Clear();
            Console.WriteLine("------ Admin Consola para Sistema Los Yuyitos -----\n\n");
            Console.WriteLine("Mantenedor de Clientes\n");
            Console.WriteLine("Opciones:\n");
            Console.WriteLine("1. Buscar Cliente");
            Console.WriteLine("2. Agregar Cliente");
            Console.WriteLine("3. Listar Clientes");
            Console.WriteLine("0. Menu Anterior");

        }
        static void MenuBuscarCliente()
        {
            string rut;
            Console.Clear();
            Console.WriteLine("------ Admin Consola para Sistema Los Yuyitos -----\n\n");
            Console.WriteLine("Mantenedor de Clientes\n");
            Console.Write("Ingrese el RUT del Cliente al buscar: ");
            rut = Console.ReadLine();
            Console.WriteLine();
            cliente = mantenedorClientes.Read(rut);
            if (rut != null)
            {
                Console.WriteLine("Datos del Cliente:\n");
                Console.WriteLine("RUT: {0}", cliente.RutCliente);
                Console.WriteLine("Nombre: {0}", cliente.NombreCliente);
                Console.WriteLine("Telefono: {0}", cliente.Telefono);
                Console.WriteLine("Direccion: {0}", cliente.Direccion);
                Console.WriteLine("Comuna: {0}", cliente.IdComuna);
                Console.WriteLine("Provincia: {0}", cliente.IdProvincia);
                Console.WriteLine("Region: {0}", cliente.IdRegion);
                Console.WriteLine("Autorizado para deudas: {0}", cliente.AutorizadoParaFiar);
            }
            

        }
        static void MenuAgregarCliente()
        {

        }
        static void MenuListarClientes()
        {

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
