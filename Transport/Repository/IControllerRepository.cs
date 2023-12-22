namespace Transport.Repository
{
    /// <summary>
    /// Manage the <see cref="Controller"/> persistancy.
    /// </summary>
    public interface IControllerRepository
    {
        /// <summary>
        /// Get a controller by Id.
        /// </summary>
        /// <param name="controllerId">The controller identifier</param>
        /// <returns>The <see cref="Controller"/>if exists, else null</returns>
        Task<Controller?> GetControllerByIdAsync(Guid controllerId);

        /// <summary>
        /// Create or update a <see cref="Controller"/>
        /// </summary>
        /// <param name="controller">The <see cref="Controller"/> to save</param>
        /// <returns><see cref="Task"/></returns>
        Task SaveAsync(Controller controller);
    }
}
