using Dapper;
using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        readonly IDbConnection _dbConnection;

        public AccountRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserLoginDto> GetUserLoginDetailByEmail(string Email)
        {
            var results = await GetQueryFirstAsync<UserLoginDto>(DbConstant.GetUserLoginDetail, new
            {
                @UserEmail = Email
            });
            return results;
        }
    }
}
