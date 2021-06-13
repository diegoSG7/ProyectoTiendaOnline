using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Services
{
    public class FotoService : IFotoContenedor
    {
        private DataBaseContext _db;
        public FotoService(DataBaseContext _db)
        {
            this._db = _db;
        }

        public void agregarfoto(Foto foto)
        {
            _db.Fotos.Add(foto);
            _db.SaveChanges();
        }

        public void eliminarFoto(Foto foto)
        {
            _db.Fotos.Remove(foto);
            _db.SaveChanges();
        }

        public Foto fotoNew(int id)
        {
            var foto = new Foto()
            {
                ProductoId = id
            };
            return foto;
        }

        public Foto getIdFind(int id)
        {
            return _db.Fotos.Find(id);
        }

        public List<Foto> listafoto(int id)
        {
            return _db.Fotos.Where(c => c.ProductoId == id).ToList();
        }
    }
}
