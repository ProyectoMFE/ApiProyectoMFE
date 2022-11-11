using Microsoft.EntityFrameworkCore;

namespace ProyectoMFE.DataAccess
{
    public class ProyectoMFEContext : DbContext
    {
        public ProyectoMFEContext(DbContextOptions<ProyectoMFEContext> opcions) : base(opcions)
        {

        }
    }
}
