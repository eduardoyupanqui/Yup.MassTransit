using System;

namespace Yup.MassTransit.Jobs.Client.EventArgs
{
    public class ExecutorStartEventArgs : System.EventArgs
    {
        public DateTime FechaInicio { get; set; }
    }
}
