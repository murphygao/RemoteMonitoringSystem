using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Agent.Model
{
    [Serializable]
    public class EventLog
    {
        public DateTime EventDateTime { get; set; }
        public string EventType { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }

    [Serializable]
    public class ListEventLogs : List<EventLog>
    {
        public void Add(EventLog log)
        {
            base.Add(log);
            var orderByDescending = this.OrderByDescending(o => o.EventDateTime).ToList();
            this.Clear();
            this.AddRange(orderByDescending);
            int maxList = 1000;
            if (this.Count > maxList)
                this.RemoveRange(maxList, this.Count - maxList);
        }
        public void AddRange(ListEventLogs logs)
        {
            base.AddRange(logs);
            var orderByDescending = this.OrderByDescending(o => o.EventDateTime).ToList();
            this.Clear();
            this.AddRange(orderByDescending);
            int maxList = 1000;
            if (this.Count > maxList)
                this.RemoveRange(maxList, this.Count - maxList);
        }
    }
}
