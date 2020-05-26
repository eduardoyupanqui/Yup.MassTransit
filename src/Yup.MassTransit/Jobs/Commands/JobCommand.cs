using System;
using System.Collections.Generic;
using System.Text;

namespace Yup.MassTransit.Jobs.Commands
{
    public interface JobCommand
    {
        public Guid JobId { get; }
        public string CodigoJob { get; }
        public string JobInput { get; }
    }
}
