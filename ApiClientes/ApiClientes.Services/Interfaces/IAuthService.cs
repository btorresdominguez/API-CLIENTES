using ApiClientes.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);

    }
}
