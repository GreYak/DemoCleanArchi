namespace Transport.Repository
{
    /// <summary>
    /// Manage the <see cref="User"/> persistancy.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get the <see cref="User"/> identified by its Id.
        /// </summary>
        /// <param name="userId">The <see cref="User"/> identifier</param>
        /// <returns>The <see cref="User"/> if found, else null.</returns>
        Task<User?> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Create or update an <see cref="User"/>
        /// </summary>
        /// <param name="user">The <see cref="User"/> to save</param>
        /// <returns><see cref="Task"/></returns>
        Task SaveAsync(User user);
    }
}
