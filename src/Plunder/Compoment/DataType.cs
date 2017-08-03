using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public enum DataType
    {
        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        Text = 0,

        /// <summary>
        /// 整数
        /// </summary>
        [Description("整数")]
        Integer = 1,

        /// <summary>
        /// 小数
        /// </summary>
        [Description("小数")]
        Decimal = 2,

        /// <summary>
        /// 百分比
        /// </summary>
        [Description("百分比")]
        Percent = 3,

        /// <summary>
        /// 日期和时间
        /// </summary>
        [Description("日期和时间")]
        DateTime = 4,

        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        Date = 5,

        /// <summary>
        /// 时间
        /// </summary>
        [Description("时间")]
        Time = 6,

        /// <summary>
        /// 电话号码
        /// </summary>
        [Description("电话号码")]
        PhoneNumber = 7,

        /// <summary>
        /// 布尔型
        /// </summary>
        [Description("布尔型")]
        Boolean = 8,

        /// <summary>
        /// 货币值
        /// </summary>
        [Description("货币值")]
        Currency = 9,


        /// <summary>
        /// Email
        /// </summary>
        [Description("Email")]
        EmailAddress = 10,

        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        Password = 11,

        /// <summary>
        /// 链接地址
        /// </summary>
        [Description("链接地址")]
        Url = 12,

        /// <summary>
        /// 图片地址
        /// </summary>
        [Description("图片地址")]
        ImageUrl = 13,

        /// <summary>
        /// 信用卡号
        /// </summary>
        [Description("信用卡号")]
        CreditCard = 14,

        /// <summary>
        /// 邮政代码
        /// </summary>
        [Description("邮政代码")]
        PostalCode = 15,


        /// <summary>
        /// Html
        /// </summary>
        [Description("Html")]
        Html = 16,

        /// <summary>
        /// Json
        /// </summary>
        [Description("Json")]
        Json = 17,

        /// <summary>
        /// XML
        /// </summary>
        [Description("XML")]
        Xml = 18,

        /// <summary>
        /// JS脚本
        /// </summary>
        [Description("JS脚本")]
        JavaScript = 19
    }
}
