using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                new Thread(() =>
                {
                    SingletonTest.Instance.SleepTest();
                }).Start();
            }
            for (int i = 0; i < 20; i++)
            {
                SingletonTest.Instance.SleepTestAsync();
            }
            Console.ReadKey();
        }
    }
}
