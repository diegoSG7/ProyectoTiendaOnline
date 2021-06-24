using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Models
{
    public class FacturaDetalle
    {
        public int Id { get; set; }

        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

    }
}
