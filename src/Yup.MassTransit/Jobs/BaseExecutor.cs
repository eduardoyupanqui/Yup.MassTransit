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

        protected BaseExecutor()
        {

        }

        public virtual async Task Execute(JobCommand command)
        {
            await NofificarInicio();
            try
            {
                await EjecutarJob(command);

            }
            catch (Exception ex)
            {
                await NofificarError(ex.Message, ex.StackTrace);
                throw;
            }

            await NofificarFin();
        }

        public abstract Task EjecutarJob(JobCommand command);

        private async Task NofificarInicio()
        {
            await (ProcessStarted?.Invoke(this, new ExecutorStartEventArgs()
            {
                FechaInicio = DateTime.Now
            }) ?? Task.CompletedTask).ConfigureAwait(false);
        }

        protected async Task NofificarProgreso(int orden, string mensaje, string mensajeException = null, string outPutTarea = null)
        {
            await (StatusTarea?.Invoke(this, new ExecutorTaskEventArgs()
            {
                Orden = orden,
                Mensaje = mensaje,
                MensajeExcepcion = mensajeException,
                OutputTarea = outPutTarea,
                FechaInicio = DateTime.Now
            }) ?? Task.CompletedTask).ConfigureAwait(false);
        }

        private async Task NofificarError(string message, string stackTrace)
        {
            await (ProcessFailed?.Invoke(this, new ExecutorFailEventArgs()
            {
                Mensaje = message,
                StackTrace = stackTrace,
                FechaFin = DateTime.Now
            }) ?? Task.CompletedTask).ConfigureAwait(false);
        }

        private async Task NofificarFin()
        {
            await (ProcessCompleted?.Invoke(this, new ExecutorCompleteEventArgs()
            {
                FechaFin = DateTime.Now
            }) ?? Task.CompletedTask).ConfigureAwait(false);
        }
    }
}
