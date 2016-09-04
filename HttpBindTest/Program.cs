using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ValueProviders;

namespace HttpBindTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Uri listenUri = new Uri("http://127.0.0.1:3721");
            //var  binding = new HttpBinding();
            ValueProviderResult result1 = new ValueProviderResult ( new string[] { "1","2","3"},"",null );

            ValueProviderResult result2 = new ValueProviderResult("123", "", null);

            var value1 = (int [])result1.ConvertTo(typeof(int[]));
            var value2 = result1.ConvertTo(typeof(int));
            int[] value3 = (int [])result2.ConvertTo(typeof(int []));

            Console.WriteLine(result1.RawValue.ToString()+""+value1.ToString());
            Console.WriteLine(result1.RawValue.ToString()+""+value2.ToString());
            Console.WriteLine(result1.RawValue.ToString()+""+value3.ToString());
            Console.ReadKey();
        }
        
    }
}
