using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniDash.BLL.Infrastructure.Mapping;
using UniDash.DAL.Infrastructure;
using UniDash.DAL.Repositories;
using UniDash.BLL.Interfaces;
using UniDash.BLL.Services;
using UniDash.DAL.EF;
using UniDash.Model.Models;
using UniDash.Model.Models.StructUniversity;

namespace UniDash.BLL.Infrastructure
{
    public static class ServiceModuleExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString)
        {
            // AutoMapper
            services.AddAutoMapper();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DomainToViewMappingProfile>();
                cfg.AddProfile<ViewToDomainMappingProfile>();
            });

            // DataBase and Identity
            services.AddDbContext<DutContext>(p => p.UseSqlServer(connectionString));
            services.AddScoped<IDataBaseFactory, DataBaseFactory>(provider => new DataBaseFactory(connectionString));
            services.AddTransient<IUnitOfWork, IdentityUnitWork>();

            services.AddIdentity<DutUser, DutRole>(p =>
                {
                    p.Password.RequireUppercase = false;
                    p.Password.RequireLowercase = false;
                    p.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<DutContext>()
                .AddDefaultTokenProviders();

            // Repository
            services.AddTransient<IUserDetailsRepository, UserDetailsRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IApplicantRepository, ApplicantRepository>();
            services.AddTransient<ISubmissionRepository, SubmissionRepository>();

            services.AddTransient<IUStructureRepository<Faculty>, UStructureRepository<Faculty>>();
            services.AddTransient<IUStructureRepository<Institute>, UStructureRepository<Institute>>();
            services.AddTransient<IUStructureRepository<Department>, UStructureRepository<Department>>();
            services.AddTransient<IUStructureRepository<Specialty>, UStructureRepository<Specialty>>();

            // Service
            services.AddTransient<IApplicantService, ApplicantService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
