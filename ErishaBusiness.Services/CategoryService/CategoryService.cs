using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public class CategoryService: ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDetailDto> GetAllCategory(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var result = await _categoryRepository.GetAll(Id, SkipRecord, TakeRecord, SortColumn, SortColumnDirection, SearchValue);
            return result;
        }

        public async Task<int> DeleteCategoryById(int Id)
        {
            var result = await _categoryRepository.DeleteCategoryById(Id);
            return result;
        }

        public async Task<CategoryDto> GetCategoryByID(int Id)
        {
            var result = await _categoryRepository.GetCategoryById(Id);
            return result;
        }

        public int InsertUpdateCategory(CategoryDto objCategory)
        {
            var result = _categoryRepository.InsertUpdateCategory(objCategory);
            return result;
        }
    }
}
