namespace Demo.Application.Exceptions
{
    /// <summary>
    /// Thrown for an entity already existing in the system.
    /// </summary>
    internal class AlreadyExistException : ApplicationException
    {
        public AlreadyExistException(string entity, Guid key) : base($"Entity {entity} with key {key} alreay exists.") { }
    }
}
