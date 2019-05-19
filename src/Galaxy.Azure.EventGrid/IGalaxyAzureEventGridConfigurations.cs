using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Azure.EventGrid
{
    public interface IGalaxyAzureEventGridConfigurations
    {
        string TopicUrl { get; set; }
        string AccessKey { get; set; }
        string TopicName { get; set; }
    }
}
