using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_03
{
    public class Operation
    {
        public int ThreadId { get; }
        public long Timestamp { get; }

        public Operation(int threadId)
        {
            ThreadId = threadId;
            Timestamp = Stopwatch.GetTimestamp();
        }
    }
}
