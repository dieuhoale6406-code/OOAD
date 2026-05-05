using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOAD.Model
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public Calendars Calendar { get; set; } = null!;

        public virtual ICollection<UserGroupMeetings> UserGroupMeetings { get; set; } = new List<UserGroupMeetings>();
    }
}