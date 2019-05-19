using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Azure.EventGrid
{
    public static class GalaxyAzureEventGridFactory
    {
        public static GalaxyAzureEventGridBus Create(Action<IGalaxyAzureEventGridConfigurations> configurations)
        {
            var configs = new GalaxyAzureEventGridConfigurations();

            configurations(configs);

            var topicCreadentials = new TopicCredentials(configs.AccessKey);

            var client = new EventGridClient(topicCreadentials);

            return new GalaxyAzureEventGridBus(client, configs);
        }
    }
}
