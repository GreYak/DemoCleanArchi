namespace Shop.Repository
{
    /// <summary>
    /// Ensure the <see cref="User"/> persistancy.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get the <see cref="User"/> identified by its Id.
        /// </summary>
        /// <param name="userId">The <see cref="User"/> identifier</param>
        /// <returns>The user if found, else null.</returns>
        Task<User?> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Create or update the <see cref="User"/>.
        /// </summary>
        /// <param name="user"><see cref="User"/></param>
        /// <returns><see cref="Task"/></returns>
        Task SaveAsync(User user);
    }
}
