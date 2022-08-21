using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public class HttpGetAttribute : BaseHttpAttribute
    {
        public HttpGetAttribute()
        {
                
        }
        public HttpGetAttribute(string url)
        {
            this.Url = url;
        }
        public override HttpMethod Method => HttpMethod.Get;
    }
}
