﻿using CleanArchitecture.Application.Interfaces;
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
    public  class UsuarioServices(DbContext context) : IUsuarioServices
    {
        protected IUsuarioRepository _usuarioRepository { get; set; } = new UsuarioRepository(context);

        public async Task<Usuario> ValidarUsuario(Usuario usuario)
        {
            return  await _usuarioRepository.ValidateUser(usuario);
        }
        public async Task<int> Insertar(Usuario usuario)
        {
            return await _usuarioRepository.Insertar(usuario);
        }
    }
}
