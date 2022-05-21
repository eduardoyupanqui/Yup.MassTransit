using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yup.MassTransit.Abstractions;
using Yup.MassTransit.Events;

namespace Yup.MassTransit.Implements
{
    public class PublishEndpointWrapper : IEventBus
    {
       private readonly IPublishEndpoint _publishEndpoint;
       public PublishEndpointWrapper(IPublishEndpoint publishEndpoint)
       {
           _publishEndpoint = publishEndpoint;
       }
       public Task Publish<T>(T eventMessage) where T: IntegrationEvent
       {
           return _publishEndpoint.Publish<T>(eventMessage); ;
       }
    }
}
