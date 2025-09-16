export interface Cliente {
  idCliente: number;
  identificacion: string;
  nombre: string;
  apellido: string;
  email: string;
  fechaCreacion: Date;
  fechaActualizacion: Date;
  nombreCompleto: string;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: string[];
}