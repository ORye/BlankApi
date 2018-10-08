using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlankApi.Data
{
    public class BlankApiContext : DbContext
    {
        public BlankApiContext(DbContextOptions<BlankApiContext> options) : base(options)
        { }

        //TODO: add db entities
        DbSet<Thing> Thing { get; set; }
        DbSet<AMaBobber> AMaBobber { get; set; }
    }

    public class Thing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AMaBobber AMaBobber { get; set; }
    }

    public class AMaBobber
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
