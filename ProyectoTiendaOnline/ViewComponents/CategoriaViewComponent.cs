using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.ViewComponents
{
    [ViewComponent(Name = "Categoria")]
    public class CategoriaViewComponent : ViewComponent
    {
        private DataBaseContext _db;
        public CategoriaViewComponent(DataBaseContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Categoria> categorias = _db.Categorias.Where(c => c.Parent == null).Include(p => p.InverseParents).ToList();
            return View("Index", categorias);
        }
    }
}
