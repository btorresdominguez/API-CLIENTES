using ApiClientes.DTOs;
using ApiClientes.DTOs.DTOs;
using ApiClientes.Models;
using ApiClientes.Models.Models;
using ApiClientes.Repository.Interfaces;
using ApiClientes.Services.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ApiClientes.Tests.Services
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ClienteService>> _mockLogger;
        private readonly ClienteService _service;

        public ClienteServiceTests()
        {
            _mockRepository = new Mock<IClienteRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ClienteService>>();
            _service = new ClienteService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ObtenerClientePorIdentificacion_ClienteExiste_RetornaSuccess()
        {
            // Arrange
            var identificacion = "12345678";
            var cliente = new Cliente
            {
                IdCliente = 1,
                Identificacion = identificacion,
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan.perez@email.com"
            };
            var clienteDto = new ClienteDto
            {
                IdCliente = 1,
                Identificacion = identificacion,
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan.perez@email.com"
            };

            _mockRepository.Setup(r => r.ObtenerClientePorIdentificacionAsync(identificacion))
                          .ReturnsAsync(cliente);
            _mockMapper.Setup(m => m.Map<ClienteDto>(cliente))
                      .Returns(clienteDto);

            // Act
            var result = await _service.ObtenerClientePorIdentificacionAsync(identificacion);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(identificacion, result.Data.Identificacion);
        }

        [Fact]
        public async Task ObtenerClientePorIdentificacion_ClienteNoExiste_RetornaError()
        {
            // Arrange
            var identificacion = "99999999";
            _mockRepository.Setup(r => r.ObtenerClientePorIdentificacionAsync(identificacion))
                          .ReturnsAsync((Cliente)null);

            // Act
            var result = await _service.ObtenerClientePorIdentificacionAsync(identificacion);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("no encontrado", result.Message);
        }

        [Fact]
        public async Task ObtenerClientePorIdentificacion_IdentificacionVacia_RetornaError()
        {
            // Arrange
            var identificacion = "";

            // Act
            var result = await _service.ObtenerClientePorIdentificacionAsync(identificacion);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("requerida", result.Message);
        }
    }
}