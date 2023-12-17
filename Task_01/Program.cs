using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_01
{
    internal class Program
    {
        static async Task Main()
        {
            var node1 = new DistributedSystemNode(1);
            var node2 = new DistributedSystemNode(2);

            node1.ConnectToNode(node2);
            node2.ConnectToNode(node1);

            var node1ActivityTask = node1.SimulateNodeActivityAsync();

            await node1.SendMessageAsync("Hello from Node 1", node2);

            await node1ActivityTask;
        }
    }
}
