using Demo.Application.Exceptions;

namespace Demo.Application.Abstraction
{
    /// <summary>
    /// Manage the transport use-cases.
    /// </summary>
    public interface ITransportApplicationService
    {
        /// <summary>
        /// Manage the using transport by user.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="dateOfTravel">The date of travel.</param>
        /// <returns><see cref="Task"/></returns>
        /// <exception cref="NotFoundException">When user doesn't exist.</exception>
        /// <exception cref="DomainException"></exception> 
        Task UserTakesTransportAsync(Guid userId, DateTimeOffset dateOfTravel);

        /// <summary>
        /// Manage the using transport by user.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="dateOfControl">The date of control.</param>
        /// <returns><see cref="Task"/></returns>
        /// <exception cref="NotFoundException">When controller or user doesn't exist.</exception>
        /// <exception cref="DomainException"></exception> 
        Task ControlUserInTransportAsync(Guid controllerId, Guid userId, DateTimeOffset dateOfControl);
    }
}
