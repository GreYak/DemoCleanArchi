namespace Demo.Infrastructure.Context
{
    public class ApplicationContext
    {
        public DateTimeOffset ExecutionDate { get; }

        public ApplicationContext()
        {
            ExecutionDate = DateTime.Now;
        }
    }
}
