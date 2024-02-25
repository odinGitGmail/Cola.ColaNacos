### [ColaNacos](https://github.com/odinGitGmail/Cola.ColaNacos)

[![Version](https://flat.badgen.net/nuget/v/Cola.ColaNacos?label=version)](https://github.com/odinGitGmail/Cola.ColaNacos) [![download](https://flat.badgen.net/nuget/dt/Cola.ColaNacos)](https://www.nuget.org/packages/Cola.ColaNacos) [![commit](https://flat.badgen.net/github/last-commit/odinGitGmail/Cola.ColaNacos)](https://flat.badgen.net/github/last-commit/odinGitGmail/Cola.ColaNacos) [![Blog](https://flat.badgen.net/static/blog/odinsam.com)](https://odinsam.com)


#### 配置
```json
{
  "ColaWebApi": [
    {
      "ClientName": "ColaNacos",
      "BaseUri": "http://192.168.202.132:8848",
      "TimeOut": 5000,
      "Cert": {
        "CertFilePath": "",
        "CertFilePwd": ""
      },
      /* 默认压缩方式  None,GZip,Deflate,Brotli,All */
      "Decompression": "All"
    }
  ],
  "ColaNacos": {
    // 子节点，名称可以自定义
    "public": {
      "EndPoint": "",
      "Listeners": [
        {
          "Optional": false,
          "DataId": "DEFAULT_ID",
          "Group": "DEFAULT_GROUP"
        }
      ],
      "ServerAddresses": [ "http://192.168.202.132:8848" ],
      "Namespace": "",
      "ConfigUseRpc": false,
      "NamingUseRpc": false
    },
    "cola-Production": {
      "EndPoint": "",
      "Listeners": [
        {
          "Optional": false,
          "DataId": "PRODUCTION_ID",
          "Group": "PRODUCTION_GROUP"
        }
      ],
      "ServerAddresses": [ "http://192.168.202.132:8848" ],
      "Namespace": "",
      "ConfigUseRpc": false,
      "NamingUseRpc": false
    },
    "cola-Development": {
      "EndPoint": "",
      "Listeners": [
        {
          "Optional": false,
          "DataId": "DEVELOPMENT_ID",
          "Group": "DEVELOPMENT_GROUP"
        }
      ],
      "ServerAddresses": [ "http://192.168.202.132:8848" ],
      "Namespace": "",
      "ConfigUseRpc": false,
      "NamingUseRpc": false
    },
    "cola-Test": {
      "EndPoint": "",
      "Listeners": [
        {
          "Optional": false,
          "DataId": "TEST_ID",
          "Group": "TEST_GROUP",
          "Description": "cola-Test-Config"
        }
      ],
      "ServerAddresses": [ "http://192.168.202.132:8848" ],
      "Namespace": "",
      "ConfigUseRpc": false,
      "NamingUseRpc": false,
      "Description": "cola-Test"
    }
  }
}
```

#### 注入
```csharp
// 必须注入核心组件
builder.Services.AddColaCore();
// 必须注入webapi
builder.Services.AddColaHttpClient(config);
// list 为 ColaNacos 的子节点，名称可以自定义
builder.Services.AddColaNacos(config, new List<string>() { "public" });
```

#### 已实现接口
```csharp
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
```

