using OOAD.Model;
using OOAD.Repository.Interfaces;
using OOAD.Service.Interfaces;

namespace OOAD.Service.Implementations
{
    public class GroupMeetingService : IGroupMeetingService
    {
        private readonly IGroupMeetingRepository _groupMeetingRepository;

        public GroupMeetingService(IGroupMeetingRepository groupMeetingRepository)
        {
            _groupMeetingRepository = groupMeetingRepository;
        }

        public IEnumerable<GroupMeetings> GetGroupMeetings()
        {
            return _groupMeetingRepository.GetAll();
        }

        public void CreateGroupMeeting(GroupMeetings groupMeeting)
        {
            if (groupMeeting.AppointmentId == Guid.Empty)
            {
                groupMeeting.AppointmentId = Guid.NewGuid();
            }

            _groupMeetingRepository.Add(groupMeeting);
        }
    }
}
