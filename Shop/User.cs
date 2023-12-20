using Shop.Exceptions;

namespace Shop
{
    /// <summary>
    /// Represents an user buying transport ticker.
    /// </summary>
    public class User
    {
        public Guid Id { get; }
        public TicketBook? TicketBook { get; private set; }

        /// <summary>
        /// New instance of <see cref="User"/>
        /// </summary>
        /// <param name="id">The user identifier</param>
        /// <param name="ticketBook">The owned ticketBook</param>
        public User(Guid id, TicketBook? ticketBook=null)
        {
            Id = id;
            TicketBook = ticketBook;
        }

        /// <summary>
        /// Buy a ticket-book
        /// </summary>
        /// <param name="newTicketBook">The ticketbook to buy</param>
        /// <exception cref="IllegalBuyException"></exception>
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
