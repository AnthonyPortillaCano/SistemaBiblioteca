using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Data.Repositories
{
    public class UsuarioRepository(DbContext context) : RepositoryEF<Usuario>(context), IUsuarioRepository
    {
        public IRepositoryEF<Usuario> RepositoryEF { get; set; } = new RepositoryEF<Usuario>(context);

        public async Task<Usuario> ValidateUser(Usuario user)
        {
            Usuario result = await RepositoryEF.Obtener<Usuario>(a => a.Email == user.Email && a.Clave == user.Clave);
            return result;
        }
        public async Task<int> Insertar(Usuario usuario)
        {
            return await RepositoryEF.Insert(usuario);
        }
    }
}
