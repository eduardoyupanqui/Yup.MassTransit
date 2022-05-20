using MassTransit.Topology;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yup.MassTransit.Topology.EntityNameFormatters
{
    public class SimpleNameFormatter :
        IEntityNameFormatter
    {
        readonly IEntityNameFormatter _original;
        public SimpleNameFormatter(IEntityNameFormatter original)
        {
            _original = original;
        }

        public string FormatEntityName<T>()
        {
            return GetMessageName(typeof(T));
        }

        string GetMessageName(Type type)
        {
            if (type.IsGenericType)
                return type.GetGenericArguments()[0].Name;

            var messageName = type.Name;

            return messageName;
        }
    }
}
