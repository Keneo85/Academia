# Academia Web (Angular 22)

Frontend para la API **Academia** (.NET 10 Minimal API). UI con **Angular Material 22** (Material 3). Estructura por capas: `core/` (servicios, modelos, guards...), `shared/` (componentes reutilizables), `layout/` (toolbar + menú lateral) y `features/` (una carpeta por módulo con su propio archivo de rutas).

## 1. Requisitos
- **Node.js 24.15+** (o 22.22.3+). Angular CLI 22 no arranca con versiones anteriores → `node -v`
- La API corriendo (Docker: `http://localhost:1600`).

## 2. Preparar la API
**Mínimo:** solo el endpoint `POST /api/auth/register` (paso 4.1 de la guía
**[Academia API: código que falta](https://claude.ai/artifact/KwHE8kKHvrqLHaGM9knhFD)**). Con eso ya puedes
registrarte, iniciar sesión (el login devuelve el JWT y el front lo guarda) y ver los cursos.

**Opcional:** el resto de la guía (GET de alumnos, matrículas y especialidades, seeder del admin, CORS).
Si esos GET no existen, el front no se rompe: pide el ID de especialidad o de alumno en un campo de número
en lugar del selector, y las listas de alumnos y matrículas muestran un aviso.

> El registro crea usuarios con rol **Alumno**. Para crear cursos, alumnos o matrículas necesitas un **Admin**:
> regístrate y luego en SQL Server ejecuta `UPDATE [User] SET Rol = 'Admin' WHERE UserName = 'tu_usuario';`
> y vuelve a iniciar sesión (el rol va dentro del token).

## 3. Ejecutar
```bash
npm install
npm start          # = ng serve  ->  http://localhost:4200  (usa proxy.conf.json)
```
- API en otro puerto (por ejemplo Visual Studio, 5046): cambia `target` en `proxy.conf.json`.
- `ng build` usa `src/environments/environment.ts` (`apiUrl` completo). `ng serve` usa `environment.development.ts` (`apiUrl: ''`, pasa por el proxy).

## 4. Pantallas
| Ruta | Rol | Qué hace | Endpoint |
|---|---|---|---|
| `/login`, `/registro` | público | Login / crear cuenta de alumno | `POST /api/auth/login`, `POST /api/auth/register` |
| `/cursos` | todos | Listado paginado con búsqueda mientras escribes; eliminar con diálogo (Admin) | `GET /api/cursos?search=`, `DELETE /api/cursos/{id}` |
| `/cursos/:id` | todos | Detalle del curso | `GET /api/cursos/{id}` + especialidades |
| `/cursos/nuevo`, `/cursos/:id/editar` | Admin | Crear / editar con selector de especialidad | `POST`, `PUT`, `GET /api/especialidades` |
| `/alumnos` | Admin | Listado con búsqueda (Enter) por nombre, apellido o DNI | `GET /api/alumnos?search=` |
| `/alumnos/nuevo` | Admin | Registrar alumno + usuario | `POST /api/alumnos` |
| `/matriculas` | Admin / Alumno | Admin: todas (filtro `?alumnoId=`). Alumno: "Mis matrículas" | `GET /api/matriculas`, `GET /api/matriculas/mias` |
| `/matriculas/nueva` | Admin | Selector de alumno y cursos, total en vivo | `POST /api/matriculas` |
| `/matriculas/:id` | todos | Detalle con subtotales y total | `GET /api/matriculas/{id}` |
| `/403`, cualquier otra | | Sin permiso / no encontrada | |

## 5. Estructura
```
src/
├── environments/                 environment.ts (build) / environment.development.ts (serve)
└── app/
    ├── app.config.ts             providers: router + HttpClient + interceptor
    ├── app.routes.ts             rutas públicas, Shell protegido con loadChildren por feature, 404
    ├── core/                     lo que existe una sola vez en la app
    │   ├── constants/api-routes.constants.ts   todas las URLs de la API
    │   ├── models/               interfaces = DTOs de .NET (auth, curso, alumno, matricula, api-result)
    │   ├── services/             auth, curso, alumno, matricula, notification (snackbar)
    │   ├── interceptors/         auth.interceptor.ts  -> "Bearer token"; 401 -> logout
    │   ├── guards/               authGuard, guestGuard, roleGuard('Admin')
    │   └── utils/api-error.ts    saca el mensaje de error del Result de .NET
    ├── shared/                   piezas reutilizables sin lógica de negocio
    │   ├── components/           page-header, empty-state, confirm-dialog
    │   └── directives/           *appHasRole="'Admin'"
    ├── layout/                   shell (contenedor) + toolbar (arriba) + sidenav (menú lateral)
    └── features/                 una carpeta por módulo; cada página = .ts + .html (+ .scss)
        ├── auth/                 login/, register/
        ├── cursos/               cursos.routes.ts, list/, form/, detail/
        ├── alumnos/              alumnos.routes.ts, form/
        ├── matriculas/           matriculas.routes.ts, form/
        └── errors/               forbidden/ (403), notfound/ (404)
```
**Regla rápida:** si lo usa toda la app y no tiene pantalla, va en `core/`. Si es un componente que se repite, va en `shared/`. Si es una pantalla, va en `features/<módulo>/`.

## 6. Chuleta de lo que se usa (Angular 22)

| Concepto | Dónde verlo | Sintaxis clave |
|---|---|---|
| Signal | `auth.service.ts` | `x = signal(0)` · leer `x()` · `x.set(1)` · `x.update(v => v + 1)` |
| computed | `auth.service.ts` | `isAdmin = computed(() => this.role() === 'Admin')` |
| inject() | todos | `private http = inject(HttpClient);` (en vez del constructor) |
| Control flow | templates | `@if (...) {} @else {}` · `@for (x of lista; track x.id) {} @empty {}` |
| httpResource | `cursos-list.ts` | `httpResource<T>(() => ({ url, params: { page: this.page() } }))` → `.value()`, `.isLoading()`, `.reload()` |
| Signal Forms | `features/*/form/*.page.ts` | `f = form(this.model, p => { required(p.nombre); })` · `<input [formField]="f.nombre">` · `f.nombre().invalid()` |
| submit() | formularios | `submit(this.f, async () => { ... })` solo corre si es válido y marca todo como touched |
| Validación propia | `register.ts` | `validate(p.confirm, ({ value, valueOf }) => value() === valueOf(p.password) ? undefined : { kind: 'mismatch', message: '...' })` |
| Arrays en forms | `matricula-form.ts` | `applyEach(p.detalles, d => ...)` · `@for (d of f.detalles; track $index)` |
| Interceptor funcional | `auth.interceptor.ts` | `HttpInterceptorFn = (req, next) => next(req.clone({ setHeaders: {...} }))` |
| Guard funcional | `auth.guard.ts` | `CanActivateFn = () => cond ? true : inject(Router).createUrlTree(['/login'])` |
| Lazy loading | `app.routes.ts` | `loadComponent: () => import('./x').then(m => m.X)` |
| Rutas por feature | `cursos.routes.ts` | `export const CURSOS_ROUTES: Routes = [...]` y en app.routes: `loadChildren: () => import('./features/cursos/cursos.routes').then(m => m.CURSOS_ROUTES)` |
| Guard con parámetro | `role.guard.ts` | `roleGuard = (role: Role): CanActivateFn => () => ...` · uso `canActivate: [roleGuard('Admin')]` |
| rxResource | `cursos-detail.page.ts` | `rxResource({ params: () => this.id(), stream: ({ params }) => this.service.getById(params) })` |
| input con transform | `cursos-detail.page.ts` | `id = input.required({ transform: numberAttribute })` |
| output() | `toolbar.component.ts` | `toggleMenu = output<void>()` · `this.toggleMenu.emit()` · padre: `(toggleMenu)="..."` |
| Directiva estructural | `has-role.directive.ts` | `input()` + `effect()` + `viewContainer.createEmbeddedView(templateRef)` · uso `*appHasRole="'Admin'"` |
| Query params como input | `matriculas-list.page.ts` | `[queryParams]="{ alumnoId: a.id }"` → `alumnoId = input<string>()` |
| Recurso con error | `cursos-list.page.ts` | `res.hasValue() ? res.value() : []` (leer `value()` con error lanza excepción) · `res.error()` |
| Búsqueda con debounce | `cursos-list.page.ts` | `toSignal(toObservable(texto).pipe(debounceTime(400), distinctUntilChanged()))` |
| httpResource condicional | `matriculas-list.page.ts` | devolver `undefined` en la función = no hace la petición |
| ng-content | `page-header.html` | el padre mete botones dentro de `<app-page-header>...</app-page-header>` |
| Parámetro de ruta como input | `curso-form.ts` | `withComponentInputBinding()` + `id = input<string>()` |
| Observable → Promise | servicios | `await firstValueFrom(this.http.get(...))` |

## 7. Chuleta de Angular Material

Cada componente importa solo los módulos que usa, por ejemplo `imports: [MatButtonModule, MatIconModule]`.

| Pieza | Dónde verla | Sintaxis clave |
|---|---|---|
| Campo de texto | todos los forms | `<mat-form-field appearance="outline"><mat-label>Nombre</mat-label><input matInput [formField]="f.nombre" /><mat-error>Obligatorio</mat-error></mat-form-field>` |
| Iconos / prefijos | `login.ts` | `<mat-icon matPrefix>lock</mat-icon>` · `<span matTextPrefix>S/</span>` · `<mat-hint align="end">` |
| Select | `matricula-form.ts` | `<mat-select [formField]="d.cursoId"> @for (...) { <mat-option [value]="c.id"> } </mat-select>` |
| Botones | todos | `mat-flat-button` (principal) · `mat-stroked-button` · `mat-button` (texto) · `mat-icon-button` |
| Toolbar + menú | `toolbar.component.html` | `<mat-toolbar>` · `<button [matMenuTriggerFor]="m">` + `<mat-menu #m="matMenu"><button mat-menu-item>` |
| Sidenav | `shell.component.html` | `<mat-sidenav-container><mat-sidenav mode="side" [opened]="open()">...</mat-sidenav><mat-sidenav-content>` |
| Lista de navegación | `sidenav.component.html` | `<mat-nav-list><a mat-list-item routerLink [activated]="rla.isActive"><mat-icon matListItemIcon>` |
| Tabla | `cursos-list.ts` | `<table mat-table [dataSource]="rows()">` + `matColumnDef` + `*matHeaderCellDef` / `*matCellDef="let c"` + `*matHeaderRowDef` / `*matRowDef` + `*matNoDataRow` |
| Diálogo | `shared/components/confirm-dialog` | `dialog.open(Comp, { data })` → `afterClosed()` · dentro: `inject(MAT_DIALOG_DATA)`, `[mat-dialog-close]="true"` |
| Snackbar | `notification.service.ts` | `snack.open('Mensaje', 'OK', { duration: 3000, panelClass: 'snack-ok' })` |
| Card / progreso | varios | `<mat-card appearance="outlined"><mat-card-content>` · `<mat-progress-bar mode="indeterminate" />` |
| Tooltip | botones de icono | `matTooltip="Editar"` |
| Colores del tema | `styles.scss` y los `.scss` de cada página | variables `var(--mat-sys-primary)`, `var(--mat-sys-error)`, `var(--mat-sys-surface-container)` |

`mat-error` se muestra solo cuando el campo es inválido **y** fue tocado: Material lee el estado directamente desde Signal Forms.
