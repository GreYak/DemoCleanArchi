namespace Demo.Infrastructure.Ef.Model
{
    public class ControllerDb
    {
        public Guid Id { get; set; }
        public IEnumerable<UserDb> Fraudsters { get; set; } = new List<UserDb>();
        public IEnumerable<TicketDb> ControlledTickets { get; set; } = new List<TicketDb>();

        public Task DoTrucAsync()
        {
            return Task.CompletedTask;
        }
    }
}
