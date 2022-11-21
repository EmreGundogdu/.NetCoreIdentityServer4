using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class AspNetIdentityContext : IdentityDbContext
    {
        public AspNetIdentityContext(DbContextOptions<AspNetIdentityContext> options) : base(options)
        {
        }
    }
}
