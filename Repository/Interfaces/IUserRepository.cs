using OOAD.Model;

namespace OOAD.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAll();
        Users? GetById(Guid userId);
        void Add(Users user);
        void Update(Users user);
        void Delete(Guid userId);
    }
}
