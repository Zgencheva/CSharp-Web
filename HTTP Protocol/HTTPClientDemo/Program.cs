using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPClientDemo
{
    class Program
    {
        static Dictionary<string, int> SessionStorage = new Dictionary<string, int>();
        const string NewLine = "\r\n";
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //await ReadData();
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();
            //daemon // service
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                ProcessClientAsync(client);  
            }
            
        }

        public static async Task ProcessClientAsync(TcpClient client) 
        {
            Console.WriteLine(client.Client.RemoteEndPoint);
            using (var stream = client.GetStream())
            {
                
                byte[] buffer = new byte[1000000];
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                var length = await stream.ReadAsync(buffer, 0, buffer.Length);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                string requestString =
                                   Encoding.UTF8.GetString(buffer, 0, length);
                Console.WriteLine(requestString);
                var sid = Guid.NewGuid().ToString();
                var match = Regex.Match(requestString, @"sid=[^\n]*\r\n");
                if (match.Success)
                {
                    sid = match.Value.Substring(4);
                }

                if (!SessionStorage.ContainsKey(sid))
                {
                    SessionStorage.Add(sid, 0);
                }
                SessionStorage[sid]++;
                Console.WriteLine(sid);
                string html = $"<h1>Hello from ZoriServer {DateTime.Now} for the {SessionStorage[sid]} time</h1>" +
                    $"<form method=post><input name=username /><input name=password />" +
                    $"<input type=submit /></form>";


                string response =
                    "HTTP/1.1 200 OK" + NewLine +
                    "Server: ZoriServer 2022" + NewLine +
                    "Content-Type: text/html; charset= utf-8" + NewLine +
                    "X-Server-Version: 1.0" + NewLine +
                    //(!sessionSet ? ("Set - Cookie: sid = 43432sfklsdjflkasdj430sdflads; Path =/; " + NewLine) : string.Empty) +
                    $"Set-Cookie: sid={sid}; HTTPOnly; Secure; "+ NewLine +
                    //"Set-Cookie: sid2=0;" + NewLine +
                    "Content-Length: " + html.Length + NewLine +
                    NewLine +
                    html
                    + NewLine;

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                await stream.WriteAsync(responseBytes);


                Console.WriteLine("===========");
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
