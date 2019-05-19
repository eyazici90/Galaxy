using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Azure.EventGrid
{
    public class GalaxyAzureEventGridConfigurations : IGalaxyAzureEventGridConfigurations
    {
        public string TopicUrl { get ; set ; }
        public string AccessKey { get ; set ; }
        public string TopicName { get ; set ; }
    }
}
