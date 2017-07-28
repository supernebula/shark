
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

    public struct PageType /*: IEquatable<PageType>*/
    {
        private string type;

        private static readonly PageType staticPage = new PageType("static");
        private static readonly PageType dynamicPage = new PageType("dynamic");

        public static PageType Static => staticPage;

        public static PageType Dynamic => dynamicPage;

        public string Type => type;

        public PageType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException(nameof(type));
            }
            this.type = type;
        }

        //public bool Equals(PageType other)
        //{
        //    if (other == default(PageType))
        //    {
        //        return false;
        //    }
        //    return ((this.type == other.type) || (string.Compare(this.type, other.type, StringComparison.OrdinalIgnoreCase) == 0));
        //}

        //public override bool Equals(object obj) => Equals(obj as PageType);

       // public override int GetHashCode() => type.ToUpperInvariant().GetHashCode();

        //public static bool operator == (PageType left, PageType right)
        //{
        //    if (left == null)
        //    {
        //        return (right == null);
        //    }
        //    if (right == null)
        //    {
        //        return (left == null);
        //    }
        //    return left.Equals(right);
        //}

        //public static bool operator != (PageType left, PageType right) => !(left == right);

        //public override string ToString() => type.ToString();

    }



}
