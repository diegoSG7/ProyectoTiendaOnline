using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Contenedor
{
    public interface IFotoContenedor
    {
        //no es del prro
        List<Foto> listafoto(int id);
        void agregarfoto(Foto foto);
        Foto fotoNew(int id);
        void eliminarFoto(Foto foto);
        Foto getIdFind(int id);
    }
}
