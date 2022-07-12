using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClientDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string NewLine = "\r\n";
            //await ReadData();
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 12345);
            tcpListener.Start();
            //daemon // service
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    int byteLength = 0;
                    byte[] buffer = new byte[1000000];
                    var length = stream.Read(buffer, byteLength, buffer.Length);

                    string requestString = Encoding.UTF8.GetString(buffer, 0, length);

                    Console.WriteLine(requestString);
                    string html = $"<h1>Hello from ZoriServer {DateTime.Now}</h1>" +
                        $"<form method=post><input name=username /><input name=password />" +
                        $"<input type=submit /></form>";


                    string response = 
                        "HTTP/1.1 200 OK" + NewLine +
                        "Server: ZoriServer 2022" + NewLine +
                        "Content-Type: text/html; charset= utf-8" + NewLine +
                        "Content-Length: " + html.Length + NewLine +
                        NewLine +
                        html
                        + NewLine;

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                    stream.Write(responseBytes);


                    Console.WriteLine("===========");
                }
                
            }
            
        }

        public static async Task ReadData()
        {
            Console.OutputEncoding = Encoding.Unicode;
            string url = "https://softuni.bg/courses/csharp-web-basics";
            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("X-text", "test...");
            var response = await httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(
               string.Join(Environment.NewLine, 
               response.Headers.Select(x => x.Key + ": " + x.Value.First())));
            //var html = await httpClient.GetStringAsync(url);
            //Console.WriteLine(html);
        }
    }
}
