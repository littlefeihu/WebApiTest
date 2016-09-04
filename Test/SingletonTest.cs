using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public class SingletonTest
    {
        private static SingletonTest instance;
        private SingletonTest()
        {

        }
        static SingletonTest()
        {
            instance = new SingletonTest();
        }
        public static SingletonTest Instance
        {
            get
            {
                return instance;
            }
        }

        public int SleepTest()
        {
            Console.WriteLine("SleepTest");
            Thread.Sleep(5000); Console.WriteLine("SleepTest1");
            return 1;
        }
        public async Task<int> SleepTestAsync()
        {
            Console.WriteLine("SleepTestAsync");
            await Task.Delay(5000);
            return await Task.Run<int>(() => { return 7; });
        }
    }
}
