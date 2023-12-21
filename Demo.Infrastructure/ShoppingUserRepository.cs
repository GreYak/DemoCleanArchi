using Demo.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Repository;

namespace Demo.Infrastructure
{
    public class ShoppingUserRepository : IUserRepository
    {
        private readonly DemoDbContext _dbContext = new DemoDbContext();

        /// <inheritdoc/>
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            var userDb = await _dbContext.Users.SingleOrDefaultAsync(c => c.Id == userId);
            return userDb?.ToShopDomain();
        }

        /// <inheritdoc/>
        public Task SaveAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
