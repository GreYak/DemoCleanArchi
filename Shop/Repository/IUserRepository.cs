namespace Shop.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task SaveAsync(User user);
    }
}
