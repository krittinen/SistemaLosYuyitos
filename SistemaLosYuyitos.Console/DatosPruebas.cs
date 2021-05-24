using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaLosYuyitos.Entidad;

namespace SistemaLosYuyitos.ConsoleApp
{
    static class DatosPruebas
    {
        public static string ContraseñaUsuarioPrueba = "A1B2C3D4";
        public static string ContraseñaUsuarioPruebaIncorrecta = "A1B2C3D42";
        public static string IdUsuarioInexistente = "-1";

        public static Cliente clientePrueba = new Cliente()
        {
            RutCliente = "21098450-K",
            NombreCliente = "Cliente Sistema de Prueba",
            Telefono = "+56912345678",
            Correo = "sistema@pruebas.cl",
            AutorizadoParaFiar = true,
            Direccion = "Direccion de Prueba 987",
            IdComuna = 1,
            IdProvincia = 1,
            IdRegion = 1
        };

        public static Cliente clientePruebaModificado = new Cliente()
        {
            RutCliente = "21098450-K",
            NombreCliente = "Cliente Sistema de Prueba 2",
            Telefono = "+56912345678",
            Correo = "sistema@pruebas.cl",
            AutorizadoParaFiar = false,
            Direccion = "Direccion de Prueba 987",
            IdComuna = 1,
            IdProvincia = 1,
            IdRegion = 1
        };

        public static Usuario usuarioPrueba = new Usuario()
        {
            IdUsuario = "UPRUEBA",
            NombreUsuario = "Usuario Sistema de Prueba",
            HashContraseña = ContraseñaUsuarioPrueba,
            Administrador = true,
            Vigente = true,
            UltimoCambioContraseña = new DateTime(2021, 1, 1)
        };
        public static Usuario usuarioPruebaModificado = new Usuario()
        {
            IdUsuario = "UPRUEBA",
            NombreUsuario = "Usuario Sistema de Prueba 2",
            HashContraseña = ContraseñaUsuarioPrueba,
            Administrador = true,
            Vigente = true,
            UltimoCambioContraseña = new DateTime(2021, 1, 1)
        };
        public static Usuario usuarioNoVigente = new Usuario()
        {
            IdUsuario = "UPRUEBANOVIGENTE",
            NombreUsuario = "Usuario Sistema de Prueba No Vigente",
            HashContraseña = ContraseñaUsuarioPrueba,
            Administrador = true,
            Vigente = false,
            UltimoCambioContraseña = new DateTime(2021, 1, 1)
        };

        public static Producto productoPrueba = new Producto()
        {
            CodigoBarra = "12345678",
            FechaVencimiento = null,
            Descripcion = "Producto de prueba",
            PrecioCompra = 12000,
            PrecioVenta = 15000,
            Stock = 1000,
            StockCritico = 100,
            Vigencia = true,
            IdProveedor = 1,
            IdFamilia = 1,
            IdTipo = 1
        };
        public static Producto productoPruebaModificado = new Producto()
        {
            CodigoBarra = "12345678",
            FechaVencimiento = null,
            Descripcion = "Producto de prueba 2",
            PrecioCompra = 18000,
            PrecioVenta = 21000,
            Stock = 1000,
            StockCritico = 50,
            Vigencia = true,
            IdProveedor = 1,
            IdFamilia = 1,
            IdTipo = 1
        };

    }
}
