
using System;
using System.Net.Http;

namespace Plunder.Download
{

    public struct PageType
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

    }



}
