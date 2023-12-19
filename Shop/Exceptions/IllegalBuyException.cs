namespace Shop.Exceptions
{
    /// <summary>
    /// Thrown when an <see cref="User"/> try toi possess several <see cref="TicketBook"/>
    /// </summary>
    public class IllegalBuyException : ApplicationException
    {
        public IllegalBuyException(Guid buyerId, Guid gotTicketBookId, Guid newTicketBookId) 
            : base($"{nameof(User)} '{buyerId}' already possesses the '{gotTicketBookId}' {nameof(TicketBook)} and can't buy a new one ({newTicketBookId}).")
        {
        }
    }
}
