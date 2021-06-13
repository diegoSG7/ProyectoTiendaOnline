using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTiendaOnline.Areas.Vendedor.Controllers
{
    [Authorize(Roles = "Cliente", AuthenticationSchemes = "Schema_Vendor")]
    [Area("vendedor")]
    [Route("vendedor/dashboard")]
    public class DashBoardController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
