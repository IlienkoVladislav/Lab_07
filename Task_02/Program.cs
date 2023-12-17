using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_02
{
    class Program
    {
        static void Main()
        {
            var cpu = new Resource("CPU");
            var ram = new Resource("RAM");
            var disk = new Resource("Disk");

            var threads = new List<Thread>();

            for (int i = 1; i <= 5; i++)
            {
                int priority = i <= 2 ? 1 : 2; 
                Thread thread = new Thread(() => AccessResources(cpu, ram, disk, priority));
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        static void AccessResources(Resource cpu, Resource ram, Resource disk, int priority)
        {
            var random = new Random();
            var threadId = Thread.CurrentThread.ManagedThreadId;

            cpu.Acquire(threadId, priority);
            Thread.Sleep(random.Next(100, 500)); 
            cpu.Release(threadId, priority);

            ram.Acquire(threadId, priority);
            Thread.Sleep(random.Next(100, 500));
            ram.Release(threadId, priority);

            disk.Acquire(threadId, priority);
            Thread.Sleep(random.Next(100, 500));
            disk.Release(threadId, priority);
        }
    }
}
