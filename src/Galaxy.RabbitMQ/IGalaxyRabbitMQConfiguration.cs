using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.RabbitMQ
{
    public interface IGalaxyRabbitMQConfiguration
    {
        string HostAddress { get; set; }

        string Username { get; set; }

        string Password { get; set; }

        string QueueName { get; set; }

        bool AutoDeleted { get; set; }

        bool UseRetryMechanism { get; set; }

        int MaxRetryCount { get; set; }

        int? PrefetchCount { get; set; }

        int? ConcurrencyLimit { get; set; }
    }
}
