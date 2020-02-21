using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net.Http;
using CfAppTestSuite.OutboundConnection.Models;

namespace CfAppTestSuite.OutboundConnection.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Http(string url)
        {
            var httpConnectionResult = new HttpConnectionResult();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetStringAsync(url);
                    httpConnectionResult.Successful = true;
                    httpConnectionResult.Content = result;
                }
                catch (Exception ex)
                {
                    httpConnectionResult.Exception = ex;
                    httpConnectionResult.Successful = false;
                }
            }
            return View(httpConnectionResult);
        }

        public async Task<IActionResult> Tcp(string host, int port)
        {
            var tcpConnectionResult = new TcpConnectionResult();
            using (var client = new TcpClient())
            {
                try
                {
                    client.SendTimeout = 1500;
                    client.ReceiveTimeout = 1500;
                    await client.ConnectAsync(host, port);
                    tcpConnectionResult.Successful = true;
                }
                catch (Exception ex)
                {
                    tcpConnectionResult.Successful = false;
                    tcpConnectionResult.Exception = ex;
                }
            }
            return View(tcpConnectionResult);
        }
    }
}