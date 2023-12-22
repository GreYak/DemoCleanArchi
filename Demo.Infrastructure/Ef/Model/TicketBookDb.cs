using Shop;
using Transport;

namespace Demo.Infrastructure.Ef.Model
{
    public class TicketBookDb
    {
        public Guid Id { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public List<TicketDb> Tickets { get; set; } = new List<TicketDb>();

        internal void Feed(TicketBook ticketBook)
        {
            ArgumentNullException.ThrowIfNull(ticketBook);

            IssueDate = ticketBook.IssueDate;

            var ticketsIds = ticketBook.TicketIds.ToList();

            TicketDb ticket;
            for (int i=0; i < Tickets.Count; i++)
            {
                ticket = Tickets.ElementAt(i);
                if (ticketsIds.Contains(ticket.Id))
                    ticketsIds.Remove(ticket.Id);
                else
                    Tickets.Remove(ticket);
            }
        }

        internal TicketBook ToShopDomain()
        {
            return new TicketBook(Id, Tickets.Select(t => t.Id), IssueDate);
        }
    }
}
