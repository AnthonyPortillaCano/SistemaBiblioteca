using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface ILibroRepository
    {
        Task<List<Libro>> ListarLibros();
        Task<Libro> ObtenerLibroPorId(int id);
        Task<int> Insertar(Libro libro);
        void Delete(Libro libro);
        void UpdateFieldsSave(Libro libro);
    }
}
