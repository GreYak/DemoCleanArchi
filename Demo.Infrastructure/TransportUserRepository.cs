using Demo.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Transport;
using Transport.Repositories;

namespace Demo.Infrastructure
{
    public class TransportUserRepository : IUserRepository
    {
        private readonly DemoDbContext _dbContext;

        public TransportUserRepository(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            var userDb = await _dbContext.Users.AsNoTracking()                
                .Include(u => u.TicketBook).ThenInclude(tb => tb.Tickets)
                .Include(u => u.CurrentTicket)
                .SingleOrDefaultAsync(c => c.Id == userId);
            return userDb?.ToTransportDomain();
        }

        /// <inheritdoc/>
        public async Task SaveAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var userDb = await _dbContext.Users
                .Include(u => u.TicketBook).ThenInclude(tb => tb.Tickets)
                .Include(u => u.CurrentTicket)
                .SingleAsync(c => c.Id == user.Id);


            //userDb.Feed(user);

            _dbContext.SaveChanges();
        }
    }
}
