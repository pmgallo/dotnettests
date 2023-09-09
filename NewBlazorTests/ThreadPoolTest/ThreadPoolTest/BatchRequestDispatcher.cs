using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    public class BatchRequestDispatcher
    {
        public int currentThreads = 0;
        public int maxThreads = 2;
        public int _iteration = 0;

        public BatchRequestDispatcher()
        {

        }

        public void Run(object o)
        {
            while (true)
            {                
                lock (this)
                {
                    while (currentThreads >= maxThreads)
                    {
                        Monitor.Wait(this);
                    }

                    currentThreads++;
                    _iteration++;
                    Executor ex = new Executor(_iteration, this);
                    Thread t = new Thread(new ThreadStart(ex.Execute));
                    t.IsBackground = true;
                    t.Start();
                }
            }
        }
    }

    public class Executor
    {
        private readonly BatchRequestDispatcher _dispatcher;

        public Executor(int iteration, BatchRequestDispatcher dispatcher)
        {
            _iteration = iteration;
            this._dispatcher = dispatcher;
        }

        public int _iteration { get; }

        public void Execute()
        {
            Console.WriteLine($"BatchRequestDispatcher {_iteration}");
            for (int i = 1; i <= 100000000; i++)
            {
                var h = i + 3;
            }

            lock (_dispatcher)
            {
                _dispatcher.currentThreads--;
                Monitor.Pulse(_dispatcher);
            }
        }
    }

}
