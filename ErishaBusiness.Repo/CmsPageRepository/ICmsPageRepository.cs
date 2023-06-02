using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public interface ICmsPageRepository
    {
        Task<CmsPageDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteDetailById(int Id);
        int InsertUpdateDetail(CmsPageDto objInsertUpdate);
        Task<CmsPageDto> GetDetailById(int Id);
        Task<IEnumerable<CmsPageDto>> GetAllCmsPageList();
    }
}
