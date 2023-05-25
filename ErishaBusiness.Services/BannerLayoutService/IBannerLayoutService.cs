using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public interface IBannerLayoutService
    {
        Task<BannerLayoutDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteDetailById(int Id);
        Task<BannerLayoutDto> GetDetailByID(int Id);
        int InsertUpdateDetail(BannerLayoutDto objBannerLayout);
        Task<IEnumerable<BannerLayoutCategoryDto>> GetCategories();
        AllRecordDateModifiedDetailDto GetAllRecordDateModifiedDetail();
    }
}
