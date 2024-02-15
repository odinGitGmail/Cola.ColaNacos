using Cola.Core.Models;
using Cola.Core.Models.ColaNacos;
using Cola.Core.Models.ColaNacos.Config;
using Cola.Core.Models.ColaNacos.Namespace;
using Cola.Core.Models.ColaNacos.Namespace.Service;

namespace Cola.ColaNacos;

public interface IColaNacos
{
    #region 命名空间管理
    
    /// <summary>
    /// 查询当前所有的命名空间
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <returns>ApiResult&lt;List&lt;NamespaceList&gt;&gt;</returns>
    ApiResult<List<NacosNamespaceInfo>>? QueryNamespaceList(string clientName);

    /// <summary>
    /// 查询具体命名空间的信息  为空查询public命名空间
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="namespaceId">namespaceId</param>
    /// <returns>ApiResult&lt;NamespaceList&gt;</returns>
    ApiResult<NacosNamespaceInfo>? QueryNamespaceById(string clientName, string? namespaceId);

    /// <summary>
    /// 创建一个命名空间, 命名空间已存在时会报错
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="nacosNamespace">nacosNamespace</param>
    /// <returns>success true</returns>
    ApiResult<bool>? CreateNamespace(string clientName, NacosNamespace nacosNamespace);

    /// <summary>
    /// 编辑命名空间信息
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="nacosNamespace">nacosNamespace</param>
    /// <returns>success true</returns>
    ApiResult<bool>? UpdateNamespace(string clientName, NacosNamespace nacosNamespace);

    /// <summary>
    /// 删除指定命名空间
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="namespaceId">namespaceId</param>
    /// <returns>success true</returns>
    ApiResult<bool>? DeleteNamespace(string clientName, string namespaceId);
    
    #endregion

    #region 配置管理

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="nacosConfig">nacosConfig</param>
    /// <returns>配置内容</returns>
    ApiResult<string>? ConfigContentQueryByNamespaceId(string clientName, GetNacosConfig nacosConfig);

    /// <summary>
    /// 发布配置 - 当配置已存在时，则对配置进行更新
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="nacosConfig">nacosConfig</param>
    /// <returns>是否执行成功</returns>
    ApiResult<bool>? PublishConfig(string clientName, PublishNacosConfig nacosConfig);

    /// <summary>
    /// 删除配置 - 删除指定配置
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="nacosConfig">nacosConfig</param>
    /// <returns>是否执行成功</returns>
    ApiResult<bool>? DeleteConfig(string clientName, DeleteNacosConfig nacosConfig);

    #endregion

    #region 服务管理

    /// <summary>
    /// 查询服务列表
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="nacosServiceList">nacosServiceList</param>
    /// <returns>ApiResult&lt;NacosServiceListResult&gt;</returns>
    ApiResult<NacosServiceListResult>? QueryServiceList(string clientName, NacosServiceList nacosServiceList);
    
    
    
    /// <summary>
    /// 创建服务
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="nacosService">nacosService</param>
    /// <returns>ApiResult&lt;NacosServiceListResult&gt;</returns>
    ApiResult<NacosServiceResult>? QueryService(string clientName, NacosService nacosService);
    
    /// <summary>
    /// 创建服务
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="nacosServiceCreate">nacosServiceCreate</param>
    /// <returns>ApiResult&lt;NacosServiceListResult&gt;</returns>
    ApiResult<string>? CreateService(string clientName, NacosServiceCreate nacosServiceCreate);

    #endregion
}