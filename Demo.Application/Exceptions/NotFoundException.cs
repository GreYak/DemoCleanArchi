namespace Demo.Application.Exceptions
{
    /// <summary>
    /// Thrown for a not found entity.
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, Guid key) : base($"Entity {name} with key {key} was not found") { }
    }
}
