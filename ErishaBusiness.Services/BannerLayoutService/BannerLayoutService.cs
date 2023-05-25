using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public class BannerLayoutService : IBannerLayoutService
    {
        IBannerLayoutRepository _bannerLayoutRepository;

        public BannerLayoutService(IBannerLayoutRepository bannerLayoutRepository)
        {
            _bannerLayoutRepository = bannerLayoutRepository;
        }

        public async Task<BannerLayoutDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var result = await _bannerLayoutRepository.GetAll(Id, SkipRecord, TakeRecord, SortColumn, SortColumnDirection, SearchValue);
            return result;
        }

        public async Task<int> DeleteDetailById(int Id)
        {
            var result = await _bannerLayoutRepository.DeleteDetailById(Id);
            return result;
        }

        public async Task<BannerLayoutDto> GetDetailByID(int Id)
        {
            var result = await _bannerLayoutRepository.GetDetailById(Id);
            return result;
        }

        public int InsertUpdateDetail(BannerLayoutDto objBannerLayout)
        {
            var result = _bannerLayoutRepository.InsertUpdateDetail(objBannerLayout);
            return result;
        }

        public async Task<IEnumerable<BannerLayoutCategoryDto>> GetCategories()
        {
            var results = await _bannerLayoutRepository.GetCategories();
            return results;
        }

        public AllRecordDateModifiedDetailDto GetAllRecordDateModifiedDetail()
        {
            var results = _bannerLayoutRepository.GetAllRecordDateModifiedDetail();
            return results;
        }
    }
}
