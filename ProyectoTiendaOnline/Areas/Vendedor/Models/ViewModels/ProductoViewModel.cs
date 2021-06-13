using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Areas.Vendedor.Models.ViewModels
{
    public class ProductoViewModel
    {
        public Producto Producto { get; set; }
        public List<SelectListItem> Categorias { get; set; }
    }
}
