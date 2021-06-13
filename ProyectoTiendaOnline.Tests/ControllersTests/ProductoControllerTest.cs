using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProyectoTiendaOnline.Areas.Vendedor.Controllers;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ProyectoTiendaOnline.Tests.ControllersTests
{
   public class ProductoControllerTest
    {
        [Test]
        public void indexProductoCreadoList()
        {
            var procudtoMock = new Mock<IProductoContenedor>();
            procudtoMock.Setup(o=>o.listaProductoCreado());

            var control = new ProductoController(null, procudtoMock.Object);
            var result = control.Index();

            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Test]
        public void eliminarProductoCReado()
        {
            var producto = new Producto()
            {
                Id=2,
                Nombre="Camara de seguridad ",
                Detalles= "Color: Blanco Batería de gran capacidad y antena mejorada: la cámara exterior tiene una batería integrada de 10400 mAh, que se puede utilizar durante 3 a 6 meses(despertar 1500 veces) después de estar completamente cargada.Cámara WiFi mejorada con antena mejorada con antenas inalámbricas avanzadas para recibir señales wifi más fuertes. (Solo soporta 2,4 GHz, no es compatible con wifi de 0.18 oz)",
                Descripcion= "Conico Cámara de seguridad para exteriores, inalámbrica, recargable a batería, 10400 mAh, 1080P, WiFi, cámara de vigilancia para el hogar con visión nocturna, audio de dos vías, detección de movimiento PIR, IP65 impermeable",
                Precio=50,
                Cantidad=20,
                Puntaje=5
            };
            var id=2;

            var productMock = new Mock<IProductoContenedor>();
            productMock.Setup(o=>o.eliminarProductoCreado(producto));

            var idProduc = new Mock<IProductoContenedor>();
            idProduc.Setup(o=>o.priodcutoFind(id));

            var prod = new ProductoController(null, productMock.Object);
            var resul = prod.Eliminar(id);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }

        [Test]
        public void RealizarUnaCompraFalla()
        {
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x => x.Identity);
            mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);


            var check = new Mock<IUsuarioContenedor>();
            check.Setup(o => o.hisoriaClaims(mockPrincipal.Object));

            var control = new Controllers.ProductoController(null, null, check.Object, null, null);
            var resul = control.CheckOut(2, 5);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }
    }
}
