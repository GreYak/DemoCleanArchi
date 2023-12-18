using Shop.Exceptions;

namespace Shop
{
    public class TicketBook
    {
        public Guid Id { get; }
        public DateTimeOffset IssueDate { get; }
        private readonly Queue<Ticket> _tickets;

        public TicketBook(Guid id, IEnumerable<Ticket> tickets, DateTimeOffset issueDate)
        {
            Id = id;
            _tickets = new Queue<Ticket>(tickets ?? Array.Empty<Ticket>());
            IssueDate = issueDate;
        }

        public static TicketBook New(Guid id, IEnumerable<Ticket> tickets, DateTimeOffset issueDate)
        {
            var ticketBook = new TicketBook(id, tickets, issueDate);
            ticketBook.Validate();

            return ticketBook;
        }

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
