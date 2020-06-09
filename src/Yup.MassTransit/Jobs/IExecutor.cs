using System.Threading.Tasks;
using Yup.MassTransit.Jobs.Client.EventArgs;
using Yup.MassTransit.Jobs.Commands;

namespace Yup.MassTransit.Jobs
{
    public delegate Task AsyncEventHandler<in TEvent>(object sender, TEvent @event) where TEvent : System.EventArgs;
    public interface IExecutor
    {
        event AsyncEventHandler<ExecutorStartEventArgs> ProcessStarted;
        event AsyncEventHandler<ExecutorTaskEventArgs> StatusTarea;
        event AsyncEventHandler<ExecutorCompleteEventArgs> ProcessCompleted;
        event AsyncEventHandler<ExecutorFailEventArgs> ProcessFailed;
        Task<JobResult> Execute(JobCommand command);
    }
}
