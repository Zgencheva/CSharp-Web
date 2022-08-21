using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public class HttpPostAttribute : BaseHttpAttribute
    {
        public HttpPostAttribute()
        {
                
        }
        public HttpPostAttribute(string url)
        {
            this.Url = url;
        }
        public override HttpMethod Method => HttpMethod.Post;
    }
}
