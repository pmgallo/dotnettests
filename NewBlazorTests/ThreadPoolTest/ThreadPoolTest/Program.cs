using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((new OnlineRequestDispatcher()).Run));
            ThreadPool.QueueUserWorkItem(new WaitCallback((new BatchRequestDispatcher()).Run));
            Console.ReadLine();
        }
    }
}
