namespace apiCore.Data.Base
{
    using apiCore.Data.Model;
    using Microsoft.EntityFrameworkCore;

    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> context) : base(context)
        {

        }


        public DbSet<Cliente>? Cliente { get; set; }

        public DbSet<Cuenta> Cuenta { get; set; }

        public DbSet<Movimientos> Movimientos { get; set; }



    }
}
