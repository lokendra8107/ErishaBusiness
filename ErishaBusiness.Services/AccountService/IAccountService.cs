using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public interface IAccountService
    {
        Task<UserLoginDto> GetUserLoginDetailByEmail(string Email);
    }
}
