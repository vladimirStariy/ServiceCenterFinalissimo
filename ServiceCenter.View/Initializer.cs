using ServiceCenter.Data.Repository;
using ServiceCenter.Data.Repository.Implementation;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Service.Implementations;
using ServiceCenter.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Data;

namespace ServiceCenter.View
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<TariffType>, TariffTypeRepository>();
            services.AddScoped<IRepository<Tariff>, TariffRepository>();
            services.AddScoped<IRepository<Abonent>, AbonentRepository>();
            services.AddScoped<IRepository<Employee>, EmployeeRepository>();
            services.AddScoped<IRepository<Material>, MaterialRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<OrderMaterial>, OrderMaterialRepository>();
            services.AddScoped<IRepository<Payment>, PaymentRepository>();
            services.AddScoped<IRepository<OrderService>, ServiceRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITariffTypeService, TariffTypeService>();
            services.AddScoped<ITariffService, TariffService>();
            services.AddScoped<IAbonentService, AbonentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IOrderService, OOrderService>();
            services.AddScoped<IOrderMaterial, OrderMaterialService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IServiceService, ServiceService>();
        }
    }
}
