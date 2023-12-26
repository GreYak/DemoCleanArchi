using Demo.Infrastructure.Ef;
using Demo.Infrastructure.Ef.Model;
using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Repositories;

namespace Demo.Infrastructure
{
    public class ShoppingUserRepository : IUserRepository
    {
        private readonly DemoDbContext _dbContext;

        public ShoppingUserRepository(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            var userDb = await _dbContext.Users.AsNoTracking()
                .Include(u => u.TicketBook).ThenInclude(tb => tb.Tickets)
                .SingleOrDefaultAsync(c => c.Id == userId);
            return userDb?.ToShopDomain();
        }

        /// <inheritdoc/>
        public async Task SaveAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var userDb = await _dbContext.Users.SingleAsync(c => c.Id == user.Id);
            userDb.Feed(user);

            if (user.TicketBook is not null)
            {
                userDb.TicketBook = await _dbContext.TicketBooks.SingleAsync(t => t.Id == user.TicketBook.Id);
            }

            _dbContext.SaveChanges();
        }
    }
}
