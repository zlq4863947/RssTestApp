using System;
using System.Collections.Generic;
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
            services.Add("myapp", new Dictionary<string, object>());

            IDictionary<string, object> myapp = (IDictionary<string, object>)services["myapp"];
            myapp.Add("mytopic", new List<string>());

            List<string> mytopics = (List<string>)myapp["mytopic"];
            mytopics.Add("item1");
            mytopics.Add("item2");

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


            object x = client.GetInvoker(input).Result;

            Console.ReadLine();
        }
    }
}
