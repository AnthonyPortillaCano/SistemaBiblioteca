using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public interface ILibroServices
    {
        Task<List<Libro>> Listar();
        Task<Libro> ConsultarPorId(int id);
        Task<int> Insertar(Libro libro);
        void Eliminar(Libro libro);

        void UpdateFieldsSave(Libro libro);
    }
}
