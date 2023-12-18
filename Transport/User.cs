namespace Transport
{
    public class User
    {
        public Guid Id { get; }
        private readonly Queue<Ticket> _tickets;

        public static User New() => new User(Guid.NewGuid(), Array.Empty<Ticket>());
        public User(Guid id, IEnumerable<Ticket> tickets)
        {
            Id = id;
            _tickets = new Queue<Ticket>(tickets);
        }

        public void BuyTickets(IEnumerable<Ticket> tickets)
        {
            foreach(var ticket in tickets)
            {
                _tickets.Enqueue(ticket);
            }
        }

        public Ticket? UseTicket() => _tickets.TryDequeue(out var ticket) ? ticket : null;
    }
}
