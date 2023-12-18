using Shop;
using Shop.Repository;

namespace Demo.Infrastructure
{
    public class ShoppingUserRepository : IUserRepository
    {
        public Task<User?> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
