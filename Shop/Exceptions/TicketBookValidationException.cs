namespace Shop.Exceptions
{
    /// <summary>
    /// Thrown when the <see cref="TicketBook"/> isn't valid.
    /// </summary>
    public class TicketBookValidationException : ApplicationException
    {
        private readonly List<string> _errors;

        public TicketBookValidationException(Guid id, IEnumerable<string> validationErrors) : base($"{nameof(TicketBook)} number '{id}' is invalid.")
        {
            _errors = validationErrors?.ToList() ?? new List<string>();
        }
    }
}
