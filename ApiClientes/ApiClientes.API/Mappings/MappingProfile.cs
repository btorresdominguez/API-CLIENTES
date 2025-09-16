using ApiClientes.DTOs;
using ApiClientes.DTOs.DTOs;
using ApiClientes.Models;
using ApiClientes.Models.Models;
using AutoMapper;

namespace ApiClientes.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}