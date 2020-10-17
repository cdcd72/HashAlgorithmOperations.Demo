using ConsoleApp.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
            Algorithm algorithm;
            // 操作
            Operate operate;

#if DEBUG
            if (args.Length == 0)
                args = new string[] { "HMACSHA256", "Hash", "Test" };
#endif

            if (args.Length > 0)
            {
                algorithm = (Algorithm)System.Enum.Parse(typeof(Algorithm), args[0]);
                operate = (Operate)System.Enum.Parse(typeof(Operate), args[1]);

                switch (algorithm)
                {
                    case Algorithm.HMACSHA256:
                        // Command call "ConsoleApp HMACSHA256 Hash message"
                        // Command call "ConsoleApp HMACSHA256 Verify message hash"
                        ExecuteHMACSHA256(operate, args);
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
            serviceCollection.AddSingleton(new HashTool(Config));

            return serviceCollection.BuildServiceProvider();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 執行 HMACSHA256 加密
        /// </summary>
        /// <param name="args"></param>
        private static void ExecuteHMACSHA256(Operate operate, string[] args)
        {
            var result = new Dictionary<string, object>();

            // 訊息內容
            var message = args[2];
            
            switch (operate)
            {
                case Operate.Hash:
                    result.Add(nameof(Operate.Hash), HashTool.HMACSHA256(message));
                    break;
                case Operate.Verify:
                    var hash = args[3];
                    result.Add(nameof(Operate.Verify), HashTool.VerifyHMACSHA256(message, hash));
                    break;
                default:
                    break;
            }

            // 顯示結果
            DisplayResult(result);
        }

        /// <summary>
        /// 顯示結果
        /// </summary>
        public static void DisplayResult(Dictionary<string, object> result)
        {
            if(result.Count > 0)
            {
                foreach (var key in result.Keys)
                {
                    Console.WriteLine($"{key}:{result[key]}");
                }
            }
        }

        #endregion
    }
}
