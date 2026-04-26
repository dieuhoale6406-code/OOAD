using OOAD.Data;
using OOAD.Model;
using OOAD.Repository.Interfaces;

namespace OOAD.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Set<Users>().ToList();
        }

        public Users? GetById(Guid userId)
        {
            return _context.Set<Users>().Find(userId);
        }

        public void Add(Users user)
        {
            _context.Set<Users>().Add(user);
            _context.SaveChanges();
        }

        public void Update(Users user)
        {
            _context.Set<Users>().Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid userId)
        {
            var user = GetById(userId);
            if (user == null)
            {
                return;
            }

            _context.Set<Users>().Remove(user);
            _context.SaveChanges();
        }
    }
}
