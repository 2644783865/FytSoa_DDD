using System;
using Xunit;
using Snowflake.Core;
using System.Net;
using System.Collections.Generic;
using FytSoa.Common;
using System.Threading;
using System.Threading.Tasks;

namespace FytSoa.Test
{
    public class UnitTest1
    {
        private static int N = 2000000;
        private static HashSet<string> set = new HashSet<string>();

        private static object o = new object();
        private static int taskCount = 0;

        [Fact]
        public void Test1()
        {
            Unique.GetInstance();
            taskCount = 0;
            set.Clear();
            Task.Run(() => GetID());
            Task.Run(() => GetID());
            Task.Run(() => GetID());
            Printf();
        }

        private void Printf()
        {
            while (taskCount != 3)
            {
                Console.WriteLine("...");
                Thread.Sleep(1000);
            }
            Console.WriteLine(set.Count == N * taskCount);
            Console.WriteLine("执行数量：" + set.Count);
            Console.WriteLine("进程数量：" + taskCount);
            Assert.Equal(set.Count, N * taskCount);
        }

        private static void GetID()
        {
            for (var i = 0; i < N; i++)
            {
                var id = Unique.Id();

                lock (o)
                {
                    if (set.Contains(id))
                    {
                        Console.WriteLine("发现重复项 : {0}", id);
                    }
                    else
                    {
                        set.Add(id);
                    }
                }

            }
            Console.WriteLine($"任务{++taskCount}完成");
        }
    }
}
