﻿using Cola.ColaWebApi;
using Cola.Core.ColaException;
using Cola.Core.Models;
using Cola.Core.Models.ColaNacos;
using Cola.Core.Models.ColaNacos.Config;
using Cola.Core.Models.ColaNacos.Namespace;
using Cola.Core.Models.ColaNacos.Namespace.Service;
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

    #region 命名空间管理

    public ApiResult<List<NacosNamespaceInfo>>? QueryNamespaceList(string clientName)
    {
        var uri = NacosConstant.Api.Namespace.QUERY_NAMESPACE;
        return _webApi.GetClient(clientName)
            .GetWebApi<ApiResult<List<NacosNamespaceInfo>>>(uri);
    }
    
    public ApiResult<NacosNamespaceInfo>? QueryNamespaceById(string clientName,string? namespaceId)
    {
        var uri = NacosConstant.Api.Namespace.QUERY_NAMESPACE_BY_ID;
        uri += "?namespaceId=" + namespaceId ?? "";
        return _webApi.GetClient(clientName).GetWebApi<ApiResult<NacosNamespaceInfo>>(uri)!;
    }
    
    public ApiResult<bool>? CreateNamespace(string clientName,NacosNamespace nacosNamespace)
    {
        var uri = NacosConstant.Api.Namespace.CREATE_NAMESPACE;
        var content = new FormUrlEncodedContent(nacosNamespace.ConvertObjectToDictionary());
        return _webApi.GetClient(clientName).PostWebApi<ApiResult<bool>>(
            uri,
            content,
            EnumMediaType.Urlencoded)!;
    }
    
    public ApiResult<bool>? UpdateNamespace(string clientName,NacosNamespace nacosNamespace)
    {
        var uri = NacosConstant.Api.Namespace.UPDATE_NAMESPACE;
        var content = new FormUrlEncodedContent(nacosNamespace.ConvertObjectToDictionary());
        return _webApi.GetClient(clientName).PutWebApi<ApiResult<bool>>(
            uri,
            content,
            EnumMediaType.Urlencoded)!;
    }
    
    public ApiResult<bool>? DeleteNamespace(string clientName, string namespaceId)
    {
        if (namespaceId.StringIsNullOrEmpty())
            throw _colaException.ThrowStringIsNullOrEmpty("namespaceId", EnumException.ParamNotNull)!;
        var uri = NacosConstant.Api.Namespace.DELETE_NAMESPACE;
        return _webApi.GetClient(clientName).DeleteWebApi<ApiResult<bool>>(
            $"{uri}?namespaceId={namespaceId}",null)!;
    }

    #endregion

    #region 配置管理

    public ApiResult<string>? ConfigContentQueryByNamespaceId(string clientName, GetNacosConfig nacosConfig)
    {
        if (nacosConfig.Group.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosConfig.Group, EnumException.ParamNotNull);
        if (nacosConfig.DataId.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosConfig.DataId, EnumException.ParamNotNull);
        var uri = NacosConstant.Api.Config.QUERY_CONFIG_BY_ID;
        nacosConfig.NamespaceId =
            nacosConfig.NamespaceId.StringIsNullOrEmpty() ? "public" : nacosConfig.NamespaceId;
        uri += $"?dataId={nacosConfig.DataId}&group={nacosConfig.Group}&namespaceId={nacosConfig.NamespaceId}";
        return _webApi.GetClient(clientName)
            .GetWebApi<ApiResult<string>>(uri);
    }
    
    public ApiResult<bool>? PublishConfig(string clientName, PublishNacosConfig nacosConfig)
    {
        if (nacosConfig.Group.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosConfig.Group, EnumException.ParamNotNull);
        if (nacosConfig.DataId.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosConfig.DataId, EnumException.ParamNotNull);
        if (nacosConfig.Content.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosConfig.Content, EnumException.ParamNotNull);
        nacosConfig.NamespaceId =
            nacosConfig.NamespaceId.StringIsNullOrEmpty() ? "public" : nacosConfig.NamespaceId;
        var uri = NacosConstant.Api.Config.PUBLISH_CONFIG;
        FormUrlEncodedContent content = new FormUrlEncodedContent(nacosConfig.ConvertObjectToDictionary());
        return _webApi.GetClient(clientName)
            .PostWebApi<ApiResult<bool>>(
                uri, 
                content, 
                EnumMediaType.Urlencoded);
    }
    
    public ApiResult<bool>? DeleteConfig(string clientName, DeleteNacosConfig nacosConfig)
    {
        if (nacosConfig.Group.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosConfig.Group, EnumException.ParamNotNull);
        if (nacosConfig.DataId.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosConfig.DataId, EnumException.ParamNotNull);
        nacosConfig.NamespaceId =
            nacosConfig.NamespaceId.StringIsNullOrEmpty() ? "public" : nacosConfig.NamespaceId;
        var uri = NacosConstant.Api.Config.DELETE_CONFIG;
        var queryString = nacosConfig.ConvertObjectToQueryString();
        uri += $"?{queryString}";
        return _webApi.GetClient(clientName)
            .DeleteWebApi<ApiResult<bool>>(uri);
    }

    #endregion

    #region 服务管理

    public ApiResult<NacosServiceListResult>? QueryServiceList(string clientName, NacosServiceList nacosServiceList)
    {
        if (nacosServiceList.Selector.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosServiceList.Selector, EnumException.ParamNotNull);
        var uri = NacosConstant.Api.Service.QUERY_SERVICE_LIST;
        var queryString = nacosServiceList.ConvertObjectToQueryString();
        uri += $"?{queryString}";
        return _webApi.GetClient(clientName)
            .GetWebApi<ApiResult<NacosServiceListResult>>(uri);
    }

    public ApiResult<NacosServiceResult>? QueryService(string clientName, NacosService nacosService)
    {
        if (nacosService.ServiceName.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosService.ServiceName, EnumException.ParamNotNull);
        var uri = NacosConstant.Api.Service.QUERY_SERVICE_LIST;
        var queryString = nacosService.ConvertObjectToQueryString();
        uri += $"?{queryString}";
        return _webApi.GetClient(clientName)
            .GetWebApi<ApiResult<NacosServiceResult>>(uri);
    }

    public ApiResult<string>? CreateService(string clientName, NacosServiceCreate nacosServiceCreate)
    {
        if (nacosServiceCreate.ServiceName.StringIsNullOrEmpty())
            _colaException.ThrowStringIsNullOrEmpty(nacosServiceCreate.ServiceName, EnumException.ParamNotNull);
        var uri = NacosConstant.Api.Service.CREATE_SERVICE;
        FormUrlEncodedContent content = new FormUrlEncodedContent(nacosServiceCreate.ConvertObjectToDictionary());
        return _webApi.GetClient(clientName)
            .PostWebApi<ApiResult<string>>(
                uri, 
                content, 
                EnumMediaType.Urlencoded);
    }

    #endregion
}