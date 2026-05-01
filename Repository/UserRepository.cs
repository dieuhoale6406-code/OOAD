using Microsoft.EntityFrameworkCore;
using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class UserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Set<Users>()
                .Include(u => u.Calendar)
                .ToList();
        }

        public Users? GetById(Guid userId)
        {
            return _context.Set<Users>()
                .Include(u => u.Calendar)
                .FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Tìm user theo email, không phân biệt hoa thường.
        /// </summary>
        public Users? GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var normalizedEmail = email.Trim().ToLower();

            return _context.Set<Users>()
                .Include(u => u.Calendar)
                .FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);
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
                return;

            _context.Set<Users>().Remove(user);
            _context.SaveChanges();
        }
    }
}