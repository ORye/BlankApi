using Microsoft.EntityFrameworkCore;

namespace BlankApi.Data
{
    public class BlankApiContext : DbContext
    {
        public BlankApiContext(DbContextOptions<BlankApiContext> options) : base(options)
        { }

        //TODO: add db entities
    }
}
