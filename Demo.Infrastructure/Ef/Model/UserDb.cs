using Shop;
using Transport;

namespace Demo.Infrastructure.Ef.Model
{
    public class UserDb
    {
        public Guid Id { get; set; }
        public TicketBookDb? TicketBook { get; set; }
        public TicketDb? CurrentTicket { get; set; }

        internal Shop.User ToShopDomain()
        {
            return new Shop.User(Id, TicketBook?.ToShopDomain());
        }

        internal Transport.User ToTransportDomain()
        {
            return new Transport.User(
                Id, 
                TicketBook?.Tickets.Select(t => t.ToTransportDomain()) ?? new List<Transport.Ticket>(),
                CurrentTicket?.ToTransportDomain()
                );
        }
    }
}
