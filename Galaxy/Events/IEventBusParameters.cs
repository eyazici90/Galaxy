using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Events
{
    public interface IEventBusParameters
    {
        string Broker_Name { get; set; }
        string Queue_Name { get; set; }
    }
}
