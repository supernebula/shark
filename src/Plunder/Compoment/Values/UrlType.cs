using System.ComponentModel;

namespace Plunder.Compoment
{
    public enum UrlType
    {
        /// <summary>
        /// 入口Url
        /// </summary>
        [Description("入口Url")]
        Entry = 0,

        /// <summary>
        /// 导航Url
        /// </summary>
        [Description("导航Url")]
        Navigation = 1,

        /// <summary>
        /// 目标Url
        /// </summary>
        [Description("目标Url")]
        Target = 2
    }
}
