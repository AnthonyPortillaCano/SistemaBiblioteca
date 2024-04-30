﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUsuarioServices usuarioServices { get; }

        ILibroServices libroServices { get; }

        Task<int> CommitAsync();
    }
}
