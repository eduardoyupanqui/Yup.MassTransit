using System;
using System.Collections.Generic;
using System.Text;

namespace Yup.MassTransit.Jobs.Events
{
    public interface JobEvent
    {
        public Guid JobId { get; }
    }
}
