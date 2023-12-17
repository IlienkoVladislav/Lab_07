using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_04
{
    internal class Event
    {
        public int EventId { get; }
        public int NodeId { get; }
        public int Timestamp { get; }

        public Event(int eventId, int nodeId, int timestamp)
        {
            EventId = eventId;
            NodeId = nodeId;
            Timestamp = timestamp;
        }
    }
}
