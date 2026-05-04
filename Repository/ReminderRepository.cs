using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class ReminderRepository : BaseRepository<Reminders>
    {
        public ReminderRepository(AppDBContext context) : base(context) { }
        public List<Reminders> GetRemindersByAppointmentId(Guid appointmentId)
        {
            return Query.Where(r => r.AppointmentId == appointmentId).ToList();
        }
        public List<Reminders> GetRemindersByGroupMeetingId(Guid userId, Guid groupMeetingId)
        {
            return Query.Where(r => r.UserId == userId && r.GroupMeetingId == groupMeetingId).ToList();
        }
    }
}