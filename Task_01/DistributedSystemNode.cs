using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_01
{
    public class DistributedSystemNode
    {
        private readonly int nodeId;
        private bool isActive;
        private readonly List<DistributedSystemNode> connectedNodes;

        public DistributedSystemNode(int nodeId)
        {
            this.nodeId = nodeId;
            this.isActive = true;
            this.connectedNodes = new List<DistributedSystemNode>();
        }

        public async Task SendMessageAsync(string message, DistributedSystemNode destinationNode)
        {
            
            await Task.Delay(100);

            Console.WriteLine($"Node {nodeId} sent message: '{message}' to Node {destinationNode.nodeId}");
            await destinationNode.ReceiveMessageAsync($"Reply to: '{message}'", this);
        }

        public async Task ReceiveMessageAsync(string message, DistributedSystemNode senderNode)
        {
            await Task.Delay(100);

            Console.WriteLine($"Node {nodeId} received message: '{message}' from Node {senderNode.nodeId}");
        }

        public void NotifyStatus()
        {
            Console.WriteLine($"Node {nodeId} is {(isActive ? "active" : "inactive")}");
            foreach (var node in connectedNodes)
            {
                node.ReceiveStatusNotification(nodeId, isActive);
            }
        }

        public void ReceiveStatusNotification(int senderNodeId, bool status)
        {
            Console.WriteLine($"Node {nodeId} received status notification from Node {senderNodeId}: {(status ? "active" : "inactive")}");
        }

        public void ConnectToNode(DistributedSystemNode node)
        {
            connectedNodes.Add(node);
        }

        public async Task SimulateNodeActivityAsync()
        {
            while (true)
            {
                await Task.Delay(1000);

                isActive = !isActive;
                NotifyStatus();
            }
        }
    }
}
