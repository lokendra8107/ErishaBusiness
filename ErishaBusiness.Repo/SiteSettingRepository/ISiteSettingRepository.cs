using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public interface ISiteSettingRepository
    {
        int InsertUpdateDetail(SiteSettingDto objInsertUpdate);
        Task<SiteSettingDto> GetDetailById();
    }
}
