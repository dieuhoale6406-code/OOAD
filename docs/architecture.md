/View
    Login.cs
    MainCalendar.cs
    Appointment.cs
    GroupMeetingSuggestion.cs
    ConflictResolution.cs

/Model
    Users.cs
    Calendars.cs
    Appointments.cs
    GroupMeetings.cs
    Reminders.cs
    UserGroupMeetings.cs

/Repository
    /Interfaces
        IUserRepository.cs
        ICalendarRepository.cs
        IAppointmentRepository.cs
        IGroupMeetingRepository.cs
        IReminderRepository.cs
    /Implementations
        UserRepository.cs
        CalendarRepository.cs
        AppointmentRepository.cs
        GroupMeetingRepository.cs
        ReminderRepository.cs

/Service
    /Interfaces
        IAuthService.cs
        ICalendarService.cs
        IAppointmentService.cs
        IGroupMeetingService.cs
        IConflictService.cs
        IReminderService.cs
    /Implementations
        AuthService.cs
        CalendarService.cs
        AppointmentService.cs
        GroupMeetingService.cs
        ConflictService.cs
        ReminderService.cs

/Data
    AppDbContext.cs
    DbInitializer.cs

/DTOs
    LoginRequestDto.cs
    AppointmentCreateDto.cs
    ConflictResolutionDto.cs