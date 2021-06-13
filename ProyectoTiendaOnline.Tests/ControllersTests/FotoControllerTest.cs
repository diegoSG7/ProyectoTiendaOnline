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
    public class FotoControllerTest
    {
        [Test]
        public void indexFoto()
        {
            int id = 1;
            var fotoMock = new Mock<IFotoContenedor>();
            fotoMock.Setup(o => o.listafoto(id));

            var prodMock = new Mock<IProductoContenedor>();
            prodMock.Setup(o => o.priodcutoFind(id));

            var control = new FotoController(null, null, fotoMock.Object, prodMock.Object);
            var resul = control.Index(id);

            Assert.IsInstanceOf<ViewResult>(resul);
        }


        //public void guardar()
        //{
        //    var foto = new Foto()
        //    {
        //        Id=2,
        //        Nombre=" 10262020104315camaraSegur.PNG",
        //        Destacado=true,
        //        ProductoId=2
        //    };
        //    var fotoNue = new Mock<IFotoContenedor>();
        //    fotoNue.Setup(o=>o.agregarfoto(foto));

        //    var file = new Mock<IFormFile>();
        //    file.Setup(o=>o.OpenReadStream());


        //    var control = new FotoController(null,null, fotoNue.Object, null);
        //    var resul = control.Agregar(1, foto,file.Object);

        //    Assert.IsNotInstanceOf<RedirectToActionResult>(resul);
        //}
        [Test]
        public void eliminarFoto()
        {
            var idFoto = new Mock<IFotoContenedor>();
            idFoto.Setup(o => o.getIdFind(2));

            var control = new FotoController(null, null, idFoto.Object, null);
            var resul = control.Eliminar(2, 2);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }

        [Test]
        public void eliminarFotoFalla()
        {
            var idFoto = new Mock<IFotoContenedor>();
            idFoto.Setup(o => o.getIdFind(0));

            var control = new FotoController(null, null, idFoto.Object, null);
            var resul = control.Eliminar(0, 2);

            Assert.IsInstanceOf<ViewResult>(resul);
        }
    }
}
