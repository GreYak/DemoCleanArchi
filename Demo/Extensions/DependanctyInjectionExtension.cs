using Demo.Application;
using Demo.Application.Abstraction;
using Demo.Infrastructure;
using Demo.Infrastructure.Abstractions;
using Demo.Infrastructure.Ef;
using Shop.Repository;
using Transport.Repository;
using static Demo.Api.Middleware.ExecutionContextMiddleware;
using IShopUserRepository = Shop.Repository.IUserRepository;
using ITransportUserRepository = Transport.Repository.IUserRepository;

namespace Demo.Api.Extensions
{
    internal static class DependanctyInjectionExtension
    {
        public static void RegisterStack(this IServiceCollection services)
        {
            // Application service
            services.AddScoped<IShoppingApplicationService, ShoppingApplicationService>();
            services.AddScoped<ITransportApplicationService, TransportApplicationService>();

            // Repository
            services.AddScoped<ITransportUserRepository, TransportUserRepository>();
            services.AddScoped<ITicketBookRepository, TicketBookRepository>();
            services.AddScoped<ITicketRespository, TicketRepository>();
            services.AddScoped<IShopUserRepository, ShoppingUserRepository>();
            services.AddScoped<IControllerRepository, ControllerRepository>();

            // Infrastructure
            services.AddScoped<IExecutionContext, HttpExecutionContext>();
            services.AddDbContext<DemoDbContext>();
        }
    }
}
