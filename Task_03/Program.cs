using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_03
{
    class Program
    {
        static ConflictLogger conflictLogger = new ConflictLogger();

        static void Main()
        {
            var threads = new List<Thread>();
            for (int i = 1; i <= 5; i++)
            {
                Thread thread = new Thread(() => PerformOperation(i));
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

        static void PerformOperation(int threadId)
        {
            for (int i = 0; i < 5; i++)
            {
                var operation = new Operation(threadId);
                Console.WriteLine($"Thread {threadId} performing operation at {operation.Timestamp}");
                if (conflictLogger.DetectConflict(operation))
                {
                    conflictLogger.ResolveConflict();
                }
                else
                {
                    conflictLogger.LogOperation(operation);
                }

                Thread.Sleep(100);
            }
        }
    }
}
