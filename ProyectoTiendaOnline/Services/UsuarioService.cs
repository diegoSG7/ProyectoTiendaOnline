using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Services
{
    public class UsuarioService : IUsuarioContenedor
    {
        private DataBaseContext _db;

        public UsuarioService(DataBaseContext _db)
        {
            this._db = _db;
        }

        public Usuario clienteHistoria(Claim user)
        {
            return _db.Usuarios.Include(p => p.Facturas).ThenInclude(y => y.Producto).ThenInclude(y => y.Fotos).SingleOrDefault(a => a.Username.Equals(user.Value));

        }

        public void guardarUsuario(Usuario usuario)
        {
            _db.Usuarios.Add(usuario);
            usuario.RolId = 2;
            _db.SaveChanges();

        }

        public Claim hisoriaClaims(ClaimsPrincipal User)
        {
            return User.FindFirst(ClaimTypes.Name);
        }

        public bool usuarioExiste(string email)
        {
            if (_db.Usuarios.Any(u => u.Email == email))
                return true;

            return false;
        }

        public Usuario usuarioLogin(string email, string password)
        {
            var user = _db.Usuarios.Include(c => c.Rol).Where(o => o.Email == email && o.Password == password &&
            o.RolId == 2).FirstOrDefault();

            return user;
        }
    }
}
