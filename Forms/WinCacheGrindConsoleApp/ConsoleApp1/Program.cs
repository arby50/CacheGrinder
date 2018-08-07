using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using cacher;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PARSING START:" + DateTime.Now.ToLongTimeString());
            TCacheGrind cache = new TCacheGrind();
            cache.Load(@"c:\wamp\tmp\cachegrind.out.1531400630.16376", false);
            Console.WriteLine("PARSING END:" + DateTime.Now.ToLongTimeString());
            Console.ReadLine();
        }
    }
}
