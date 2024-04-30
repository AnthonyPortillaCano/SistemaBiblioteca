using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Models
{

    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Clave { get; set; }

        public string Token { get; set; } = string.Empty;
        

    }
}
