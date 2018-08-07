using System;

namespace cacher
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
