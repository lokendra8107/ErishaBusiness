using Microsoft.Extensions.DependencyInjection;
using ErishaBusiness.Repo;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace ErishaBusiness.Services.CommonService
{
    public class ServiceInjector
    {
        public ServiceInjector(IServiceCollection services)
        {
            services.AddScoped(typeof(FileService));
        }
    }
}
