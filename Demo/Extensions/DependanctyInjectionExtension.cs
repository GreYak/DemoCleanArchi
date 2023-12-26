﻿using Demo.Application;
using Demo.Application.Abstraction;
using Demo.Infrastructure;
using Demo.Infrastructure.Ef;
using Shop.Repositories;
using Transport.Repositories;
using IShopUserRepository = Shop.Repositories.IUserRepository;
using ITransportUserRepository = Transport.Repositories.IUserRepository;

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
            services.AddDbContext<DemoDbContext>();
        }
    }
}
