using Shop;

namespace Demo.Application.Model.Commands
{
    public record CreateTicketBookDto(Guid TicketBookId, IEnumerable<Guid> Tickets)
    {
        internal bool IsValid()
        {
            return TicketBookId != Guid.Empty
                && Tickets != null;
        }

        internal TicketBook ToDomain(DateTimeOffset issueDate)
        {
            return TicketBook.New(TicketBookId, Tickets.Select(t => new Ticket(t)), issueDate);
        }
    }
}
