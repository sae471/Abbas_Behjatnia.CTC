
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Abbas_Behjatnia.Shared.AspNetCore;

public static class AppConfiguration
{
    public static IConfiguration Configuration
    {
        get
        {
            try
            {
                return LazyServiceProvider.LazyGetService<IConfiguration>();
            }
            catch (System.Exception)
            {

                var appSetting = Debugger.IsAttached ? "appsettings.Development.json" : "appsettings.json";
                return new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile(appSetting, optional: false, reloadOnChange: true).Build();
            }
        }
    }
}
