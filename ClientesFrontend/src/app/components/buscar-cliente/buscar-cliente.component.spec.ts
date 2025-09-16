// buscar-cliente.component.spec.ts
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of, throwError } from 'rxjs';

import { BuscarClienteComponent } from './buscar-cliente.component';
import { ClienteService } from '../../services/cliente.service';
import { Cliente } from '../../models/cliente.model';

describe('BuscarClienteComponent', () => {
  let component: BuscarClienteComponent;
  let fixture: ComponentFixture<BuscarClienteComponent>;
  let clienteService: jasmine.SpyObj<ClienteService>;

  const mockCliente: Cliente = {
    idCliente: 1,
    identificacion: '12345678',
    nombre: 'Juan',
    apellido: 'Pérez',
    email: 'juan.perez@email.com',
    fechaCreacion: new Date(),
    fechaActualizacion: new Date(),
    nombreCompleto: 'Juan Pérez'
  };

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('ClienteService', ['obtenerClientePorIdentificacion']);

    await TestBed.configureTestingModule({
      declarations: [ BuscarClienteComponent ],
      imports: [ ReactiveFormsModule, HttpClientTestingModule ],
      providers: [
        { provide: ClienteService, useValue: spy }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BuscarClienteComponent);
    component = fixture.componentInstance;
    clienteService = TestBed.inject(ClienteService) as jasmine.SpyObj<ClienteService>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form with empty identificacion', () => {
    expect(component.searchForm.get('identificacion')?.value).toBe('');
  });

  it('should validate identificacion as required', () => {
    const identificacionControl = component.searchForm.get('identificacion');
    identificacionControl?.setValue('');
    identificacionControl?.markAsTouched();

    expect(identificacionControl?.invalid).toBeTruthy();
    expect(identificacionControl?.errors?.['required']).toBeTruthy();
  });

  it('should validate identificacion pattern', () => {
    const identificacionControl = component.searchForm.get('identificacion');
    identificacionControl?.setValue('abc123');
    identificacionControl?.markAsTouched();

    expect(identificacionControl?.invalid).toBeTruthy();
    expect(identificacionControl?.errors?.['pattern']).toBeTruthy();
  });

  it('should call buscarCliente when form is valid', () => {
    spyOn(component, 'buscarCliente');
    component.searchForm.get('identificacion')?.setValue('12345678');
    
    component.onSubmit();

    expect(component.buscarCliente).toHaveBeenCalled();
  });

  it('should find cliente successfully', () => {
    clienteService.obtenerClientePorIdentificacion.and.returnValue(of(mockCliente));
    component.searchForm.get('identificacion')?.setValue('12345678');

    component.buscarCliente();

    expect(component.loading).toBeFalsy();
    expect(component.cliente).toEqual(mockCliente);
    expect(component.error).toBeNull();
    expect(component.searched).toBeTruthy();
  });

  it('should handle cliente not found', () => {
    const errorMessage = 'Cliente no encontrado';
    clienteService.obtenerClientePorIdentificacion.and.returnValue(
      throwError(() => new Error(errorMessage))
    );
    component.searchForm.get('identificacion')?.setValue('99999999');

    component.buscarCliente();

    expect(component.loading).toBeFalsy();
    expect(component.cliente).toBeNull();
    expect(component.error).toBe(errorMessage);
    expect(component.searched).toBeTruthy();
  });

  it('should clear search results', () => {
    component.cliente = mockCliente;
    component.error = 'Some error';
    component.searched = true;

    component.limpiarBusqueda();

    expect(component.cliente).toBeNull();
    expect(component.error).toBeNull();
    expect(component.searched).toBeFalsy();
    expect(component.searchForm.get('identificacion')?.value).toBeNull();
  });
});