using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class AspNetIdentityContext : DbContext
    {
        public AspNetIdentityContext(DbContextOptions options) : base(options)
        {
        }
    }
}
