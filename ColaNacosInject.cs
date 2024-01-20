using Cola.ColaWebApi;
using Cola.Core.ColaConsole;
using Cola.Core.ColaException;
using Cola.Core.Models.ColaNacos.Config;
using Cola.CoreUtils.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nacos.V2;
using Nacos.V2.DependencyInjection;

namespace Cola.ColaNacos;

public static class ColaNacosInject
{
    /// <summary>
    /// AddColaNacos
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="configuration">configuration</param>
    /// <param name="subSectionNames">subSectionNames</param>
    /// <returns></returns>
    public static IServiceCollection AddColaNacos(
        this IServiceCollection services,
        IConfiguration configuration,
        List<string> subSectionNames)
    {
        services = InjectColaNacos(services, configuration, subSectionNames);
        var webApi = services.BuildServiceProvider().GetService<IWebApi>();
        if (webApi == null)
        {
            new ColaException().ThrowIfNull(webApi,"需要先注入 IWebApi ");
        }
        services.AddSingleton<IColaNacos, ColaNacos>();
        ConsoleHelper.WriteInfo("AddColaNacos 注入【 IColaNacos, ColaNacos 】");
        return services;
    }

    private static IServiceCollection InjectColaNacos(IServiceCollection services, IConfiguration configuration, List<string> subSectionNames)
    {
        foreach (var subSectionName in subSectionNames)
        {
            services.AddNacosV2Config(configuration.GetSection($"{SystemConstant.CONSTANT_COLANACOS_SECTION}:{subSectionName}"));
        }
        return services;
    }
}