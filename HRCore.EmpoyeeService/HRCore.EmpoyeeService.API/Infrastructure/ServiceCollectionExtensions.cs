using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.Services;
using HRCore.EmployeeService.Domain.Interfaces;
using HRCore.EmployeeService.Infrastructure.Persistence.DBContext;
using HRCore.EmployeeService.Infrastructure.Persistence.Repositories;
using HRCore.EmployeeService.Infrastructure.Persistence.UnitOfWork;
using HRCore.EmployeeService.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;

namespace HRCore.EmpoyeeService.API.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmployeeServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEmployeeAppService, EmployeeAppService>();
            services.AddScoped<IMessagingService, MessageService>();


            return services;
        }

        public static IServiceCollection AddEmployeeDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EmployeeServiceDBConnection")
                ?? throw new InvalidOperationException("Connection string 'EmployeeServiceDBConnection' not found.");

            services.AddDbContext<EmployeeServiceDBContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("__MyMigrationsHistory", "emp")));

            return services;
        }
    }
}
