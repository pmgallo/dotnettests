using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    public class OnlineRequestDispatcher
    {
        private int _currentThreadPoolSize = 0;
        private int _maxThreadPoolSize = 2;
        private int _iteration = 0;

        public void Run(object o)
        {
            while (true)
            {
                lock (this)
                {
                    while (_currentThreadPoolSize >= _maxThreadPoolSize)
                    {
                        Monitor.Wait(this);
                    }

                    _currentThreadPoolSize++;
                    _iteration++;
                    ThreadPool.QueueUserWorkItem(Execute, _iteration);


                }
            }
        }

        private void Execute(object o)
        {
            Console.WriteLine($"OnlineRequestDispatcher {_iteration}");
            for(int i = 1; i <= 500000000; i++)
            {
                var h = i + 3;
            }
            lock (this)
            {
                _currentThreadPoolSize--;
                Monitor.Pulse(this);
            }
        }
    }
}
