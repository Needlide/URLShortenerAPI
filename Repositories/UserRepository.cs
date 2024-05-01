using URLShortenerAPI.Abstract;
using URLShortenerAPI.Database;
using URLShortenerAPI.Models;

namespace URLShortenerAPI.Repositories
{
    public class UserRepository(UserContext context) : IRepository<User>
    {
        public void Add(User entity)
        {
            context.Users.Add(entity);
        }

        public void Delete(User entity)
        {
            context.Remove(entity);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Set<User>().ToList();
        }

        public User? GetById(int id)
        {
            return context.Set<User>().Find(id);
        }

        public void Update(User entity)
        {
            context.Update(entity);
        }
    }
}
