using System;
using Yup.MassTransit.Jobs.Events;

namespace Yup.MassTransit.Jobs.Client.EventArgs
{
    public class ExecutorCompleteEventArgs : System.EventArgs, JobCompleted
    {
        public Guid IdJob { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
