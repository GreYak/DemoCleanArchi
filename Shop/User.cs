using Shop.Exceptions;

namespace Shop
{
    public class User
    {
        public Guid Id { get; }
        public TicketBook? TicketBook { get; private set; }
        public User(Guid id)
        {
            Id = id;
        }
        public User(Guid id, TicketBook ticketBook) : this(id) 
        {
            TicketBook = ticketBook;
        }

        public void Buy(TicketBook newTicketBook)
        {
            ArgumentNullException.ThrowIfNull(newTicketBook);
            if (!CanBuyTicketBook)
                throw new IllegalBuyException(Id, TicketBook!.Id, newTicketBook.Id);
            TicketBook = newTicketBook;
        }

        public bool CanBuyTicketBook => TicketBook is null;
    }
}
