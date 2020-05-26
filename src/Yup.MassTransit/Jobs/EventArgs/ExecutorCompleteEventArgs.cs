using System;

namespace Yup.MassTransit.Jobs.Client.EventArgs
{
    public class ExecutorCompleteEventArgs : System.EventArgs
    {
        public DateTime FechaFin { get; set; }
    }
}
