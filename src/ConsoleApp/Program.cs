using HashCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ConsoleApp
{
    static class Program
    {
        /// <summary>
        /// 組態設定
        /// </summary>
        private static IConfiguration Config => InitializeConfiguration();

        /// <summary>
        /// 服務提供者
        /// </summary>
        private static IServiceProvider ServiceProvider => InitializeServiceProvider();

        /// <summary>
        /// 雜湊工具
        /// </summary>
        private static HashTool HashTool => ServiceProvider.GetService<HashTool>();

        /// <summary>
        /// 主程式(進入點)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 演算法
            string algorithm;

            // 此行暫時測試用
            // args = new string[] { "HMACSHA256", "123" };

            if (args.Length != 0)
            {
                algorithm = args[0];

                switch (algorithm)
                {
                    case "HMACSHA256":
                        DoHMACSHA256(args);
                        break;
                    default:
                        break;
                }
            }
        }

        #region Initialize

        /// <summary>
        /// 初始化組態設定
        /// </summary>
        /// <returns></returns>
        static IConfiguration InitializeConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("secretsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"secretsettings.{Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .Build();
        }

        /// <summary>
        /// 初始化注入服務
        /// </summary>
        /// <returns></returns>
        static IServiceProvider InitializeServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            // 注入雜湊工具
            serviceCollection.AddSingleton(new HashTool());

            return serviceCollection.BuildServiceProvider();
        }

        #endregion

        #region Private Method

        private static void DoHMACSHA256(string[] args)
        {
            #region 輸入

            var message = args[1];
            var key = args.Length > 2 ? args[2] : Config.GetValue<string>("HMACSHA256:Key");

            #endregion

            var hash = HashTool.HMACSHA256(message, key);

            Console.WriteLine($"Message:{message}");
            Console.WriteLine($"Key:{key}");
            Console.WriteLine($"Hash:{hash}");
            Console.WriteLine($"Verify:{HashTool.VerifyHMACSHA256(hash, message, key)}");
        }

        #endregion
    }
}
