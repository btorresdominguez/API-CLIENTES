using ApiClientes.DTOs;
using ApiClientes.DTOs.DTOs;

namespace ApiClientes.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ApiResponse<ClienteDto>> ObtenerClientePorIdentificacionAsync(string identificacion);
    }
}