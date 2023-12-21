using Demo.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Transport;
using Transport.Repository;

namespace Demo.Infrastructure
{
    public class TransportUserRepository : IUserRepository
    {
        private readonly DemoDbContext _dbContext = new DemoDbContext();

        /// <inheritdoc/>
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            var userDb = await _dbContext.Users.SingleOrDefaultAsync(c => c.Id == userId);
            return userDb?.ToTransportDomain();
        }

        /// <inheritdoc/>
        public Task SaveAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
