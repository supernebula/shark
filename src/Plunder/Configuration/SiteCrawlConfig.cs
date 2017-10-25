using System.Collections.Generic;
using System.Xml.Serialization;

namespace Plunder.Configuration
{
    [XmlRoot("siteCrawlConfig")]
    public class SiteCrawlConfig
    {
        [XmlElement("siteId")]
        public string SiteId { get; set; }

        [XmlElement("site")]
        public string Site { get; set; }

        [XmlElement("domain")]
        public string Domain { get; set; }

        [XmlElement("topic")]
        public string Topic { get; set; }

        [XmlArray("seedUrls"),XmlArrayItem("url")]
        public List<string> SeedUrls { get; set; }

        //urlMatch四种规则
        //start:url地址关键字
        //end:url地址关键字
        //include:url地址关键字
        //regex:url正则表达式
        [XmlArray("navUrls"),XmlArrayItem("urlMatch")]
        public List<string> NavUrlMatchs { get; set; }

        [XmlArray("targetPages"), XmlArrayItem("page")]
        public List<TargetPage> Targets { get; set; }
    }


    public class TargetPage
    {
        [XmlElement("collectKey")]
        public string CollectKey { get; set; }

        /// <summary>
        /// static、dynamic、file
        /// </summary>
        public string Type { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("urlMatch")]
        public string UrlMatch { get; set; }

        [XmlArray("fields"), XmlArrayItem("field")]
        public List<FieldRule> FieldRules { get; set; }
    }

    public class FieldRule
    {
        [XmlArray("name")]
        public string Name { get; set; }

        [XmlArray("remark")]
        public string Remark { get; set; }

        [XmlArray("xPath")]
        public string XPath { get; set; }

        [XmlArray("childs")]
        public List<FieldRule> ChildFields { get; set; }
    }
}
