using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public class SiteSettingService : ISiteSettingService
    {
        ISiteSettingRepository _siteSettingRepository;

        public SiteSettingService(ISiteSettingRepository SiteSettingRepository)
        {
            _siteSettingRepository = SiteSettingRepository;
        }

        public async Task<SiteSettingDto> GetDetailByID()
        {
            var result = await _siteSettingRepository.GetDetailById();
            return result;
        }

        public int InsertUpdateDetail(SiteSettingDto objSiteSetting)
        {
            var result = _siteSettingRepository.InsertUpdateDetail(objSiteSetting);
            return result;
        }
	}
}
