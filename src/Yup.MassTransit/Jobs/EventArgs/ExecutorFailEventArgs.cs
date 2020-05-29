using System;
using Yup.MassTransit.Jobs.Events;

namespace Yup.MassTransit.Jobs.Client.EventArgs
{
    public class ExecutorFailEventArgs : System.EventArgs, JobFailed
    {
        public Guid IdJob { get; set; }
        public string Mensaje { get; set; }
        public string StackTrace { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
