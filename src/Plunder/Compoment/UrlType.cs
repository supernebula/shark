using System;

namespace Plunder.Compoment
{
    public enum UrlType
    {
        /// <summary>
        /// 入口页
        /// </summary>
        Entry = 0,

        /// <summary>
        /// 导航页url，可以直接或间接跳转到目标页
        /// </summary>
        Navigation = 1,

        /// <summary>
        /// 目标页，抽取数据
        /// </summary>
        Target = 2
    }
}
