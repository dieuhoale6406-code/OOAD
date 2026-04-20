using System;
using System.Collections.Generic;

namespace OOAD.Model
{
    public class GroupMeetings : Appointments
    {
        public List<UserGroupMeetings> UserGroupMeetings { get; set; } = new List<UserGroupMeetings>();
    }
}
