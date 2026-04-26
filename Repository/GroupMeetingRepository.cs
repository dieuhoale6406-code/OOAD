using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class GroupMeetingRepository
    {
        private readonly AppDBContext _context;

        public GroupMeetingRepository(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<GroupMeetings> GetAll()
        {
            return _context.Set<GroupMeetings>().ToList();
        }

        public GroupMeetings? GetById(Guid groupMeetingId)
        {
            return _context.Set<GroupMeetings>().Find(groupMeetingId);
        }

        public void Add(GroupMeetings groupMeeting)
        {
            _context.Set<GroupMeetings>().Add(groupMeeting);
            _context.SaveChanges();
        }

        public void Update(GroupMeetings groupMeeting)
        {
            _context.Set<GroupMeetings>().Update(groupMeeting);
            _context.SaveChanges();
        }

        public void Delete(Guid groupMeetingId)
        {
            var meeting = GetById(groupMeetingId);
            if (meeting == null)
            {
                return;
            }

            _context.Set<GroupMeetings>().Remove(meeting);
            _context.SaveChanges();
        }
    }
}
