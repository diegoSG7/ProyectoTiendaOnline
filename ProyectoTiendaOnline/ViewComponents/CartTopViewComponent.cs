using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Helpers;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.ViewComponents
{
    [ViewComponent(Name = "CartTop")]
    public class CartTopViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (SessionHelper.GetObjetFromJson<List<Item>>(HttpContext.Session, "carrito") == null)
            {
                ViewBag.countItems = 0;
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjetFromJson<List<Item>>(HttpContext.Session,
                    "carrito");
                ViewBag.countItems = cart.Count;
            }
            return View("Index");
        }
    }
}
