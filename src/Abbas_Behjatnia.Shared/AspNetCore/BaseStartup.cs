
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.Shared.AspNetCore;

public static class BaseStartup
{
    public static void AddApplication(this IServiceCollection services)
    {
        var loadedAssemblies = new List<Assembly>();
        var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").ToList();
        referencedPaths.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

        services.AddRouting(opt => opt.LowercaseUrls = true);
        services.AddHttpContextAccessor();
        services.AddAutoMapper(loadedAssemblies);

        foreach (Assembly assembly in loadedAssemblies)
        {
            assembly
            .GetTypes()
            .Where(item => item.IsClass && item.IsSubclassOf(typeof(DbContext)))
            .ToList()
            .ForEach(appDbContext =>
            {
                services.AddScoped(typeof(DbContext), appDbContext);
            });

            services.Scan(scan => scan
            .FromAssemblies(assembly) 
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainService<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

            services.Scan(scan => scan
            .FromAssemblies(assembly) 
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

            services.Scan(scan => scan
            .FromAssemblies(assembly) 
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseAppService<,,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
        }
    }

    public static void InitializeApplication(this IApplicationBuilder app)
    {
        // app.UseMiddleware<ExceptionMiddleware>();
    }

}