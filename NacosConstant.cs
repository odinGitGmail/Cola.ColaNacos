namespace Cola.ColaNacos;

public class NacosConstant
{
    public static class Api
    {
        public static class Namespace
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
    }
}