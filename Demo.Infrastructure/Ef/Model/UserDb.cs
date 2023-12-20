namespace Demo.Infrastructure.Ef.Model
{
    public class UserDb
    {
        public Guid Id { get; set; }
        public TicketBookDb? TicketBook { get; set; }
    }
}
