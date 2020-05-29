using System;
using Yup.MassTransit.Jobs.Events;

namespace Yup.MassTransit.Jobs.Client.EventArgs
{
    public class ExecutorTaskEventArgs : System.EventArgs, JobTaskCompleted
    {
        public Guid IdJob { get; set; }
        public int Orden { get; set; }
        public string Mensaje { get; set; }
        public string MensajeExcepcion { get; set; }
        public string OutputTarea { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

    }
}
