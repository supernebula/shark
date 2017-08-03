using System.ComponentModel;

namespace Plunder.Compoment.Values
{
    public enum HttpProxyType
    {
        /// <summary>
        /// 普通代理
        /// </summary>
        [Description("普通代理")]
        Normal = 0,

        /// <summary>
        /// 透明代理
        /// </summary>
        [Description("透明代理")]
        Transparent = 1,

        /// <summary>
        /// 匿名代理
        /// </summary>
        [Description("匿名代理")]
        Anonymous = 2,

        /// <summary>
        /// 高匿代理
        /// </summary>
        [Description("高匿代理")]
        HighAnonymity = 3,

        /// <summary>
        /// 混淆代理
        /// </summary>
        [Description("混淆代理")]
        Distorting = 4,


    }
}
