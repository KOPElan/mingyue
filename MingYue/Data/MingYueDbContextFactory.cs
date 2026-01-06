using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MingYue.Data
{
    public class MingYueDbContextFactory : IDesignTimeDbContextFactory<MingYueDbContext>
    {
        public MingYueDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MingYueDbContext>();
            optionsBuilder.UseSqlite("Data Source=mingyue.db");

            return new MingYueDbContext(optionsBuilder.Options);
        }
    }
}
