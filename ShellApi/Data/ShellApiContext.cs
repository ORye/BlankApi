using Microsoft.EntityFrameworkCore;

namespace ShellApi.Data
{
    public class ShellApiContext : DbContext
    {
        public ShellApiContext(DbContextOptions<ShellApiContext> options) : base(options)
        { }

        //TODO: add db entities
    }
}
