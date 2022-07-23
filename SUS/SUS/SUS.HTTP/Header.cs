using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class Header
    {
        public Header(string headerLine)
        {
            var headerParts = headerLine.Split(new string[] { ": " },
                2, StringSplitOptions.None);
            this.Name = headerParts[0];
            this.Value = headerParts[1];
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
