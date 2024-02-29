using Cola.ColaWebApi;
using Cola.Core.ColaConsole;
using Cola.Core.ColaEx;
using Cola.Core.Models.ColaNacos;
using Cola.Core.Models.ColaNacos.Config;
using Cola.Core.Models.ColaNacos.Namespace;
using Cola.CoreUtils.Constants;
using Cola.CoreUtils.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cola.ColaNacos;

public static class ColaNacosInject
{
    /// <summary>
    /// AddColaNacos
    /// </summary>
    /// <param name="builder">services</param>
    /// <param name="configuration">configuration</param>
    /// <param name="webApiClientName">webApiClientName</param>
    /// <param name="subSectionName">subSectionNames</param>
    /// <returns></returns>
    public static IHostBuilder AddColaNacos(
        this WebApplicationBuilder builder,
        IConfiguration configuration,
        string webApiClientName,
        string subSectionName)
    {
        var services = builder.Services;
        var exceptionHelper = services.BuildServiceProvider().GetService<IColaException>();
        if (subSectionName.StringIsNullOrEmpty())
        {
            throw new Exception("subSectionName 不能为空");
        }
        if (exceptionHelper == null)
        {
            throw new Exception("需要先注入 IColaException");
        }
        var webApi = services.BuildServiceProvider().GetService<IWebApi>();
        if (webApi == null)
        {
            exceptionHelper.ThrowException("需要先注入 IWebApi 详请查看: https://github.com/odinGitGmail/Cola.ColaNacos");
        }

        builder.Services.AddSingleton<IColaNacos, ColaNacos>();
        
        var colaNacos = services.BuildServiceProvider().GetService<IColaNacos>();
        var nacosNsResult = colaNacos!.QueryNamespaceList(webApiClientName);
        if (nacosNsResult != null)
        {
            if (nacosNsResult.Code == 0)
            {
                var allNameSpaces = nacosNsResult.Data;
                if (allNameSpaces != null && allNameSpaces.Count > 0)
                {
                    #region 自动注册所有子节点

                    var colaNacosOption = configuration.GetColaSection<ColaNacosOption>(
                        $"{SystemConstant.CONSTANT_COLANACOS_SECTION}:{subSectionName}");

                    var allNs = allNameSpaces.Where(n => n.NamespaceShowName != "public")
                        .Select(ns => ns.NamespaceShowName).ToList();
                    var noRegisterNs = allNs.SingleOrDefault(
                        ns => string.Compare(ns, subSectionName, StringComparison.CurrentCultureIgnoreCase) == 0);
                    if (string.IsNullOrEmpty(noRegisterNs))
                    {
                        colaNacos!.CreateNamespace(webApiClientName, new NacosNamespace()
                        {
                            NamespaceId = colaNacosOption.Namespace,
                            NamespaceName = subSectionName,
                            NamespaceDesc = colaNacosOption.Description
                        });
                    }

                    if (colaNacosOption.Listeners != null && colaNacosOption.Listeners.Count > 0)
                    {
                        var nacosConfigResult =
                            colaNacos!.QueryConfigListByNamespace(webApiClientName, colaNacosOption.Namespace);
                        if (nacosConfigResult != null && nacosConfigResult.Data != null)
                        {
                            var result = nacosConfigResult.Data.SingleOrDefault(
                                cfg =>
                                    cfg.DataId == colaNacosOption.Listeners[0].DataId &&
                                    cfg.Group == colaNacosOption.Listeners[0].Group);
                            if (result == null)
                            {
                                var publishConfig = new PublishNacosConfig()
                                {
                                    NamespaceId = colaNacosOption.Namespace,
                                    Group = colaNacosOption.Listeners[0].Group,
                                    DataId = colaNacosOption.Listeners[0].DataId,
                                    Desc = colaNacosOption.Listeners[0].Description,
                                    EnumConfigType = EnumNacosConfigType.JSON
                                };
                                colaNacos!.PublishConfig(webApiClientName, publishConfig);
                            }
                        }
                    }

                    #endregion
                }
            }
            else
            {
                exceptionHelper.ThrowException("查询 nacos 下所有的命名空间出错");
            }
        }
        
        var host = builder.Host;
        host.UseNacosConfig(section: $"{SystemConstant.CONSTANT_COLANACOS_SECTION}:{subSectionName}", parser: null, logAction: null);
        ConsoleHelper.WriteInfo("AddColaNacos 注入【 IColaNacos, ColaNacos 】");
        return host;
    }
}