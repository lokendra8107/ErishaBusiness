using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public class AccountService : IAccountService
    {
        IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<UserLoginDto> GetUserLoginDetailByEmail(string Email)
        {
            return await _accountRepository.GetUserLoginDetailByEmail(Email);
        }
    }
}
