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

        public GroupMeetings? GetById(Guid groupMeetingId)
        {
            return _groupMeetingRepository.GetById(groupMeetingId);
        }

        public GroupMeetings? FindMatchingGroupMeeting(string appointmentName, DateTime startTime, DateTime endTime)
        {
            if (string.IsNullOrWhiteSpace(appointmentName))
                return null;

            if (endTime <= startTime)
                return null;

            var duration = endTime - startTime;

            return _groupMeetingRepository.FindByNameAndDuration(appointmentName, duration);
        }

        public void CreateGroupMeeting(GroupMeetings groupMeeting)
        {
            if (groupMeeting.AppointmentId == Guid.Empty)
            {
                groupMeeting.AppointmentId = Guid.NewGuid();
            }

            if (string.IsNullOrWhiteSpace(groupMeeting.Name))
                throw new ArgumentException("Tên cuộc họp nhóm không được để trống.");

            if (groupMeeting.EndTime <= groupMeeting.StartTime)
                throw new ArgumentException("Thời gian kết thúc phải lớn hơn thời gian bắt đầu.");

            _groupMeetingRepository.Add(groupMeeting);
        }

        public void JoinGroupMeeting(Guid userId, Guid groupMeetingId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId không hợp lệ.");

            if (groupMeetingId == Guid.Empty)
                throw new ArgumentException("GroupMeetingId không hợp lệ.");

            var groupMeeting = _groupMeetingRepository.GetById(groupMeetingId);

            if (groupMeeting == null)
                throw new Exception("Không tìm thấy cuộc họp nhóm.");

            _groupMeetingRepository.JoinGroupMeeting(userId, groupMeetingId);
        }

        public void UpdateGroupMeeting(GroupMeetings groupMeeting)
        {
            _groupMeetingRepository.Update(groupMeeting);
        }

        public void DeleteGroupMeeting(Guid groupMeetingId)
        {
            _groupMeetingRepository.Delete(groupMeetingId);
        }
    }
}