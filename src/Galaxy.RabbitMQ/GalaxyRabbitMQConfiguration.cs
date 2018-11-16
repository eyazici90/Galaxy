using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.RabbitMQ
{
    public sealed class GalaxyRabbitMQConfiguration : IGalaxyRabbitMQConfiguration
    {
        public string LifetimeScopeTag { get; set; }

        public string HostAddress { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string QueueName { get; set; }

        public bool UseRetryMechanism { get; set; }

        public int MaxRetryCount { get; set; }

        public int? PrefetchCount { get; set; }

        public int? ConcurrencyLimit { get; set; }

        public bool AutoDeleted { get; set; }
    }
}
