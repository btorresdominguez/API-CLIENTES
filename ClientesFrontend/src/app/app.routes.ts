// app.routes.ts
import { Routes } from '@angular/router';
import { BuscarClienteComponent } from './components/buscar-cliente/buscar-cliente.component';

export const routes: Routes = [
  { path: '', redirectTo: '/buscar-cliente', pathMatch: 'full' },
  { path: 'buscar-cliente', component: BuscarClienteComponent },
  { path: '**', redirectTo: '/buscar-cliente' } // Ruta wildcard para 404
];