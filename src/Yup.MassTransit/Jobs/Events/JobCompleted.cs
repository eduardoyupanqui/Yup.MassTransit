using System;
using System.Collections.Generic;
using System.Text;

namespace Yup.MassTransit.Jobs.Events
{
    public interface JobCompleted: JobEvent
    {
        public DateTime FechaFin { get; }
    }
}
