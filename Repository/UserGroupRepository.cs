using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class UserGroupRepository : BaseRepository<UserGroupMeetings>
    {
        public UserGroupRepository(AppDBContext context) : base(context) { }
        public bool IsGroupMeetingEmpty(Guid groupMeetingId)
        {
            return !Query.Any(ug => ug.GroupMeetingId == groupMeetingId);
        }
        public bool Remove(Guid userid, Guid groupMeetingId)
        {
            var entity = Query.FirstOrDefault(ug => ug.UserId == userid && ug.GroupMeetingId == groupMeetingId);
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            return true;
        }
    }
}
