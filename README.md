# API-CLIENTES

# Sistema de Gestión de Clientes - Frontend Angular

## Descripción

Frontend desarrollado en **Angular 17+** para la gestión y búsqueda de clientes. Este proyecto utiliza **componentes standalone** y consume una API REST para proporcionar una interfaz moderna e intuitiva para la consulta de información de clientes.

## Características Principales
- **Búsqueda de Clientes**: Consulta por número de identificación
- **Validación en Tiempo Real**: Verificación de formato de identificación
- **Estados de UI**: Loading, error y success states
- **Responsive Design**: Bootstrap 5 para adaptabilidad móvil
- **Componentes Standalone**: Arquitectura moderna sin NgModules

## Tecnologías Utilizadas
- **Angular 17+** (Standalone Components)
- **TypeScript**
- **Bootstrap 5**
- **FontAwesome**
- **RxJS**
- **SCSS**

## Requisitos
- [Node.js](https://nodejs.org/) (versión 18.13.0 o superior)
- [Angular CLI](https://angular.io/cli) (versión 17+)
- [Git]

## Instalación

### 1. Clonar el Repositorio
```bash
git clone https://github.com/btorresdominguez/ClientesFrontend.git
cd ClientesFrontend
```

### 2. Instalar Dependencias
```bash
npm install bootstrap @fortawesome/fontawesome-free
```

### 3. Configurar Variables de Entorno
Crear archivo `src/environments/environment.ts`:
```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7036'
};
```

### 4. Ejecutar la Aplicación
```bash
ng serve
```

La aplicación estará disponible en `http://localhost:4200`

## Estructura del Proyecto
```
src/
├── app/
│   ├── components/
│   │   └── buscar-cliente/     # Componente principal de búsqueda
│   ├── models/
│   │   └── cliente.model.ts    # Interfaces y tipos
│   ├── services/
│   │   └── cliente.service.ts  # Servicio HTTP
│   ├── app.component.ts        # Componente raíz
│   ├── app.config.ts          # Configuración Angular 17+
│   └── main.ts
├── environments/               # Configuraciones de entorno
├── public/                    # Assets estáticos
└── styles.scss               # Estilos globales
```

## Funcionalidades

### Búsqueda de Clientes
La aplicación permite buscar clientes mediante:
- **Identificación numérica**: Solo acepta números
- **Validación automática**: Verificación en tiempo real
- **Estados visuales**: Loading, éxito y error

### Resultados de Búsqueda
Cuando se encuentra un cliente, se muestra:
- Número de identificación
- Nombre completo
- Email con enlace directo
- Fecha de creación
- Última actualización

## API Endpoints

### Clientes
- `GET /api/Clientes/{identificacion}` - Buscar cliente por identificación

### Ejemplo de Respuesta
```json
{
  "success": true,
  "message": "Cliente encontrado exitosamente",
  "data": {
    "idCliente": 1,
    "identificacion": "12345678",
    "nombre": "Juan",
    "apellido": "Pérez",
    "email": "juan.perez@email.com",
    "fechaCreacion": "2024-01-15T10:30:00",
    "fechaActualizacion": "2024-01-20T14:20:00",
    "nombreCompleto": "Juan Pérez"
  },
  "errors": []
}
```

## Servicios Angular

### ClienteService
```typescript
@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private readonly apiUrl = `${environment.apiUrl}/api/Clientes`;

  obtenerClientePorIdentificacion(identificacion: string): Observable<Cliente> {
    return this.http.get<ApiResponse<Cliente>>(`${this.apiUrl}/${identificacion}`)
      .pipe(
        map(response => response.data),
        catchError(this.handleError)
      );
  }
}
```

## Configuración Angular 17+

### App Config (app.config.ts)
```typescript
import { ApplicationConfig } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptorsFromDi())
  ]
};
```

### Componente Standalone
```typescript
@Component({
  selector: 'app-buscar-cliente',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './buscar-cliente.component.html',
  styleUrls: ['./buscar-cliente.component.scss']
})
export class BuscarClienteComponent { }
```

## Estilos y UI

### Bootstrap 5
- Grid system responsive
- Form controls con validación
- Alerts para estados de error/éxito
- Botones con estados loading

### Animaciones CSS
- Fade-in para resultados
- Spinner de loading
- Hover effects en botones

## Responsive Design

La aplicación se adapta a diferentes tamaños de pantalla:
- **Desktop**: Layout completo con columnas
- **Tablet**: Ajuste de espaciado
- **Mobile**: Layout en una columna

## Comandos de Desarrollo

```bash
# Desarrollo
ng serve

# Build para producción
ng build --configuration production

```
##  Build y Deploy

```bash
# Build optimizado
ng build --configuration production

# Verificar archivos generados
ls -la dist/clientes-frontend/
```

##  Manejo de Errores

La aplicación maneja diferentes tipos de errores:
- **400**: Identificación inválida
- **404**: Cliente no encontrado
- **500**: Error interno del servidor
- **Network**: Problemas de conectividad

---

**Nota**:  API backend esté ejecutándose en `https://localhost:7036`.

