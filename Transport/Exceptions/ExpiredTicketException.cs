namespace Transport.Exceptions
{
    public class ExpiredTicketException : ApplicationException
    {
        public ExpiredTicketException(Guid id) : base($"{nameof(Ticket)} number '{id}' has expired.")
        {
        }
    }
}
