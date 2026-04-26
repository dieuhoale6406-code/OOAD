using OOAD.Model;

namespace OOAD.Repository.Interfaces
{
    public interface IGroupMeetingRepository
    {
        IEnumerable<GroupMeetings> GetAll();
        GroupMeetings? GetById(Guid groupMeetingId);
        void Add(GroupMeetings groupMeeting);
        void Update(GroupMeetings groupMeeting);
        void Delete(Guid groupMeetingId);
    }
}
