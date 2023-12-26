namespace Authorization.Exceptions
{
    public class RoleValidationException : ApplicationException
    {
        private readonly List<string> _errors;

        public RoleValidationException(Guid id, IEnumerable<string> validationErrors) : base($"{nameof(Role)} number '{id}' is invalid.")
        {
            _errors = validationErrors?.ToList() ?? new List<string>();
        }
    }
}
