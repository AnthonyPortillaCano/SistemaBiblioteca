using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infraestructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BibliotecaContext _context;
        public UnitOfWork(BibliotecaContext context)
        {
            _context = context;
            usuarioServices = new UsuarioServices(context);
            libroServices = new LibroServices(context);
        }
        public IUsuarioServices usuarioServices { get; private set; }

        public ILibroServices libroServices { get; private set; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
