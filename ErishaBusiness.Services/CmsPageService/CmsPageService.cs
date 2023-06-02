using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public class CmsPageService : ICmsPageService
    {
        ICmsPageRepository _cmsPageRepository;

        public CmsPageService(ICmsPageRepository cmsPageRepository)
        {
            _cmsPageRepository = cmsPageRepository;
        }

        public async Task<CmsPageDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var result = await _cmsPageRepository.GetAll(Id, SkipRecord, TakeRecord, SortColumn, SortColumnDirection, SearchValue);
            return result;
        }

        public async Task<int> DeleteDetailById(int Id)
        {
            var result = await _cmsPageRepository.DeleteDetailById(Id);
            return result;
        }

        public async Task<CmsPageDto> GetDetailByID(int Id)
        {
            var result = await _cmsPageRepository.GetDetailById(Id);
            return result;
        }

        public int InsertUpdateDetail(CmsPageDto objCmsPage)
        {
            var result = _cmsPageRepository.InsertUpdateDetail(objCmsPage);
            return result;
        }

		public Task<IEnumerable<CmsPageDto>> GetAllCmsPage()
		{
			var results = _cmsPageRepository.GetAllCmsPageList();
			return results;
		}
	}
}
