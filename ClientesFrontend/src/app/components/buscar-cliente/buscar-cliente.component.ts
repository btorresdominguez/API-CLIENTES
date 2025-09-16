// components/buscar-cliente/buscar-cliente.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClienteService } from '../../services/cliente.service';
import { Cliente } from '../../models/cliente';

@Component({
  selector: 'app-buscar-cliente',
  standalone: true,
  imports: [CommonModule, FormsModule], // Imports necesarios para standalone
  templateUrl: './buscar-cliente.component.html',
  styleUrls: ['./buscar-cliente.component.scss']
})
export class BuscarClienteComponent {
  identificacion: string = '';
  cliente: Cliente | null = null;
  isLoading = false;
  errorMessage: string = '';
  searched = false;

  constructor(private clienteService: ClienteService) {}

  onSubmit(): void {
    if (this.identificacion.trim()) {
      this.buscarCliente();
    }
  }

  buscarCliente(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.cliente = null;
    this.searched = false;

    this.clienteService.obtenerClientePorIdentificacion(this.identificacion.trim())
      .subscribe({
        next: (cliente: Cliente) => {
          this.cliente = cliente;
          this.isLoading = false;
          this.searched = true;
        },
        error: (error: Error) => {
          this.errorMessage = error.message;
          this.isLoading = false;
          this.searched = true;
        }
      });
  }

  limpiarBusqueda(): void {
    this.identificacion = '';
    this.cliente = null;
    this.errorMessage = '';
    this.searched = false;
  }

  isValidIdentificacion(): boolean {
    return this.identificacion.trim().length > 0 && /^[0-9]+$/.test(this.identificacion.trim());
  }
}