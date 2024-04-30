using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Autor { get; set; }
        public bool DisponibleParaPrestamo { get; set; } = true;
    }
}
