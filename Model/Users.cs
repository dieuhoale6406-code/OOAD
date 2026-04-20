using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOAD.Model
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }
        public Calendars Calendar { get; set; } = null!;
        public List<UserGroupMeetings> UserGroupMeetings { get; set; } = new List<UserGroupMeetings>();
    }
}
