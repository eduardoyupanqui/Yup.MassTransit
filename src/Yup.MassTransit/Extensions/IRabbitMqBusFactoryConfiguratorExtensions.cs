using Yup.MassTransit.Jobs;
using Yup.MassTransit.Jobs.Consumers;
using MassTransit;
using System;
using Yup.MassTransit.Jobs.Events;
using Yup.MassTransit.Events;

namespace Yup.MassTransit.Extensions
{
    public static class IRabbitMqBusFactoryConfiguratorExtensions
    {
        public static void ReceivedJobEndpoint<T>(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext provider, string host = null)
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

        public static void Subscribe<T, TH>(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext provider)
            where T : IntegrationEvent
            where TH : class, IConsumer<T>
        {
            cfg.ReceiveEndpoint(typeof(T).Name.ToKebabCase(), ep =>
            {
                //ep.UseMessageRetry(r => r.None());
                //ep.DiscardFaultedMessages();
                //ep.DiscardSkippedMessages();
                ep.Consumer<TH>(provider);
                //EndpointConvention.Map<T>(ep.InputAddress);
            });
        }
        
    }
}
