using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ErishaBusiness.Repo.CategoryRepository
{
    public class CategoryRepository: BaseRepository, ICategoryRepository
    {
        readonly IDbConnection _dbConnection;

        public CategoryRepository(IDbConnection dbConnection): base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}
