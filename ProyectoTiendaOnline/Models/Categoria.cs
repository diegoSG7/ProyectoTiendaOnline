using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int? ParentId { get; set; }

        public virtual Categoria Parent { get; set; }

        public virtual ICollection<Categoria> InverseParents { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

    }
}
