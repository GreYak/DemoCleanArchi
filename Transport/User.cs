using Transport.Exceptions;

namespace Transport
{
    /// <summary>
    /// Represents a user of transport
    /// </summary>
    public class User
    {
        public Guid Id { get; }
        private readonly Queue<Ticket> _tickets;
        public Ticket? CurrentTicket { get; private set; }

        /// <summary>
        /// Initialize a new instance of <see cref="User"/>
        /// </summary>
        /// <param name="id">he user identifier</param>
        /// <param name="tickets">The list of tickets owned by user</param>
        /// <param name="currentTicket">The ticket in use</param>
        public User(Guid id, IEnumerable<Ticket> tickets, Ticket? currentTicket=null)
        {
            Id = id;
            _tickets = new Queue<Ticket>(tickets?? Array.Empty<Ticket>());
            CurrentTicket = currentTicket;
        }
        public User(Guid id) : this(id, null, null) { }

        /// <summary>
        /// User buy tickets
        /// </summary>
        /// <param name="tickets">The tickets bought</param>
        public void BuyTickets(IEnumerable<Ticket> tickets)
        {
            if (tickets != null)
            {
                foreach (var ticket in tickets)
                {
                    _tickets.Enqueue(ticket);
                }
            }
        }

        /// <summary>
        /// To use a <see cref="Ticket"/> among the owned ones.
        /// </summary>
        /// <returns>The used<see cref="Ticket"/></returns>
        /// <exception cref="NoMoreTicketForUserException"></exception>
        public Ticket UseTicket()
        {
            Ticket ticket;
            if (!_tickets.TryDequeue(out ticket!))
                throw new NoMoreTicketForUserException(Id);
            CurrentTicket = ticket;
            return ticket;
        }

        /// <summary>
        /// The <see cref="Ticket"/> in use.
        /// </summary>
        public bool HasTicket => CurrentTicket != null;

        /// <summary>
        /// The <see cref="Ticket"/>s owned.
        /// </summary>
        public IReadOnlyCollection<Ticket> Tickets => _tickets;
    }
}
