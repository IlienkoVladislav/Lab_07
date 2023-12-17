using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_04
{
    internal class EventSystem
    {
        private readonly List<Event> eventLog = new List<Event>();
        private readonly object logLock = new object();

        public void RegisterEvent(int nodeId, int timestamp)
        {
            lock (logLock)
            {
                int eventId = eventLog.Count + 1;
                var newEvent = new Event(eventId, nodeId, timestamp);
                eventLog.Add(newEvent);
                Console.WriteLine($"Node {nodeId} registered Event {eventId} at Timestamp {timestamp}");
                NotifySubscribers(newEvent);
            }
        }

        internal void Subscribe(Action<Event> callback)
        {
            EventPublisher.Subscribe(callback);
        }

        private void NotifySubscribers(Event newEvent)
        {
            EventPublisher.NotifySubscribers(newEvent);
        }
    }

    public static class EventPublisher
    {
        private static readonly List<Action<Event>> subscribers = new List<Action<Event>>();
        private static readonly object subscribersLock = new object();

        internal static void Subscribe(Action<Event> callback)
        {
            lock (subscribersLock)
            {
                subscribers.Add(callback);
            }
        }

        internal static void NotifySubscribers(Event newEvent)
        {
            lock (subscribersLock)
            {
                foreach (var subscriber in subscribers)
                {
                    subscriber(newEvent);
                }
            }
        }
    }
}
