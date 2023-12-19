using Shop;

namespace Demo.Application.Dtos.Commands
{
    /// <summary>
    /// A command for creation of a <see cref="TicketBook"/>.
    /// </summary>
    /// <param name="TicketBookId">Identifier of the ticketBook</param>
    /// <param name="Tickets">The tickets in the ticketbook</param>
    public record CreateTicketBookCommand(Guid TicketBookId, IEnumerable<Guid> Tickets)
    {
        /// <summary>
        /// Check the validity of the command.
        /// </summary>
        /// <returns>True id valid, else false.</returns>
        internal bool IsValid()
        {
            return TicketBookId != Guid.Empty
                && Tickets != null;
        }

        /// <summary>
        /// Generate the <see cref="TicketBook"/>.
        /// </summary>
        /// <param name="issueDate">The date of creation.</param>
        /// <returns><see cref="TicketBook"/></returns>
        internal TicketBook ToDomain(DateTimeOffset issueDate)
        {
            return TicketBook.New(TicketBookId, Tickets.Select(t => new Ticket(t)), issueDate);
        }
    }
}
