using System.Collections.Generic;
using System;
using System.Text;

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
            this.Method = headerLineParts[0];
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
            this.Body = bodyBuilder.ToString();
        }

        public string Path { get; set; }
        public string Method { get; set; }
        public List<Header> Headers { get; set; }
        public List<Cookie> Cookies { get; set; }
        public string Body { get; set; }
    }
}