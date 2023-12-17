using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_02
{
    class Resource
    {
        public string Name { get; }
        private Semaphore semaphore;
        private Mutex mutex;
        private int ownerThreadId;

        public Resource(string name)
        {
            Name = name;
            semaphore = new Semaphore(1, 1);
            mutex = new Mutex();
            ownerThreadId = -1;
        }

        public void Acquire(int threadId, int priority)
        {
            Console.WriteLine($"Thread {threadId} (Priority {priority}) trying to acquire {Name}");

            if (priority == 1)
            {
                semaphore.WaitOne();
            }
            else
            {
                mutex.WaitOne();
                while (ownerThreadId != -1)
                {
                    Monitor.Wait(mutex);
                }
                ownerThreadId = threadId;
                mutex.ReleaseMutex();
            }

            Console.WriteLine($"Thread {threadId} (Priority {priority}) acquired {Name}");
        }

        public void Release(int threadId, int priority)
        {
            Console.WriteLine($"Thread {threadId} (Priority {priority}) releasing {Name}");

            if (priority == 1)
            {
                semaphore.Release();
            }
            else
            {
                mutex.WaitOne();
                ownerThreadId = -1;
                Monitor.PulseAll(mutex);
                mutex.ReleaseMutex();
            }
        }
    }
}
