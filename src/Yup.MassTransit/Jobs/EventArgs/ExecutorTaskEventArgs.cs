namespace Yup.MassTransit.Jobs.Client.EventArgs
{
    public class ExecutorTaskEventArgs : System.EventArgs
    {
        public int Orden { get; set; }
        public string Mensaje { get; set; }
    }
}
