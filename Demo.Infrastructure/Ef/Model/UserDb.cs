namespace Demo.Infrastructure.Ef.Model
{
    public class UserDb
    {
        public Guid Id { get; set; }
        public TicketBookDb? TicketBook { get; set; }
        public TicketDb? CurrentTicket { get; set; }

        internal void Feed(Transport.User user)
        {
            if (user.HasCurrentTicket)
            {
                if (CurrentTicket is null)
                {
                    CurrentTicket = new TicketDb() { Id = user.CurrentTicket!.Id };
                }
                CurrentTicket.Feed(user.CurrentTicket!);
            }
        }
        internal void Feed(Shop.User user)
        {
            if (user.TicketBook is not null)
            {
                if (TicketBook is null)
                {
                    TicketBook = new TicketBookDb() { Id = user.TicketBook.Id };
                }
                TicketBook.Feed(user.TicketBook);
            }
        }


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
