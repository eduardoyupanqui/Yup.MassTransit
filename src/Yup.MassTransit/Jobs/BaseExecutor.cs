using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yup.MassTransit.Jobs.Client.EventArgs;
using Yup.MassTransit.Jobs.Commands;
using Yup.MassTransit.Jobs.Events;

namespace Yup.MassTransit.Jobs
{
    public abstract class BaseExecutor : IExecutor
    {
        public event AsyncEventHandler<JobStarted> ProcessStarted;
        public event AsyncEventHandler<JobTaskCompleted> StatusTarea;
        public event AsyncEventHandler<JobCompleted> ProcessCompleted;
        public event AsyncEventHandler<JobFailed> ProcessFailed;

        protected BaseExecutor()
        {

        }

        public virtual async Task Execute(JobCommand command)
        {
            await NofificarInicio(new ExecutorStartEventArgs()
            {
                FechaInicio = DateTime.Now
            });
            try
            {
                await EjecutarJob(command);

            }
            catch (Exception ex)
            {
                await NofificarError(new ExecutorFailEventArgs()
                {
                    Mensaje = ex.Message,
                    StackTrace = ex.StackTrace,
                    FechaFin = DateTime.Now
                });
                throw;
            }

            await NofificarFin(new ExecutorCompleteEventArgs { IdJob = command.IdJob, FechaFin = DateTime.Now });
        }

        public abstract Task EjecutarJob(JobCommand command);

        private async Task NofificarInicio(JobStarted jobStarted)
        {
            await (ProcessStarted?.Invoke(this, jobStarted) ?? Task.CompletedTask).ConfigureAwait(false);
        }

        protected async Task NofificarProgreso(JobTaskCompleted jobTaskCompleted)
        {
            await (StatusTarea?.Invoke(this, jobTaskCompleted) ?? Task.CompletedTask).ConfigureAwait(false);
        }

        private async Task NofificarError(JobFailed jobFailed)
        {
            await (ProcessFailed?.Invoke(this, jobFailed) ?? Task.CompletedTask).ConfigureAwait(false);
        }

        private async Task NofificarFin(JobCompleted jobCompleted)
        {
            await (ProcessCompleted?.Invoke(this, jobCompleted) ?? Task.CompletedTask).ConfigureAwait(false);
        }
    }
}
