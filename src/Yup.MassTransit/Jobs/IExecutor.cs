using System.Threading.Tasks;
using Yup.MassTransit.Jobs.Client.EventArgs;
using Yup.MassTransit.Jobs.Commands;
using Yup.MassTransit.Jobs.Events;

namespace Yup.MassTransit.Jobs
{
    public delegate Task AsyncEventHandler<in TEvent>(object sender, TEvent @event) where TEvent : JobEvent;
    public interface IExecutor
    {
        event AsyncEventHandler<JobStarted> ProcessStarted;
        event AsyncEventHandler<JobTaskCompleted> StatusTarea;
        event AsyncEventHandler<JobCompleted> ProcessCompleted;
        event AsyncEventHandler<JobFailed> ProcessFailed;
        Task Execute(JobCommand command);
    }
}
