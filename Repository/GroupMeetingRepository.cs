using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class GroupMeetingRepository : BaseRepository<GroupMeetings>
    {
        public GroupMeetingRepository(AppDBContext context) : base(context) { }
    }
}