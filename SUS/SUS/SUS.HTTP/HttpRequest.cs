using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using System.Net;

namespace SUS.HTTP
{
    public class HttpRequest
    {
        public static IDictionary<string, Dictionary<string, string>>
          Sessions = new Dictionary<string, Dictionary<string, string>>();
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.FormData = new Dictionary<string, string>();

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
            var sessionCookie = this.Cookies.FirstOrDefault(x => x.Name == HttpConstants.SessionCookieName);
            if (sessionCookie == null)
            {
                var sessionId = Guid.NewGuid().ToString();
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionId, this.Session);
                this.Cookies.Add(new Cookie(HttpConstants.SessionCookieName, sessionId));
            }
            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionCookie.Value, this.Session);
            }
            else
            {
                this.Session = Sessions[sessionCookie.Value];
            }

            this.Body = bodyBuilder.ToString();
            var parameters = this.Body.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                var parameterParts = parameter.Split('=');
                var name = parameterParts[0];
                var value = WebUtility.UrlDecode(parameterParts[1]);
                if (!this.FormData.ContainsKey(name))
                {
                    this.FormData.Add(name, value);
                }
                
            }
        }

        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public IDictionary<string, string> FormData { get; set; }
        public Dictionary<string, string> Session { get; set; }

        public string Body { get; set; }
    }
}