using Microsoft.EntityFrameworkCore;
using OOAD.Model;

namespace OOAD.Data
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Calendars> Calendars { get; set; }
        public DbSet<GroupMeetings> GroupMeetings { get; set; }
        public DbSet<Reminders> Reminders { get; set; }
        public DbSet<UserGroupMeetings> UserGroupMeetings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=OOADDb;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Calendar)
                .WithOne(c => c.User)
                .HasForeignKey<Calendars>(c => c.UserId);

            modelBuilder.Entity<Calendars>()
                .HasMany(c => c.Appointments)
                .WithOne(a => a.Calendar)
                .HasForeignKey(a => a.CalendarId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointments>()
                .HasMany(a => a.Reminders)
                .WithOne(r => r.Appointment)
                .HasForeignKey(r => r.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserGroupMeetings>()
                .HasKey(ug => new { ug.UserId, ug.GroupMeetingId });

            modelBuilder.Entity<UserGroupMeetings>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroupMeetings)
                .HasForeignKey(ug => ug.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserGroupMeetings>()
                .HasOne(ug => ug.GroupMeeting)
                .WithMany(g => g.UserGroupMeetings)
                .HasForeignKey(ug => ug.GroupMeetingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
