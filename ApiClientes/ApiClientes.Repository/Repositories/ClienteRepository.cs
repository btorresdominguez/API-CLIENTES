using ApiClientes.Models;
using ApiClientes.Models.Models;
using ApiClientes.Repository.Data;
using ApiClientes.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Repository.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> ObtenerClientePorIdentificacionAsync(string identificacion)
        {
            var parametro = new SqlParameter("@Identificacion", identificacion);

            var clientes = await _context.Clientes
                .FromSqlRaw("EXEC SP_ObtenerClientePorIdentificacion @Identificacion", parametro)
                .ToListAsync();

            return clientes.FirstOrDefault();
        }
    }
}