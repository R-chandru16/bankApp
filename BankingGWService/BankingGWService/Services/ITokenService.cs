using BankingGWService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingGWService.Services
{
    public interface ITokenService
    {
        public string CreateToken(UserDTO userDTO);
    }
}
