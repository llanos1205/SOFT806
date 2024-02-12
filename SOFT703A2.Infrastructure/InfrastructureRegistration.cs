using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Auth;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Catalog;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Product;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Trolley;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;
using SOFT703A2.Infrastructure.Persistence;
using SOFT703A2.Infrastructure.Repositories;
using SOFT703A2.Infrastructure.ViewModels.Auth;
using SOFT703A2.Infrastructure.ViewModels.Catalog;
using SOFT703A2.Infrastructure.ViewModels.Product;
using SOFT703A2.Infrastructure.ViewModels.Trolley;
using SOFT703A2.Infrastructure.ViewModels.User;
using Serilog;
using Serilog.Filters;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Home;
using SOFT703A2.Infrastructure.ViewModels.Home;

namespace SOFT703A2.Infrastructure;

public static class InfrastructureRegistration
{
    public static async Task<IServiceCollection> AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddIdentity<User, Role>(opt => { opt.SignIn.RequireConfirmedAccount = false; })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.ConfigureApplicationCookie(options => { options.LoginPath = "/Account/Login"; });
        LoadLogging(services);
        LoadRepositories(services);
        LoadViewModels(services);
        await Seeding(services);
        return services;
    }

    private static void LoadRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITrolleyRepository, TrolleyRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<DataSeeder>();
    }

    private static void LoadViewModels(IServiceCollection services)
    {
        services.AddScoped<IRegisterViewModel, RegisterViewModel>();
        services.AddScoped<ILoginViewModel, LoginViewModel>();
        services.AddScoped<ICreateProductViewModel, CreateProductViewModel>();
        services.AddScoped<IDetailProductViewModel, DetailProductViewModel>();
        services.AddScoped<IListProductViewModel, ListProductViewModel>();
        services.AddScoped<IListUserViewModel, ListUserViewModel>();
        services.AddScoped<IDetailUserViewModel, DetailUserViewModel>();
        services.AddScoped<ICreateUserViewModel, CreateUserViewModel>();
        services.AddScoped<IMarketPlaceViewModel, MarketPlaceViewModel>();
        services.AddScoped<ITrolleyViewModel, TrolleyViewModel>();
        services.AddScoped<IDetailMarketProductViewModel, DetailMarketProductViewModel>();
        services.AddScoped<IHomeViewModel, HomeViewModel>();
    }

    private static void LoadLogging(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("./log.txt", rollingInterval: RollingInterval.Day)
            .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore.Database.Command"))
            .CreateLogger();
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
    }

    private static async Task Seeding(IServiceCollection services)
    {
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            await dataSeeder.SeedData();
        }
    }
}