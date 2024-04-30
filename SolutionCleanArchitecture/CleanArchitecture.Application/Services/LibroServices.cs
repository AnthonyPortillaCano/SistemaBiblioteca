using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class LibroServices(DbContext context) : ILibroServices
    {
        protected ILibroRepository _libroRepository { get; set; } = new LibroRepository(context);

        public async Task<List<Libro>> Listar()
        {
            return await _libroRepository.ListarLibros();
        }
        public async Task<Libro> ConsultarPorId(int id)
        {
            return await _libroRepository.ObtenerLibroPorId(id);
        }

        public async Task<int> Insertar(Libro libro)
        {
            return await _libroRepository.Insertar(libro);
        }
        public void UpdateFieldsSave(Libro libro)
        {
            _libroRepository.UpdateFieldsSave(libro);
        }
        public  void Eliminar(Libro libro) 
        {
              _libroRepository.Delete(libro);
        }
    }
}
