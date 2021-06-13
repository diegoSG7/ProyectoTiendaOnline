using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProyectoTiendaOnline.Areas.Vendedor.Controllers;
using ProyectoTiendaOnline.Contenedor;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoTiendaOnline.Tests.ControllersTests
{
    class VientaControllerTest
    {
        [Test]
        public void VEntaDetalle()
        {
            var deta = new Mock<IVentaContenedor>();
            deta.Setup(o => o.ventaDeTalle(2));

            var control = new VentaController(null, deta.Object);
            var resul = control.Detalles(2);

            Assert.IsInstanceOf<ViewResult>(resul);
        }
        [Test]
        public void entaCambiarEstadoDeenvioFalla()
        {
            var process = new Mock<IVentaContenedor>();
            process.Setup(o => o.ventaProcess(0));

            //var guarda = new Mock<IVentaContenedor>();
            //guarda.Setup(o => o.guardarVentaProcess(process.Object));

            var control = new VentaController(null, process.Object);
            var resul = control.Process(0);

            Assert.IsInstanceOf<ViewResult>(resul);
        }
        [Test]
        public void VentaCambiarEstadoDeenvio()
        {
            var process = new Mock<IVentaContenedor>();
            process.Setup(o => o.ventaProcess(3));

            //var guarda = new Mock<IVentaContenedor>();
            //guarda.Setup(o => o.guardarVentaProcess(process.Object));

            var control = new VentaController(null, process.Object);
            var resul = control.Process(3);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }

        [Test]
        public void loginNull()
        {
            var login = new Mock<IUsuarioContenedor>();
            login.Setup(o => o.usuarioLogin(null, null));

            var control = new LoginController(null, login.Object);
            var resul = control.Process(null, null);

            Assert.IsInstanceOf<ViewResult>(resul);
        }
    }
}
