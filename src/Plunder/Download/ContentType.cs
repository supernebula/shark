
using System;
using System.Net.Http;

namespace Plunder.Download
{
    //public class ContentType
    //{
    //    public const string HTML = "HTML";

    //    public const string DHTML = "DHTML";

    //    public const string XML = "XML";

    //    public const string JSON = "JSON";

    //}

    public class ContentType : IEquatable<ContentType>
    {
        private string type;

        private static readonly ContentType html = new ContentType("HTML");
        private static readonly ContentType dhtml = new ContentType("DHTML");
        private static readonly ContentType xml = new ContentType("XML");
        private static readonly ContentType json = new ContentType("JSON");
        private static readonly ContentType text = new ContentType("TEXT");

        public static ContentType HTML => html;

        public static ContentType DHTML => dhtml;

        public static ContentType XML => xml;

        public static ContentType JSON => json;

        public static ContentType TEXT => text;

        public string Type => type;

        public ContentType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException(nameof(type));
            }
            this.type = type;
        }

        public bool Equals(ContentType other)
        {
            if (other == default(ContentType))
            {
                return false;
            }
            return ((this.type == other.type) || (string.Compare(this.type, other.type, StringComparison.OrdinalIgnoreCase) == 0));
        }

        public override bool Equals(object obj) => Equals(obj as ContentType);

        public override int GetHashCode() => type.ToUpperInvariant().GetHashCode();

        public static bool operator ==(ContentType left, ContentType right)
        {
            if (left == null)
            {
                return (right == null);
            }
            if (right == null)
            {
                return (left == null);
            }
            return left.Equals(right);
        }

        public static bool operator !=(ContentType left, ContentType right) => !(left == right);

        public override string ToString() => type.ToString();

    }



}
