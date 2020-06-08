using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Yup.MassTransit.Jobs.Commands
{
    public interface JobCommand
    {
        public Guid IdJob { get; set; }
        public string InputJob { get; set; }
    }
}
