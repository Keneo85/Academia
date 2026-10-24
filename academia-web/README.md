# Academia Web

Frontend del sistema **Academia**, una app para gestionar cursos, alumnos y matrículas. Está hecho en **Angular 22** con **Angular Material** y consume una API REST en **.NET 10 (Minimal API)**.

Lo armé como proyecto de práctica para trabajar con lo más nuevo de Angular: componentes standalone, signals, Signal Forms, `httpResource` y guards/interceptors funcionales.

## Funcionalidades

- Login y registro con JWT (el token se guarda en el navegador y se envía en cada petición).
- Roles **Admin** y **Alumno**, con rutas y botones que se muestran según el rol.
- **Cursos:** listado paginado con búsqueda, detalle, crear, editar y eliminar.
- **Alumnos:** listado con búsqueda por nombre, apellido o DNI y registro de nuevos alumnos (solo Admin).
- **Matrículas:** registro con varios cursos y total calculado al momento, listado y detalle. El alumno solo ve sus propias matrículas.
- Páginas de error 403 y 404.

## Tecnologías

| | |
|---|---|
| Framework | Angular 22 (standalone, signals) |
| UI | Angular Material 22 (Material 3) |
| Formularios | Signal Forms |
| HTTP | `HttpClient`, `httpResource`, interceptor funcional |
| Tests | Vitest |
| Backend | .NET 10 Minimal API + SQL Server |

## Requisitos

- Node.js 24.15 o superior (también funciona con 22.22.3+)
- La API de Academia corriendo en `http://localhost:5046`

## Cómo ejecutarlo

```bash
npm install
npm start
```

La app abre en `http://localhost:4200`.

En desarrollo, las llamadas a `/api` pasan por `proxy.conf.json` hacia la API, así que no hace falta configurar CORS. Si tu API corre en otro puerto, cambia el `target` en ese archivo y vuelve a ejecutar `npm start`.

Para generar la versión de producción:

```bash
npm run build
```

La URL de la API para producción está en `src/environments/environment.ts`.

## Usuarios y roles

Al registrarse desde la app, el usuario se crea con rol **Alumno**. Para tener un **Admin** puedes cambiar el rol directamente en la base de datos:

```sql
UPDATE [User] SET Rol = 'Admin' WHERE UserName = 'tu_usuario';
```

Después cierra sesión y vuelve a entrar, porque el rol viene dentro del token.

## Estructura del proyecto

```
src/app/
├── core/        servicios, modelos, guards, interceptor y rutas de la API
├── shared/      componentes reutilizables (page-header, empty-state, confirm-dialog) y directiva de roles
├── layout/      shell, toolbar y menú lateral
└── features/    una carpeta por módulo: auth, cursos, alumnos, matriculas, errors
```

Cada módulo de `features/` tiene su propio archivo de rutas y se carga con lazy loading.

## Pantallas

| Ruta | Acceso | Descripción |
|---|---|---|
| `/login`, `/registro` | Público | Iniciar sesión y crear cuenta |
| `/cursos` | Todos | Listado y búsqueda de cursos |
| `/cursos/:id` | Todos | Detalle del curso |
| `/cursos/nuevo`, `/cursos/:id/editar` | Admin | Crear y editar cursos |
| `/alumnos` | Admin | Listado de alumnos |
| `/alumnos/nuevo` | Admin | Registrar alumno |
| `/matriculas` | Todos | Admin ve todas, Alumno ve las suyas |
| `/matriculas/nueva` | Admin | Registrar matrícula |
| `/matriculas/:id` | Todos | Detalle de la matrícula |
