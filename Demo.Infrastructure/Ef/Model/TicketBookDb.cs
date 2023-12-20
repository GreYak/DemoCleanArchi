namespace Demo.Infrastructure.Ef.Model
{
    public class TicketBookDb
    {
        public Guid Id { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public ICollection<TicketDb> Tickets { get; set; } = new List<TicketDb>();
    }
}
