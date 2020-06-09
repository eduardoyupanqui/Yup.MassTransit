using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yup.MassTransit.Jobs.Client.EventArgs;
using Yup.MassTransit.Jobs.Commands;

namespace Yup.MassTransit.Jobs
{
    public abstract class BaseExecutor : IExecutor
    {
        public event AsyncEventHandler<ExecutorStartEventArgs> ProcessStarted;
        public event AsyncEventHandler<ExecutorTaskEventArgs> StatusTarea;
        public event AsyncEventHandler<ExecutorCompleteEventArgs> ProcessCompleted;
        public event AsyncEventHandler<ExecutorFailEventArgs> ProcessFailed;
        private Guid IdJob;
        protected BaseExecutor()
        {

        }

        public virtual async Task<JobResult> Execute(JobCommand command)
        {
            IdJob = command.IdJob;
            await NofificarInicio();
            try
            {
                var result = await EjecutarJob(command);
                await NofificarFin(result);
                return result;
            }
            catch (Exception ex)
            {
                await NofificarError(ex.Message, ex.StackTrace);
                throw;
            }
        }

        public abstract Task<JobResult> EjecutarJob(JobCommand command);

        private Task NofificarInicio()
        {
            return (ProcessStarted?.Invoke(this, new ExecutorStartEventArgs()
            {
                IdJob = this.IdJob,
                FechaInicio = DateTime.Now
            }) ?? Task.CompletedTask);//.ConfigureAwait(false);
        }

        protected Task NofificarProgreso(int orden, string mensaje, string mensajeException = null, string outPutTarea = null)
        {
            return (StatusTarea?.Invoke(this, new ExecutorTaskEventArgs()
            {
                IdJob = this.IdJob,
                Orden = orden,
                Mensaje = mensaje,
                MensajeExcepcion = mensajeException,
                OutputTarea = outPutTarea,
                FechaInicio = DateTime.Now
            }) ?? Task.CompletedTask);//.ConfigureAwait(false);
        }

        private Task NofificarError(string message, string stackTrace)
        {
            return (ProcessFailed?.Invoke(this, new ExecutorFailEventArgs()
            {
                IdJob = this.IdJob,
                Mensaje = message,
                StackTrace = stackTrace,
                FechaFin = DateTime.Now
            }) ?? Task.CompletedTask);//.ConfigureAwait(false);
        }

        private Task NofificarFin(JobResult result)
        {
            return (ProcessCompleted?.Invoke(this, new ExecutorCompleteEventArgs()
            {
                IdJob = this.IdJob,
                OutPutJob = result.OutputJob,
                FechaFin = DateTime.Now
            }) ?? Task.CompletedTask);//.ConfigureAwait(false);
        }
    }
}
