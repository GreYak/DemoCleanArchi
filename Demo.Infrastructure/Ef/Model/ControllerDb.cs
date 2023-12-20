using Transport;

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

        internal Controller ToDomain()
        {
            return new Controller(Id, Fraudsters.Select(u => u.Id), ControlledTickets.Where(t => t.ControlDate.HasValue).ToDictionary(t => t.Id, t => t.ControlDate!.Value));
        }
    }
}
