using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Controllers;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ProyectoTiendaOnline.Tests.ControllersTests
{
    public class ClienteControllerTest
    {
        
        [Test]
        public void loginIngreso()
        {
            var usua = new Mock<IUsuarioContenedor>();

            usua.Setup(o => o.usuarioLogin("@gmail.com", "123")).Returns(new Usuario { });
            var controller = new ClienteController(null, usua.Object, null);

            var view = controller.Login("josue62008@gmail.com", "123");

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
        [Test]
        public void loginFalla()
        {
            var usua = new Mock<IUsuarioContenedor>();

            usua.Setup(o => o.usuarioLogin(null, null)).Returns(new Usuario { Email = null, Password = null });
            var controller = new ClienteController(null, usua.Object, null);

            var view = controller.Login(null, null);

            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        public void RegistrarUsuarioYaExiste()
        {
            // string email = "josue62008@gmail.com";
            Usuario usuario = new Usuario()
            {
                Id = 5,
                Nombre = "ricardo",
                Apellidos = "epiquien",
                Username = "ricardoE",
                Email = "ricardo.E@gmail.com",
                Password = "123",
                RolId = 2
            };

            var user = new Mock<IUsuarioContenedor>();
            user.Setup(o => o.usuarioExiste(usuario.Email)).Returns(true);

            var controller = new ClienteController(null, user.Object, null);
            var view = controller.Registrar(usuario);

            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        public void Detalles()
        {
            var detalles = new Mock<IVentaContenedor>();
            detalles.Setup(o => o.Facturadecliente(2));

            var controller = new ClienteController(null, null, detalles.Object);
            var result = controller.Detalles(2);

            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Test]
        public void DetalleNuevos()
        {
            var detalles = new Mock<IVentaContenedor>();
            detalles.Setup(o => o.Facturadecliente(2));

            var controller = new ClienteController(null, null, detalles.Object);
            var result = controller.Detalles(2);

            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Test]
        public void MarcarComoRecibido()
        {
            var comoRecibido = new Mock<IVentaContenedor>();
            comoRecibido.Setup(o => o.marcarComoRecibido(2));

            var controller = new ClienteController(null, null, comoRecibido.Object);
            var result = controller.MarcarComoRecibido(2);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
        [Test]
        public void MarcarComoRecibidoFalla()
        {
            var comoRecibido = new Mock<IVentaContenedor>();
            comoRecibido.Setup(o => o.marcarComoRecibido(0));

            var controller = new ClienteController(null, null, comoRecibido.Object);
            var result = controller.MarcarComoRecibido(0);

            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Test]
        public void HistoriaFalla()
        {


            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x => x.Identity);
            mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);


            var historia = new Mock<IUsuarioContenedor>();
            historia.Setup(o => o.hisoriaClaims(mockPrincipal.Object));

            //var clienthistoria = new Mock<IUsuarioContenedor>();
            //clienthistoria.Setup(o=>o.clienteHistoria(user));

            var venta = new Mock<IVentaContenedor>();
            venta.Setup(o => o.listaHistoria(new Usuario { }));

            var controller = new ClienteController(null, historia.Object, venta.Object);
            var result = controller.Historia();

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void processClienteFacturas()
        {
            var factura = new Mock<IVentaContenedor>();
            factura.Setup(o => o.procesFactura(3));

            var control = new ClienteController(null, null, factura.Object);
            var resul = control.Process(3);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }
    }
}
