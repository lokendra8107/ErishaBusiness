using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public interface ICmsPageService
    {
        Task<CmsPageDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteDetailById(int Id);
        Task<CmsPageDto> GetDetailByID(int Id);
        int InsertUpdateDetail(CmsPageDto objCmsPage);
        Task<IEnumerable<CmsPageDto>> GetAllCmsPage();
    }
}
