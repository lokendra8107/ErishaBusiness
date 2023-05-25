using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public interface IBannerLayoutRepository
    {
        Task<BannerLayoutDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteDetailById(int Id);
        Task<BannerLayoutDto> GetDetailById(int Id);
        int InsertUpdateDetail(BannerLayoutDto objInsertUpdate);
        Task<IEnumerable<BannerLayoutCategoryDto>> GetCategories();
        AllRecordDateModifiedDetailDto GetAllRecordDateModifiedDetail();
    }
}
