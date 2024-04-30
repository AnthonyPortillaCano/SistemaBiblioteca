using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Data.Repositories
{
    public class LibroRepository : RepositoryEF<Libro>, ILibroRepository
    {
        public LibroRepository(DbContext context) : base(context)
        {
            repositoryEF = new RepositoryEF<Libro>(context);
        }
        public IRepositoryEF<Libro> repositoryEF { get; set; }

        public async Task<List<Libro>> ListarLibros()
        {
            return await repositoryEF.GetAll();
        }
        public async Task<Libro> ObtenerLibroPorId(int id)
        {
            return await repositoryEF.GetById(id);
        }
        public async Task<int> Insertar(Libro libro)
        {
            return await repositoryEF.Insert(libro);
        }
        public void Delete(Libro libro)
        {

            repositoryEF.Delete(libro);
        }

        public void UpdateFieldsSave(Libro libro)
        {
            repositoryEF.UpdateFieldsSave(libro, b => b.DisponibleParaPrestamo);
        }
    }
}
