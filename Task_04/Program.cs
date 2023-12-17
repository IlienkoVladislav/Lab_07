using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_04
{
    internal class Program
    {
        static void Main()
        {
            var eventSystem = new EventSystem();

            eventSystem.Subscribe(ProcessEvent);
            eventSystem.Subscribe(ProcessEvent);

            for (int i = 1; i <= 3; i++)
            {
                int nodeId = i;
                Thread nodeThread = new Thread(() => SimulateNodeEventRegistration(nodeId, eventSystem));
                nodeThread.Start();

                Thread.Sleep(1000);
                Console.WriteLine($"Removing Node {nodeId} from the system");
                eventSystem.Subscribe(ProcessEvent);
                nodeThread.Join();
            }
        }

        static void SimulateNodeEventRegistration(int nodeId, EventSystem eventSystem)
        {
            for (int i = 0; i < 3; i++)
            {
                int timestamp = i + 1;
                eventSystem.RegisterEvent(nodeId, timestamp);
                Thread.Sleep(500);
            }
        }

        static void ProcessEvent(Event newEvent)
        {
            Console.WriteLine($"Node {newEvent.NodeId} processed Event {newEvent.EventId} at Timestamp {newEvent.Timestamp}");
        }
    }
}
