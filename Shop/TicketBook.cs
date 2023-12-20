using Shop.Exceptions;

namespace Shop
{
    /// <summary>
    /// Represents a ticket book.
    /// </summary>
    public class TicketBook
    {
        public Guid Id { get; }
        public DateTimeOffset IssueDate { get; }
        private readonly Queue<Ticket> _tickets;

        /// <summary>
        /// New instance of <see cref="TicketBook"/>
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="tickets">The <see cref="Ticket"/>s composing the book</param>
        /// <param name="issueDate">The creation date</param>
        public TicketBook(Guid id, IEnumerable<Ticket> tickets, DateTimeOffset issueDate)
        {
            Id = id;
            _tickets = new Queue<Ticket>(tickets ?? Array.Empty<Ticket>());
            IssueDate = issueDate;
        }

        /// <summary>
        /// Initialize and validate a new instance of <see cref="TicketBook"/>
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="tickets">The <see cref="Ticket"/>s composing the book</param>
        /// <param name="issueDate">The creation date</param>
        public static TicketBook New(Guid id, IEnumerable<Ticket> tickets, DateTimeOffset issueDate)
        {
            var ticketBook = new TicketBook(id, tickets, issueDate);
            ticketBook.Validate();

            return ticketBook;
        }

        /// <summary>
        /// Get the list of tickets composing the book
        /// </summary>
        /// <exception cref="TicketBookValidationException"></exception>
        public IReadOnlyList<Ticket> Tickets => _tickets.ToList().AsReadOnly();

        private void Validate()
        {
            var errors = new List<string>();

            if (_tickets.Count != 10) errors.Add("10 tickets are required.");
            if (_tickets.DistinctBy<Ticket, Guid>(t=> t.Id).Count() != 10) errors.Add("Tickets are unique.");
            
            if (errors.Any())
                throw new TicketBookValidationException(Id, errors);
        }
    }
}
