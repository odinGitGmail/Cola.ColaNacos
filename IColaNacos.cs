using Cola.Core.Models;
using Cola.Core.Models.ColaNacos;

namespace Cola.ColaNacos;

public interface IColaNacos
{
    #region 命名空间管理接口
    
    /// <summary>
    /// 查询当前所有的命名空间
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <returns>ApiResult&lt;List&lt;NamespaceList&gt;&gt;</returns>
    ApiResult<List<NacosNamespaceInfo>>? NamespaceQuery(string clientName);

    /// <summary>
    /// 查询具体命名空间的信息  为空查询public命名空间
    /// </summary>
    /// <param name="clientName">clientName</param>
    /// <param name="namespaceId">namespaceId</param>
    /// <returns>ApiResult&lt;NamespaceList&gt;</returns>
    ApiResult<NacosNamespaceInfo>? NamespaceQueryById(string clientName, string? namespaceId);

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
}