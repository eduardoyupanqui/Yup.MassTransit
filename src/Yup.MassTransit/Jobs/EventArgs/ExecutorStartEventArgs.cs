using System;
using Yup.MassTransit.Jobs.Events;

namespace Yup.MassTransit.Jobs.Client.EventArgs
{
    public class ExecutorStartEventArgs : System.EventArgs, JobStarted
    {
        public Guid IdJob { get; set; }
        public DateTime FechaInicio { get; set; }
    }
}
