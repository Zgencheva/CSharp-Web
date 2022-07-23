using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace SUS.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            var lines = requestString.Split(new string[]{ HttpConstants.NewLine }, 
                StringSplitOptions.None);
            //GET /somepage HTTP/1.1
            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(" ");
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), 
                headerLineParts[0], true);
            this.Path = headerLineParts[1];

            int lineIndx = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (lineIndx < lines.Length)
            {
                var line = lines[lineIndx];
                lineIndx++;
                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }

                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    //this.Body += (line + HttpConstants.NewLine);
                    bodyBuilder.AppendLine(line);
                }
               
            }
            if (this.Headers.Any(x=> x.Name == HttpConstants.CookieName))
            {
                var cookiesAsString = this.Headers.FirstOrDefault(x => 
                x.Name == HttpConstants.CookieName).Value;
                var cookies = cookiesAsString.Split(new string[] { "; "}, 
                    StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookieAsString in cookies)
                {
                    this.Cookies.Add(new Cookie(cookieAsString));
                }

            }
            this.Body = bodyBuilder.ToString();
        }

        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public string Body { get; set; }
    }
}