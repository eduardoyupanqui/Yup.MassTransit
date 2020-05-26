using System;
using System.Collections.Generic;
using System.Text;

namespace Yup.MassTransit.Jobs.Events
{
    public interface JobStarted : JobEvent
    {
        public DateTime FechaInicio { get; }
    }
}
