using OOAD.Model;

namespace OOAD.Service.Interfaces
{
    public interface IGroupMeetingService
    {
        IEnumerable<GroupMeetings> GetGroupMeetings();
        void CreateGroupMeeting(GroupMeetings groupMeeting);
    }
}
