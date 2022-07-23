﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class Cookie
    {
        public Cookie(string cookieAsString)
        {
            var cookieParts = cookieAsString.Split(new char[] {'='}, 2);
            this.Name = cookieParts[0];
            this.Value = cookieParts[1];
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}
