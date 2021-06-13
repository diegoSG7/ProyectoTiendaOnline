using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProyectoTiendaOnline.Areas.Vendedor.Controllers;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoTiendaOnline.Tests.ControllersTests
{
   public  class CategoriaControllerTest
    {
        [Test]
        public void indexCategoria()
        {
            var categoriaMock = new Mock<ICategoriaContenedor>();
            categoriaMock.Setup(o => o.listaCatergoria());

            var control = new CategoriaController(categoriaMock.Object);
            var view = control.Index();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void AgregarCategoria()
        {
            var categoria = new Categoria()
            {
                Id = 2,
                Nombre = "Arte",
                ParentId = 1
            };

            var categoriaMock = new Mock<ICategoriaContenedor>();
            categoriaMock.Setup(o => o.categoiaNuevo(categoria));

            var control = new CategoriaController(categoriaMock.Object);
            var resul = control.Agregar(categoria);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }

        [Test]
        public void AgregarCategoriaNueva()
        {
            var categoria = new Categoria()
            {
                Id = 3,
                Nombre = "Ropa",
                ParentId = 1
            };

            var categoriaMock = new Mock<ICategoriaContenedor>();
            categoriaMock.Setup(o => o.categoiaNuevo(categoria));

            var control = new CategoriaController(categoriaMock.Object);
            var resul = control.Agregar(categoria);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }
        [Test]
        public void eliminarCategoriaFalla()
        {
            int id = 0;
            var categoriaMock = new Mock<ICategoriaContenedor>();
            categoriaMock.Setup(o => o.eliminarCategoria(id));

            var control = new CategoriaController(categoriaMock.Object);
            var resul = control.Elimiar(id);

            Assert.IsInstanceOf<ViewResult>(resul);
        }
        [Test]
        public void eliminarCategoria()
        {
            int id = 2;
            var categoriaMock = new Mock<ICategoriaContenedor>();
            categoriaMock.Setup(o => o.eliminarCategoria(id));

            var control = new CategoriaController(categoriaMock.Object);
            var resul = control.Elimiar(id);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }
        [Test]
        public void eliminarCategoriaNueva()
        {
            int id = 3;
            var categoriaMock = new Mock<ICategoriaContenedor>();
            categoriaMock.Setup(o => o.eliminarCategoria(id));

            var control = new CategoriaController(categoriaMock.Object);
            var resul = control.Elimiar(id);

            Assert.IsInstanceOf<RedirectToActionResult>(resul);
        }
        [Test]
        public void editarCategoria()
        {
            var id = 2;
            var categoria = new Categoria()
            {
                Id = 2,
                Nombre = "Arte",
                ParentId = 1
            };
            var categoriaMock = new Mock<ICategoriaContenedor>();
            categoriaMock.Setup(o => o.editarCategoria(id, categoria));

            var control = new CategoriaController(categoriaMock.Object);
            var result = control.Editar(id, categoria);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
        [Test]
        public void AgregarSubCategoria()
        {
            var subcategoria = new Categoria()
            {
                ParentId = 1
            };

            var categoriaMock = new Mock<ICategoriaContenedor>();
            categoriaMock.Setup(o => o.agregarSubCategoria(subcategoria));

            var control = new CategoriaController(categoriaMock.Object);
            var result = control.AgregarSubCategoria(subcategoria);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
