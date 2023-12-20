namespace Transport.Exceptions
{
    /// <summary>
    /// Thrown when an <see cref="User"/> spends all its <see cref="Ticket"/>s
    /// </summary>
    public class NoMoreTicketForUserException : ApplicationException
    {
        public NoMoreTicketForUserException(Guid id) : base($"No more {nameof(Ticket)} for {nameof(User)} '{id}'.")
        {
        }
    }
}
