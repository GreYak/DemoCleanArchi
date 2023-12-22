namespace Transport.Exceptions
{
    /// <summary>
    /// Thrown when a <see cref="Ticket"/> has expired
    /// </summary>
    public class ExpiredTicketException : ApplicationException
    {
        public ExpiredTicketException(Guid id) : base($"{nameof(Ticket)} number '{id}' has expired.")
        {
        }
    }
}
