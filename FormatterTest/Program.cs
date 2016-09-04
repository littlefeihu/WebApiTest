using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FormatterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:38232/api/contacts");
            request.Method = "POST";
            request.ContentType = "application/xml;charset=utf-8";
            string content = "<Contact xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/WebAPITest.Models\"><Address><City>合肥</City><District>庐阳产业园</District><Province>安徽省</Province><Street>阜阳路1008号</Street></Address><EmailAddress>378917466@qq.com</EmailAddress><Name>张三</Name><PhoneNo>18949881803</PhoneNo></Contact>";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            request.GetRequestStream().Write(buffer,0,buffer.Length);
            request.GetResponse();

        }
    }
}
