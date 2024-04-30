using CleanArchitecture.Infraestructure.IoC;
using CleanArchitecture.MinimalApi;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, builder =>
    {
        builder.WithOrigins(configuration.GetValue<string>("APP_URL_REACT")).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthorization();
builder.Services.AddTokenAuthentication(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProject", Version = "v1.0.0" });

        var securitySchema = new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };

        c.AddSecurityDefinition("Bearer", securitySchema);

        var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

        c.AddSecurityRequirement(securityRequirement);
    }
);
DependencyContainer.RegisterServices(builder.Services, configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(myAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapUsuarios(configuration);
app.MapLibro(configuration);
app.Run();

