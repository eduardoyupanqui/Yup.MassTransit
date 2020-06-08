﻿using MassTransit;
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

        private ConsumeContext<JobCommand> _context;
        public JobConsumer(ILogger<T> logger, T executor)
        {
            _logger = logger;
            _executor = executor;

            _executor.ProcessStarted += OnProcessStarted;
            _executor.StatusTarea += OnStatusTarea;
            _executor.ProcessCompleted += OnProcessCompleted;
            _executor.ProcessFailed += OnProcessFailed;
        }



        public async Task Consume(ConsumeContext<JobCommand> context)
        {
            _logger.LogInformation($"JobId: {context.Message.IdJob} , InputJob: {context.Message.InputJob}");
            _context = context;
            await _executor.Execute(context.Message);
        }

        private async Task OnProcessStarted(object sender, JobStarted e)
        {
            _logger.LogInformation($"JobId: {e.IdJob} Started on : {e.FechaInicio}");
            //Comunicar al Master el inicio del job
            if (_context != null)
            {
                await _context.Send<JobStarted>(e);
            }
        }

        private async Task OnStatusTarea(object sender, JobTaskCompleted e)
        {
            _logger.LogInformation($"JobId: {e.IdJob} Execute Tarea {e.Orden} : {e.Mensaje}");
            //Comunicar al Master el progreso de las tareas
            if (_context != null)
            {
                await _context.Send<JobTaskCompleted>(e);
            }
        }
        private async Task OnProcessCompleted(object sender, JobCompleted e)
        {
            _logger.LogInformation($"JobId: {e.IdJob} Complete on : {e.FechaFin}");
            //Comunicar al Master el fin del job
            if (_context != null)
            {
                await _context.Send<JobCompleted>(e);
            }
        }

        private async Task OnProcessFailed(object sender, JobFailed e)
        {
            _logger.LogInformation($"JobId: {e.IdJob} Failed on : {e.Mensaje} {e.StackTrace}");
            //Comunicar al Master que el job ha fallado
            if (_context != null)
            {
                await _context.Send<JobFailed>(e);
            }
        }
    }
}
