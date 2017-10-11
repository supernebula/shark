using System.Collections.Generic;
using System.Xml.Serialization;

namespace Plunder.Process.Analyze
{
    [XmlRoot("xml")]
    public class PageAnalyzeConfig : IPageAnalyzeConfig
    {
        [XmlElement("tableName")]
        public string TableName { get; set; }

        [XmlElement("topic")]
        public string Topic { get; set; }

        [XmlElement("urlRegex")]
        public string UrlRegex { get; set; }

        [XmlElement("navUrlRegex")]
        public string NavUrlRegex { get; set; }

        [XmlElement("targetUrlRegex")]
        public string TargetUrlRegex { get; set; }

        [XmlArray("rules")]
        [XmlArrayItem("rule")]
        public List<ExtractRule> Rules { get; set; }
    }
}
