using Cola.ColaWebApi;
using Cola.Core.ColaException;
using Cola.Core.Models;
using Cola.Core.Models.ColaNacos;
using Cola.CoreUtils.Enums;
using Cola.CoreUtils.Extensions;
using Cola.CoreUtils.Helper;

namespace Cola.ColaNacos;

public class ColaNacos : IColaNacos
{
    private readonly IWebApi _webApi;
    private readonly IColaException _colaException;
    public ColaNacos(IWebApi webApi, IColaException colaException)
    {
        _webApi = webApi;
        _colaException = colaException;
    }

    public ApiResult<List<NacosNamespaceInfo>>? NamespaceQuery(string clientName)
    {
        var uri = NacosConstant.Api.Namespace.QUERY_NAMESPACE;
        return _webApi.GetClient(clientName)
            .GetWebApi<ApiResult<List<NacosNamespaceInfo>>>(uri);
    }
    
    public ApiResult<NacosNamespaceInfo>? NamespaceQueryById(string clientName,string? namespaceId)
    {
        var uri = NacosConstant.Api.Namespace.QUERY_NAMESPACE_BY_ID;
        uri += "?namespaceId=" + namespaceId ?? "";
        return _webApi.GetClient(clientName).GetWebApi<ApiResult<NacosNamespaceInfo>>(uri)!;
    }
    
    public ApiResult<bool>? CreateNamespace(string clientName,NacosNamespace nacosNamespace)
    {
        var uri = NacosConstant.Api.Namespace.CREATE_NAMESPACE;
        var httpContent = new FormUrlEncodedContent(nacosNamespace.ConvertObjectToDictionary());
        return _webApi.GetClient(clientName).PostWebApi<ApiResult<bool>>(
            uri,
            httpContent,
            EnumMediaType.Urlencoded)!;
    }
    
    public ApiResult<bool>? UpdateNamespace(string clientName,NacosNamespace nacosNamespace)
    {
        var uri = NacosConstant.Api.Namespace.UPDATE_NAMESPACE;
        var httpContent = new FormUrlEncodedContent(nacosNamespace.ConvertObjectToDictionary());
        return _webApi.GetClient(clientName).PutWebApi<ApiResult<bool>>(
            uri,
            httpContent,
            EnumMediaType.Urlencoded)!;
    }
    
    public ApiResult<bool>? DeleteNamespace(string clientName,string namespaceId)
    {
        if (namespaceId.StringIsNullOrEmpty())
            throw _colaException.ThrowStringIsNullOrEmpty("namespaceId", "is not null")!;
        var uri = NacosConstant.Api.Namespace.DELETE_NAMESPACE;
        return _webApi.GetClient(clientName).DeleteWebApi<ApiResult<bool>>(
            $"{uri}?namespaceId={namespaceId}",null)!;
    }
}