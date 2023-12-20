namespace Demo.Infrastructure.Ef.Model
{
    public class TicketDb
    {
        public Guid Id { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset? CompostDate { get; set; }
        public DateTimeOffset? EndOfValidityDate { get; set; }
        public DateTimeOffset? ControlDate { get; set; }
    }
}
