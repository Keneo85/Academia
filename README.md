# Academia API

API REST desarrollada en **.NET 10** con arquitectura en capas para la gestión académica de alumnos, cursos y matrículas.

## 🏗️ Arquitectura

```
Academia.API          → Minimal APIs, Endpoints, Middleware
Academia.Business     → Servicios, DTOs, Interfaces
Academia.Repositories → Repository Pattern
Academia.DataAccess   → EF Core, Entidades, Configuraciones
Academia.Common       → Helpers compartidos (Result Pattern)
```

## 🚀 Tecnologías

- .NET 10 / C#
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQL Server
- Docker
- JWT Authentication
- Mapster
- BCrypt
- Swagger / OpenAPI

## 📋 Endpoints

### Auth
- `POST /api/auth/login` — Iniciar sesión

### Alumnos
- `POST /api/alumnos` — Registrar alumno

### Cursos
- `GET  /api/cursos` — Listar cursos
- `GET  /api/cursos/{id}` — Detalle de curso
- `POST /api/cursos` — Crear curso
- `PUT  /api/cursos/{id}` — Actualizar curso
- `DELETE /api/cursos/{id}` — Eliminar curso

### Matrículas
- `POST /api/matriculas` — Registrar matrícula

## ⚙️ Cómo ejecutar

1. Clonar el repositorio
2. Levantar SQL Server con Docker:
```bash
docker-compose up sqlserver -d
```
3. Ejecutar migraciones:
```
Update-Database -Project Academia.DataAccess -StartupProject Academia.api
```
4. Ejecutar la API:
```bash
docker-compose up -d
```
5. Abrir Swagger:
```
http://localhost:5046/swagger/index.html
```

## 🔐 Autenticación

La API usa JWT. Para obtener el token:

1. Hacer POST a `/api/auth/login`
2. Copiar el token de la respuesta
3. En Swagger click en **Authorize** y escribir: `Bearer {token}`
