using Microsoft.EntityFrameworkCore;
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
            return _context.Set<GroupMeetings>()
                .Include(g => g.UserGroupMeetings)
                .ThenInclude(ug => ug.User)
                .OrderBy(g => g.StartTime)
                .ToList();
        }

        public GroupMeetings? GetById(Guid groupMeetingId)
        {
            return _context.Set<GroupMeetings>()
                .Include(g => g.UserGroupMeetings)
                .ThenInclude(ug => ug.User)
                .FirstOrDefault(g => g.AppointmentId == groupMeetingId);
        }

        public GroupMeetings? FindByNameAndDuration(string name, TimeSpan duration)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var normalizedName = name.Trim().ToLower();

            return _context.Set<GroupMeetings>()
                .AsEnumerable()
                .FirstOrDefault(g =>
                    g.Name.Trim().ToLower() == normalizedName &&
                    (g.EndTime - g.StartTime) == duration);
        }

        public bool IsUserJoined(Guid userId, Guid groupMeetingId)
        {
            return _context.Set<UserGroupMeetings>()
                .Any(ug => ug.UserId == userId && ug.GroupMeetingId == groupMeetingId);
        }

        public void JoinGroupMeeting(Guid userId, Guid groupMeetingId)
        {
            if (IsUserJoined(userId, groupMeetingId))
                return;

            var userGroupMeeting = new UserGroupMeetings
            {
                UserId = userId,
                GroupMeetingId = groupMeetingId
            };

            _context.Set<UserGroupMeetings>().Add(userGroupMeeting);
            _context.SaveChanges();
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
                return;

            _context.Set<GroupMeetings>().Remove(meeting);
            _context.SaveChanges();
        }
    }
}