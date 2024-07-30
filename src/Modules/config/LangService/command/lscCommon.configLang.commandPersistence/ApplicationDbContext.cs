using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace lscCommon.configLang.commandPersistence
{
    /// <summary>
    /// Application database context 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Application database context 
        /// </summary>
        /// <param name="options">Options for database context</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
