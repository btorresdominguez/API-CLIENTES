// Controllers/ClientesController.cs
using ApiClientes.DTOs;
using ApiClientes.DTOs.DTOs;
using ApiClientes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obtiene un cliente por su número de identificación
        /// </summary>
        /// <param name="identificacion">Número de identificación del cliente</param>
        /// <returns>Datos del cliente si existe</returns>
        /// <response code="200">Cliente encontrado exitosamente</response>
        /// <response code="404">Cliente no encontrado</response>
        /// <response code="400">Identificación inválida</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet("{identificacion}")]
        [ProducesResponseType(typeof(ApiResponse<ClienteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ClienteDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<ClienteDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ClienteDto>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ClienteDto>>> ObtenerClientePorIdentificacion(string identificacion)
        {
            var response = await _clienteService.ObtenerClientePorIdentificacionAsync(identificacion);

            if (!response.Success)
            {
                if (response.Message.Contains("no encontrado"))
                {
                    return NotFound(response);
                }
                if (response.Message.Contains("requerida"))
                {
                    return BadRequest(response);
                }
                return StatusCode(500, response);
            }

            return Ok(response);
        }
    }
}