using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.MinimalApi
{
    public static class LibroEndpoint
    {
        public static RouteGroupBuilder MapLibro(this WebApplication app, IConfiguration configuration)
        {
            var group = app.MapGroup("Api/Libros");


            group.MapGet("/ObtenerTodos", [Authorize] async (IUnitOfWork unitOfWork) =>
            {
                try
                {
                    var result = await unitOfWork.libroServices.Listar();
                    if (result != null)
                    {
                        return Results.Ok(result);
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

            group.MapGet("/ObtenerPorId/{id}", [Authorize] async (int id,IUnitOfWork unitOfWork) =>
            {
                try
                {
                    var result = await unitOfWork.libroServices.ConsultarPorId(id);
                    if (result != null)
                    {
                        return Results.Ok(result);
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
            group.MapPost("/Agregar",[Authorize] async (Libro libro, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    
                    if (libro == null) Results.BadRequest();
                    var result = await unitOfWork.libroServices.Insertar(libro);
                    if (result != null)
                    {
                        return Results.Ok(result);
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

            group.MapDelete("/Eliminar/{id}", [Authorize]  async (int id, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    if (id <= 0) Results.BadRequest();
                    var result = await unitOfWork.libroServices.ConsultarPorId(id);
                    if(result == null) Results.NotFound();
                    unitOfWork.libroServices.Eliminar(result);
                    var cantidad=await unitOfWork.CommitAsync();
                    if(cantidad<=0) Results.BadRequest();
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });


            group.MapPut("/Prestar", [Authorize]  async (Libro libro, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    if (libro.Id <= 0) Results.BadRequest();
                    var result = await unitOfWork.libroServices.ConsultarPorId(libro.Id);
                    if (result == null) Results.NotFound();

                    if (result.DisponibleParaPrestamo)
                    {
                        result.DisponibleParaPrestamo = false;
                        unitOfWork.libroServices.UpdateFieldsSave(result);
                        await unitOfWork.CommitAsync();
                        return Results.Ok(result);
                    }
                    else
                    {
                        return Results.BadRequest("El libro ya está prestado.");
                    }
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPut("/Devolver", [Authorize]  async (Libro libro, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    if (libro.Id <= 0) Results.BadRequest();
                    var result = await unitOfWork.libroServices.ConsultarPorId(libro.Id);
                    if (result == null) Results.NotFound();

                    if (!result.DisponibleParaPrestamo)
                    {
                        libro.DisponibleParaPrestamo = true;
                        unitOfWork.libroServices.UpdateFieldsSave(libro);
                        await unitOfWork.CommitAsync();
                        return Results.Ok(result);
                    }
                    else
                    {
                        return Results.BadRequest("El libro no estaba prestado.");
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
