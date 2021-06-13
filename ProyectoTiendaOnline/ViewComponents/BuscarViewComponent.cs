using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.ViewComponents
{
    [ViewComponent(Name = "Buscar")]
    public class BuscarViewComponent : ViewComponent
    {
        private DataBaseContext _db;
        public BuscarViewComponent(DataBaseContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Producto> productos = _db.Productos.ToList();
            return View("Index", productos);

        }
    }
}
