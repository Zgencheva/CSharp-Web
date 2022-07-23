using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpServer : IHttpServer
    {
       

        IDictionary<string, Func<HttpRequest, HttpResponse>>
            routeTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();
        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            if (routeTable.ContainsKey(path))
            {
                routeTable[path] = action;
            }
            else
            {
                routeTable.Add(path, action);
            }
            
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                //NOT ASYNC!
                ProcessClientAsync(tcpClient);
                //ProcessClientAsync is not async, beacause we don`t want to wait this client to
                //finish his work, but we want to accept next client.
                //This way we make the multithread.
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            //two directions data stream. Buffer collect data and send it to the other
            //end of the direction. Everything using IDisposable should be in using;
            using NetworkStream stream = tcpClient.GetStream();
            List<byte> data = new List<byte>();
            int position = 0;
            byte[] buffer = new byte[HttpConstants.BufferSize];

            while (true)
            {
                int count = await stream.ReadAsync(buffer, position, buffer.Length);
                position += count;

                if (count < buffer.Length)
                {
                    var partialBuffer = new byte[count];
                    Array.Copy(buffer, partialBuffer, count);
                    data.AddRange(partialBuffer);
                    break;
                }
                else
                {
                    data.AddRange(buffer);
                }
            }

            //when we want to receive a text from array of bytes we make Encoding.
            //Encoding knows how to make byte[] to string.
            //ASCII makes 1 byte to 1 symbol
            //unicode makes 2 bytes into 1 symbol.
            //UTF-8 1-2-3-4 bytes makes 1 symbol. 
            //HTP uses Utf-8;
            var requesAsString = Encoding.UTF8.GetString(data.ToArray());
            var request = new HttpRequest(requesAsString);

            Console.WriteLine(requesAsString);

            var resposeHtml = "<h1>Welcome!</h1>" +
                request.Headers.FirstOrDefault(x=> x.Name == "User-Agent")?.Value;
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            var resposeHttp = "HTTP/1.1 200 OK" + HttpConstants.NewLine +
                               "Server: SUS Server 1.0" + HttpConstants.NewLine +
                               "Content-Type: text/html" + HttpConstants.NewLine +
                               "Content-Length: " + responseBodyBytes.Length + HttpConstants.NewLine +
                                HttpConstants.NewLine;
            var resposeHeaderBytes = Encoding.UTF8.GetBytes(resposeHttp);

            await stream.WriteAsync(resposeHeaderBytes, 0, resposeHeaderBytes.Length);
            await stream.WriteAsync(responseBodyBytes, 0, responseBodyBytes.Length);
            //await stream.WriteAsync();

            tcpClient.Close();
        }

    }
}
