using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public interface ICategoryService
    {
        Task<CategoryDetailDto> GetAllCategory(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteCategoryById(int Id);
        Task<CategoryDto> GetCategoryByID(int Id);
        int InsertUpdateCategory(CategoryDto objCategory);
    }
}
