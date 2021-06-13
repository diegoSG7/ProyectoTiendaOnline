using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Contenedor
{
    public interface IUsuarioContenedor
    {
        bool usuarioExiste(string email);
        void guardarUsuario(Usuario usuario);

        Usuario usuarioLogin(string email, string password);

        Usuario clienteHistoria(Claim user);
        Claim hisoriaClaims(ClaimsPrincipal User);
    }
}
