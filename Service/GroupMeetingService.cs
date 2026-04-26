using OOAD.Model;
using OOAD.Repository;

namespace OOAD.Service
{
    public class GroupMeetingService
    {
        private readonly GroupMeetingRepository _groupMeetingRepository;

        public GroupMeetingService(GroupMeetingRepository groupMeetingRepository)
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
