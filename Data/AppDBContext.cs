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
        public DbSet<Appointments> Appointments { get; set; }
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
                .HasIndex(u => u.Email)
                .IsUnique();

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

            SeedDefaultUsers(modelBuilder);
        }

        private static void SeedDefaultUsers(ModelBuilder modelBuilder)
        {
            var user1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var user2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var user3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var user4Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var user5Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            var calendar1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var calendar2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var calendar3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var calendar4Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            var calendar5Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            // Password mặc định: 123456
            var defaultPasswordHash =
                "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92";

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    UserId = user1Id,
                    FullName = "Nguyễn Đỗ Khánh Linh",
                    Email = "nnguyenlinh33662222@gmail.com",
                    PhoneNumber = "0900000001",
                    PasswordHash = defaultPasswordHash
                },
                new Users
                {
                    UserId = user2Id,
                    FullName = "Nguyễn Thị Cẩm Tuyền",
                    Email = "ntctuyen06@gmail.com",
                    PhoneNumber = "0900000002",
                    PasswordHash = defaultPasswordHash
                },
                new Users
                {
                    UserId = user3Id,
                    FullName = "Lê Thị Diệu Hòa",
                    Email = "dieuhoale6406@gmail.com",
                    PhoneNumber = "0900000003",
                    PasswordHash = defaultPasswordHash
                },
                new Users
                {
                    UserId = user4Id,
                    FullName = "Văn Ngọc Như Ý",
                    Email = "vanngocnhuy30032006@gmail.com",
                    PhoneNumber = "0900000004",
                    PasswordHash = defaultPasswordHash
                },
                new Users
                {
                    UserId = user5Id,
                    FullName = "Nguyễn Văn An",
                    Email = "nguyenvanan@gmail.com",
                    PhoneNumber = "0900000005",
                    PasswordHash = defaultPasswordHash
                }
            );

            modelBuilder.Entity<Calendars>().HasData(
                new Calendars
                {
                    CalendarId = calendar1Id,
                    UserId = user1Id
                },
                new Calendars
                {
                    CalendarId = calendar2Id,
                    UserId = user2Id
                },
                new Calendars
                {
                    CalendarId = calendar3Id,
                    UserId = user3Id
                },
                new Calendars
                {
                    CalendarId = calendar4Id,
                    UserId = user4Id
                },
                new Calendars
                {
                    CalendarId = calendar5Id,
                    UserId = user5Id
                }
            );
        }
    }
}