namespace Cola.ColaNacos;

public class NacosConstant
{
    public class Api
    {
        public class Namespace
        {
            /// <summary>
            /// 查询命名空间列表
            /// </summary>
            public const string QUERY_NAMESPACE = "nacos/v2/console/namespace/list";
            
            /// <summary>
            /// 查询具体命名空间
            /// </summary>
            public const string QUERY_NAMESPACE_BY_ID = "nacos/v2/console/namespace";
            
            /// <summary>
            /// 创建命名空间
            /// </summary>
            public const string CREATE_NAMESPACE = "nacos/v2/console/namespace";
            
            /// <summary>
            /// 编辑命名空间
            /// </summary>
            public const string UPDATE_NAMESPACE = "nacos/v2/console/namespace";
            
            /// <summary>
            /// 删除命名空间
            /// </summary>
            public const string DELETE_NAMESPACE = "nacos/v2/console/namespace";
        }

        public class Config
        {
            /// <summary>
            /// 获取配置
            /// </summary>
            public const string QUERY_CONFIG_CONTENT_BY_ID = "nacos/v2/cs/config";
            
            /// <summary>
            /// 发布配置
            /// </summary>
            public const string PUBLISH_CONFIG = "nacos/v2/cs/config";
            
            /// <summary>
            /// 删除配置
            /// </summary>
            public const string DELETE_CONFIG = "nacos/v2/cs/config";
            
            /// <summary>
            /// 删除配置
            /// </summary>
            public const string QUERY_CONFIG_LIST_BY_NAMESPACE = "nacos/v2/cs/history/configs";
        }

        public class Service
        {
            /// <summary>
            /// 查询服务列表
            /// </summary>
            public const string QUERY_SERVICE_LIST = "nacos/v2/ns/service/list";
            
            /// <summary>
            /// 查询服务详情
            /// </summary>
            public const string QUERY_SERVICE = "nacos/v2/ns/service";
            
            /// <summary>
            /// 创建服务
            /// </summary>
            public const string CREATE_SERVICE = "nacos/v2/ns/service";
        }

        public class Instance
        {
            /// <summary>
            /// 查询指定服务的实例列表
            /// </summary>
            public const string QUERY_INSTANCE_LIST = "nacos/v2/ns/instance/list";
            
            /// <summary>
            /// 查询实例详情
            /// </summary>
            public const string QUERY_INSTANCE = "nacos/v2/ns/instance";
            
            /// <summary>
            /// 创建服务
            /// </summary>
            public const string REGISTER_INSTANCE = "nacos/v2/ns/instance";
            
            /// <summary>
            /// 创建服务
            /// </summary>
            public const string UNREGISTER_INSTANCE = "nacos/v2/ns/instance";
        }
    }
}