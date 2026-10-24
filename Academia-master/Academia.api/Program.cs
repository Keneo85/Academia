using Academia.API.Data;
using Academia.API.Endpoints;
using Academia.API.Middleware;
using Academia.Business;
using Academia.DataAccess;
using Academia.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Base de Datos
builder.Services.AddDbContext<AcademiaDbContext>(p =>
{
    p.UseSqlServer(builder.Configuration.GetConnectionString("DbAcademia"));
});

// Autenticacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();

// CORS: solo necesario si el front NO usa proxy (por ejemplo, el build publicado)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDev", policy => policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Inyeccion de dependencias
builder.Services
    .AddBusiness()
    .AddRepositories();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Academia API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingresa solo el token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var app = builder.Build();

// Datos iniciales (admin + especialidades)
await DbSeeder.SeedAsync(app.Services);

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Academia API v1"));
}

app.UseCors("AngularDev");
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapGroup("/api/auth").MapAuthEndpoints().WithTags("Auth");
app.MapGroup("/api/alumnos").MapAlumnoEndpoints().WithTags("Alumnos");
app.MapGroup("/api/cursos").MapCursoEndpoints().WithTags("Cursos");
app.MapGroup("/api/matriculas").MapMatriculaEndpoints().WithTags("Matriculas");
app.MapGroup("/api/especialidades").MapEspecialidadEndpoints().WithTags("Especialidades");

app.Run();
