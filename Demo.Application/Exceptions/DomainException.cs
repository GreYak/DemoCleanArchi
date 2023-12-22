using Shop.Exceptions;
using Transport.Exceptions;

namespace Demo.Application.Exceptions
{
    /// <summary>
    /// Represents a business exception.
    /// </summary>
    public class DomainException : ApplicationException
    {
        public int ErrorCode { get; }
        
        public DomainException(string usecase, Exception innerException): base($"An domain exception has occured while dealing the '{usecase}' usecase.", innerException)
        {
            ErrorCode = innerException switch
            {
                ExpiredTicketException => 1,
                NoMoreTicketForUserException => 2,
                IllegalBuyException => 3,
                TicketBookValidationException =>4,
                _ => 0
            };
        }
    }
}
