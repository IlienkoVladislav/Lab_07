using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_03
{
    public class ConflictLogger
    {
        private readonly List<Operation> operationLog = new List<Operation>();
        private readonly object logLock = new object();

        public void LogOperation(Operation operation)
        {
            lock (logLock)
            {
                operationLog.Add(operation);
            }
        }

        public bool DetectConflict(Operation newOperation)
        {
            lock (logLock)
            {
                foreach (var operation in operationLog)
                {
                    if (operation.ThreadId != newOperation.ThreadId && Math.Abs(operation.Timestamp - newOperation.Timestamp) < 100)
                    {
                        Console.WriteLine($"Conflict detected between Thread {operation.ThreadId} and Thread {newOperation.ThreadId}.");
                        return true;
                    }
                }
                return false;
            }
        }

        public void ResolveConflict()
        {
            Console.WriteLine("Conflict resolution in progress...");
        }
    }
}
