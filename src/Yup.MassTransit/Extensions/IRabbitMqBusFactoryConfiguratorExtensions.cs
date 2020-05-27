using Yup.MassTransit.Jobs;
using Yup.MassTransit.Jobs.Commands;
using Yup.MassTransit.Jobs.Consumers;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;
using Yup.MassTransit.Jobs.Events;

namespace Yup.MassTransit.Extensions
{
    public static class IRabbitMqBusFactoryConfiguratorExtensions
    {
        public static void ReceivedJobEndpoint<T>(this IRabbitMqBusFactoryConfigurator cfg, IServiceProvider provider, string host = null)
            where T : BaseExecutor
        {
            EndpointConvention.Map<JobStarted>(new Uri($"queue:{typeof(JobEvent).Name.ToUnderscoreCase()}"));
            EndpointConvention.Map<JobTaskCompleted>(new Uri($"queue:{typeof(JobEvent).Name.ToUnderscoreCase()}"));
            EndpointConvention.Map<JobCompleted>(new Uri($"queue:{typeof(JobEvent).Name.ToUnderscoreCase()}"));
            EndpointConvention.Map<JobFailed>(new Uri($"queue:{typeof(JobEvent).Name.ToUnderscoreCase()}"));
            cfg.ReceiveEndpoint(typeof(T).Name.ToUnderscoreCase().ToConcatHost(host), ep =>
            {
                ep.Consumer<JobConsumer<T>>(provider);
            });
        }
    }
}
