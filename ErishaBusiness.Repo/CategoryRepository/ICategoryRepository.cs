using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public interface ICategoryRepository
    {
        Task<CategoryDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteCategoryById(int Id);
        Task<CategoryDto> GetCategoryById(int Id);
        int InsertUpdateCategory(CategoryDto objCategory);
    }
}
