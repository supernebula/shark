using System;

namespace Plunder.Compoment
{
    public enum UrlType
    {
        /// <summary>
        /// 导航页url，可以直接或间接跳转到数据页
        /// </summary>
        Navigation,

        /// <summary>
        /// 数据页url，例如：商品详情页，可抽取商品数据
        /// </summary>
        Extracting
    }
}
