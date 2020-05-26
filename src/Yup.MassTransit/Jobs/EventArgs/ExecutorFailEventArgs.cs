namespace Yup.MassTransit.Jobs.Client.EventArgs
{
    public class ExecutorFailEventArgs : System.EventArgs
    {
        public string Mensaje { get; set; }
        public string StackTrace { get; set; }
    }
}
