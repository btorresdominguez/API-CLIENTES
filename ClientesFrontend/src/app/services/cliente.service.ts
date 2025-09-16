import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Cliente, ApiResponse } from '../models/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private readonly apiUrl = `${environment.apiUrl}/api/Clientes`;

  constructor(private http: HttpClient) { }

  obtenerClientePorIdentificacion(identificacion: string): Observable<Cliente> {
    return this.http.get<ApiResponse<Cliente>>(`${this.apiUrl}/${identificacion}`)
      .pipe(
        map(response => {
          if (response.success && response.data) {
            return response.data;
          }
          throw new Error(response.message || 'Cliente no encontrado');
        }),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Ha ocurrido un error desconocido';
    
    if (error.error instanceof ErrorEvent) {
      // Error del cliente
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Error del servidor
      switch (error.status) {
        case 404:
          errorMessage = 'Cliente no encontrado';
          break;
        case 400:
          errorMessage = 'Identificación inválida';
          break;
        case 500:
          errorMessage = 'Error interno del servidor';
          break;
        default:
          if (error.error?.message) {
            errorMessage = error.error.message;
          }
      }
    }

    return throwError(() => new Error(errorMessage));
  }
}