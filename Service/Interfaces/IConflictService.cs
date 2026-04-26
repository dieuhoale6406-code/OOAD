using OOAD.DTOs;

namespace OOAD.Service.Interfaces
{
    public interface IConflictService
    {
        ConflictResolutionDto ResolveConflict(Guid appointmentId);
    }
}
