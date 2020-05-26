using System;
using System.Collections.Generic;
using System.Text;

namespace Yup.MassTransit.Jobs.Events
{
    public interface JobTaskCompleted: JobEvent
    {
        public int Orden { get; }
        public string Mensaje { get; }
        public DateTime FechaEjecucion { get; }
    }
}
