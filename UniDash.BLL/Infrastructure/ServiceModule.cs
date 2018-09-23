using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UniDash.DAL.Infrastructure;
using UniDash.DAL.Repositories;
using Ninject.Web.Common;
using UniDash.DAL.EF;
using UniDash.Model.Models;

namespace UniDash.BLL.Infrastructure
{
    public static class ServiceModuleExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IDataBaseFactory, DataBaseFactory>(provider => new DataBaseFactory(connectionString));
            services.AddTransient<IUnitOfWork, IdentityUnitWork>();

            services.AddIdentity<DutUser, DutRole>()
                .AddEntityFrameworkStores<DutContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserDetailsRepository, UserDetailsRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IApplicantRepository, ApplicantRepository>();
            services.AddTransient<ISubmissionRepository, SubmissionRepository>();
            //services.AddTransient<IUStructureRepository<>, UStructureRepository<>>()

            return services;
        }
    }
}
