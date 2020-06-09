using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yup.MassTransit.Jobs.Client.EventArgs;
using Yup.MassTransit.Jobs.Commands;
using Yup.MassTransit.Jobs.Events;

namespace Yup.MassTransit.Jobs.Consumers
{
    public class JobConsumer<T> : IConsumer<JobCommand> where T : IExecutor
    {
        private readonly ILogger _logger;
        private readonly T _executor;

        public JobConsumer(ILogger<T> logger, T executor)
        {
            _logger = logger;
            _executor = executor;
        }

        public async Task Consume(ConsumeContext<JobCommand> context)
        {
            _logger.LogInformation($"JobId: {context.Message.IdJob} , InputJob: {context.Message.InputJob}");

            _executor.ProcessStarted += (sender, e) => Task.Run(() => _logger.LogInformation($"JobId: {e.IdJob} Started on : {e.FechaInicio}"));
            _executor.StatusTarea += (sender, e) => Task.Run(() => _logger.LogInformation($"JobId: {e.IdJob} Execute Tarea {e.Orden} : {e.Mensaje}"));
            _executor.ProcessCompleted += (sender, e) => Task.Run(() => _logger.LogInformation($"JobId: {e.IdJob} Complete on : {e.FechaFin}"));
            _executor.ProcessFailed += (sender, e) => Task.Run(() => _logger.LogInformation($"JobId: {e.IdJob} Failed on : {e.Mensaje} {e.StackTrace}"));

            _executor.ProcessStarted += (sender, e) => context.Send<JobStarted>(e);
            _executor.StatusTarea += (sender, e) => context.Send<JobTaskCompleted>(e);
            _executor.ProcessCompleted += (sender, e) => context.Send<JobCompleted>(e);
            _executor.ProcessFailed += (sender, e) => context.Send<JobFailed>(e);

            var result = await _executor.Execute(context.Message);
            _logger.LogInformation($"JobId: {context.Message.IdJob} , OutputJob: {result.OutputJob}");
        }
    }
}
