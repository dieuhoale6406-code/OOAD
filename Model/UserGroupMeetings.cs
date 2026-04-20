using System;

namespace OOAD.Model
{
    public class UserGroupMeetings
    {
        public Guid UserId { get; set; }
        public Guid GroupMeetingId { get; set; }
        public Users User { get; set; } = null!;
        public GroupMeetings GroupMeeting { get; set; } = null!;
    }
}
