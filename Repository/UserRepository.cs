using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class UserRepository : BaseRepository<Users>
    {
        public UserRepository(AppDBContext context) : base(context) { }
    }
}