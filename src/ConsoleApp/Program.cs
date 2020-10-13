using HashCore;
using System;

namespace ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var message = "哈哈";
            var key = "123";
            var hash = HashTool.HMACSHA256(message, key);

            Console.WriteLine(hash);
            Console.WriteLine(HashTool.VerifyHMACSHA256(hash, message, key));
        }
    }
}
