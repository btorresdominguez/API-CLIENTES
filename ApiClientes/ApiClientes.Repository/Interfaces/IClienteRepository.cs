using ApiClientes.Models;
using ApiClientes.Models.Models;

namespace ApiClientes.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObtenerClientePorIdentificacionAsync(string identificacion);
    }
}