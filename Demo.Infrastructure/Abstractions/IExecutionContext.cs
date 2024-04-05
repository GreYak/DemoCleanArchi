namespace Demo.Infrastructure.Abstractions
{
    /// <summary>
    /// The application execution context.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// The reference date 
        /// </summary>
        DateTimeOffset ReferenceDateTime { get; }

        /// <summary>
        /// Id of correlation
        /// </summary>
        Guid CorrelationId { get; }

        /// <summary>
        /// Id of the current user.
        /// </summary>
        Guid? UserID { get; }
    }
}
