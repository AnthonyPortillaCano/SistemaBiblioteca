using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.MinimalApi
{
    public static class UsuarioEndpoint
    {
        public static RouteGroupBuilder MapUsuarios(this WebApplication app, IConfiguration configuration)
        {
            var group = app.MapGroup("Api/Usuario");
            group.MapPost("/ValidarUsuario", async (Usuario usuario, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    ITokenService tokenService = new TokenService(configuration);
                    if (usuario == null) Results.BadRequest();
                    var credencial = await unitOfWork.usuarioServices.ValidarUsuario(usuario);
                    if (credencial != null)
                    {
                        var token = tokenService.BuildToken(usuario);
                        usuario.Token = token;
                        return Results.Ok(usuario);
                    }
                    else
                    {
                        return Results.NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/Insertar", async (Usuario usuario, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    if (usuario == null) Results.BadRequest();
                    var credencial = await unitOfWork.usuarioServices.Insertar(usuario);
                    if (credencial >0)
                    {
                        return Results.Ok(usuario);
                    }
                    else
                    {
                        return Results.BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            return group;
        }
    }
}
