using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOFT806.Domain.Models;
using SOFT806.Infrastructure.Contracts.Repositories;
using SOFT806.Infrastructure.Contracts.ViewModels.Auth;
using SOFT806.Infrastructure.Contracts.ViewModels.Catalog;
using SOFT806.Infrastructure.Contracts.ViewModels.Product;
using SOFT806.Infrastructure.Contracts.ViewModels.Trolley;
using SOFT806.Infrastructure.Contracts.ViewModels.User;
using SOFT806.Infrastructure.Persistence;
using SOFT806.Infrastructure.Repositories;
using SOFT806.Infrastructure.ViewModels.Auth;
using SOFT806.Infrastructure.ViewModels.Catalog;
using SOFT806.Infrastructure.ViewModels.Product;
using SOFT806.Infrastructure.ViewModels.Trolley;
using SOFT806.Infrastructure.ViewModels.User;
using Serilog;
using Serilog.Filters;
using SOFT806.Infrastructure.Contracts.ViewModels.Home;
using SOFT806.Infrastructure.ViewModels.Home;

namespace SOFT806.Infrastructure;

public static class InfrastructureRegistration
{
    public static async Task<IServiceCollection> AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            if (environment == "Test")
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            }
            else
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        });
        services.AddIdentity<User, Role>(opt => { opt.SignIn.RequireConfirmedAccount = false; })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.ConfigureApplicationCookie(options => { options.LoginPath = "/Account/Login"; });
        LoadLogging(services);
        LoadRepositories(services);
        LoadViewModels(services);
        var seeding = configuration.GetSection("SEEDING").Value;
        Log.Warning("SEEDING: {seeding}", seeding);
        if (seeding == "True")
        {
            Log.Warning("I AM SEEDING");
            await Seeding(services);
        }
        else
        {
            Log.Warning("DIDNT SEED");
        }

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