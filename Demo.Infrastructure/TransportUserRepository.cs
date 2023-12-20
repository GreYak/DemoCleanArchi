using Transport;
using Transport.Repository;

namespace Demo.Infrastructure
{
    public class TransportUserRepository : IUserRepository
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
