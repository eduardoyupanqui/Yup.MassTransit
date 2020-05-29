using System;
using System.Collections.Generic;
using System.Text;

namespace Yup.MassTransit.Jobs.Events
{
    public interface JobFailed : JobEvent
    {
        public string Mensaje { get; }
        public string StackTrace { get; }
        public DateTime FechaFin { get; }
    }
}
