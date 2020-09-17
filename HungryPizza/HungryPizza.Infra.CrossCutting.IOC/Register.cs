using HungryPizza.Domain.Core.Interfaces.Repositories;
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
            services.AddScoped<IRepositoryClient, RepositoryClient>();
            services.AddScoped<IRepositoryPizza, RepositoryPizza>();
            services.AddScoped<IRepositoryOrder, RepositoryOrder>();
        }

        public static void AddDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionStrings connectionStrings = new ConnectionStrings();

            new ConfigureFromConfigurationOptions<ConnectionStrings>(configuration.GetSection("ConnectionStrings")).Configure(connectionStrings);

            services.AddSingleton(connectionStrings);
        }
    }
}
