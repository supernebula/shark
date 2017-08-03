using System;
using System.Collections.Generic;

namespace Plunder.Compoment
{
    public class ExtractRule
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public List<ExtractField> Fields { get; set; }

        public bool IsMatch(string htmlDocument)
        {
            throw new NotImplementedException();
        }
    }


}
