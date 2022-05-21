using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yup.MassTransit.Events;

namespace Yup.MassTransit.Abstractions
{
    public interface IEventBus
    {
        Task Publish<T>(T eventMessage) where T : IntegrationEvent;
    }
}
