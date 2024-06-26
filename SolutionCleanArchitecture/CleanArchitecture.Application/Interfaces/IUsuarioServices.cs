﻿using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IUsuarioServices
    {
        Task<Usuario> ValidarUsuario(Usuario usuario);

        Task<int> Insertar(Usuario usuario);
    }
}
