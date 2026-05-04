using Microsoft.Identity.Client;
using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class AppointmentRepository : BaseRepository<Appointments>
    {
        public AppointmentRepository(AppDBContext context) : base(context) { }
    }
}
