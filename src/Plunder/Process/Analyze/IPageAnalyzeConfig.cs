using System.Collections.Generic;
using System.Xml.Serialization;

namespace Plunder.Process.Analyze
{
    public interface IPageAnalyzeConfig
    {
        string TableName { get; set; }

        string Topic { get; set; }

        string UrlRegex { get; set; }

        string NavUrlRegex { get; set; }

        string TargetUrlRegex { get; set; }

        List<ExtractRule> Rules { get; set; }
    }

    public class ExtractRule
    {
        [XmlArray("fieldName")]
        public string FieldName { get; set; }

        [XmlArray("xPath")]
        public string XPath { get; set; }

        [XmlArray("childs")]
        public List<ExtractRule> Childs { get; set; }
    }
}
