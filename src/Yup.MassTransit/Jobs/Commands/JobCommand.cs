using System;
using System.Collections.Generic;
using System.Text;

namespace Yup.MassTransit.Jobs.Commands
{
    public interface JobCommand
    {
        public Guid UsuarioRegistro { get; set; }

        public Guid IdJob { get; set; }
        public Guid IdTipoJob { get; set; }
        public Guid IdAplicacion { get; set; }

        public string RegistroAsociado { get; set; }
        public string InputJob { get; set; }
        public string HostName { get; set; }

        public string CodigoTipoJob { get; set; }
        public string NombreJob { get; set; }
    }
}
