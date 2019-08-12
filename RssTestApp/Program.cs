using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeDde;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var client = new Client();

            IDictionary<string, object> services = new Dictionary<string, object>();
            services.Add("RSS", new Dictionary<string, object>());

            IDictionary<string, object> rss = (IDictionary<string, object>)services["RSS"];
            rss.Add("9501.T", new string[] { "銘柄名称", "現在値" });

            IDictionary<string, object> callbacks = new Dictionary<string, object>();
            callbacks.Add("OnDisconnected", (Func<object, Task<object>>)(async (data) => {
                Console.WriteLine("OnDisconnected");
                return "222";
            }));

            callbacks.Add("OnAdvise", (Func<object, Task<object>>)(async (data) => {
                Console.WriteLine("OnAdvise");
                return "222";
            }));

            var input = new Dictionary<string, object>();
            input.Add("services", services);
            input.Add("callbacks", callbacks);


            Func<object, Task<object>> invoke = (Func<object, Task<object>>)client.GetInvoker(input).Result;

            var res = invoke(new Dictionary<string, object>() {
                { "method", "Service" }
            }).Result;
            Console.WriteLine(res);

            invoke(new Dictionary<string, object>() {
                { "method", "Connect" }
            }).Wait();

            var isConnected = invoke(new Dictionary<string, object>() {
                { "method", "IsConnected" }
            }).Result;

            invoke(new Dictionary<string, object>() {
                { "method", "StartAdvise" }
            }).Wait();

            var reqParam = new Dictionary<string, object>() {
                { "method", "Request" },
            };
            reqParam.Add("item", new string[] { "市場部名称", "現在値詳細時刻", "現在値" });

            var reqRes = invoke(reqParam).Result;
            Console.WriteLine(reqRes);

            /*
            IDictionary<string, object> opts2 = new Dictionary<string, object>();
            opts2.Add("method", "Connect");
            invoke(opts2).Wait();


            IDictionary<string, object> opts3 = new Dictionary<string, object>();
            opts2.Add("method", "Connect");
            invoke(opts2).Wait();*/

            /*
            IDictionary<string, object> method = (IDictionary<string, object>)opts["method"];
            method.Add("Service", (Func<object, Task<object>>)(async (data) => {
                Console.WriteLine("Service");
                return "222";
            }));
            */
            // 

            Console.ReadLine();
        }
    }
}
