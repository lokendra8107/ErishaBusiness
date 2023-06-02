using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public interface ISiteSettingService
    {
        Task<SiteSettingDto> GetDetailByID();
        int InsertUpdateDetail(SiteSettingDto objSiteSetting);
    }
}
