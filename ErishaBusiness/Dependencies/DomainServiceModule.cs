﻿using Autofac;
using Autofac.Core;
using ErishaBusiness.Repo;
using ErishaBusiness.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ErishaBusiness.Dependencies
{
    /// <summary>
    /// DomainServiceModule
    /// </summary>
    public class DomainServiceModule : Module
    {
        /// <summary>
        /// IConfiguration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// DomainServiceModule
        /// </summary>
        /// <param name="configuration"></param>
        public DomainServiceModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<VoucherRepository>().As<IVoucherRepository>();
            builder.RegisterType<VoucherService>().As<IVoucherService>();
            builder.RegisterType<EmailTemplateRepository>().As<IEmailTemplateRepository>();
            builder.RegisterType<EmailTemplateService>().As<IEmailTemplateService>();
            builder.RegisterType<BannerLayoutRepository>().As<IBannerLayoutRepository>();
            builder.RegisterType<BannerLayoutService>().As<IBannerLayoutService>();
            builder.RegisterType<CmsPageRepository>().As<ICmsPageRepository>();
            builder.RegisterType<CmsPageService>().As<ICmsPageService>();
            builder.RegisterType<SiteSettingRepository>().As<ISiteSettingRepository>();
            builder.RegisterType<SiteSettingService>().As<ISiteSettingService>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<ProductService>().As<IProductService>();
        }
    }
}
