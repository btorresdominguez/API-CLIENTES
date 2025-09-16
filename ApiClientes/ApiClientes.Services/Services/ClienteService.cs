using ApiClientes.DTOs;
using ApiClientes.DTOs.DTOs;
using ApiClientes.Repository.Interfaces;
using ApiClientes.Services.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ApiClientes.Services.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(
            IClienteRepository clienteRepository,
            IMapper mapper,
            ILogger<ClienteService> logger)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<ClienteDto>> ObtenerClientePorIdentificacionAsync(string identificacion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identificacion))
                {
                    return ApiResponse<ClienteDto>.ErrorResponse("La identificación es requerida");
                }

                var cliente = await _clienteRepository.ObtenerClientePorIdentificacionAsync(identificacion);

                if (cliente == null)
                {
                    return ApiResponse<ClienteDto>.ErrorResponse("Cliente no encontrado");
                }

                var clienteDto = _mapper.Map<ClienteDto>(cliente);
                return ApiResponse<ClienteDto>.SuccessResponse(clienteDto, "Cliente encontrado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cliente con identificación {Identificacion}", identificacion);
                return ApiResponse<ClienteDto>.ErrorResponse("Error interno del servidor");
            }
        }
    }
}