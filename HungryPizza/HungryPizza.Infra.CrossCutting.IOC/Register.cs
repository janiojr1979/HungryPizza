using HungryPizza.Domain.Core.Interfaces.Repositories;
using HungryPizza.Domain.Core.Interfaces.Services;
using HungryPizza.Domain.Services.Services;
using HungryPizza.Infra.Data.Common;
using HungryPizza.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HungryPizza.Infra.CrossCutting.IOC
{
    public static class Register
    {
        public static void AddServices(this IServiceCollection services)
        {
            //Repository
            services.AddScoped<IRepositoryClient, RepositoryClient>();
            services.AddScoped<IRepositoryPizza, RepositoryPizza>();
            services.AddScoped<IRepositoryOrder, RepositoryOrder>();

            //Service
            services.AddScoped<IServiceClient, ServiceClient>();
            services.AddScoped<IServicePizza, ServicePizza>();
            services.AddScoped<IServiceOrder, ServiceOrder>();
        }

        public static void AddDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionStrings connectionStrings = new ConnectionStrings();

            new ConfigureFromConfigurationOptions<ConnectionStrings>(configuration.GetSection("ConnectionStrings")).Configure(connectionStrings);

            services.AddSingleton(connectionStrings);
        }
    }
}
