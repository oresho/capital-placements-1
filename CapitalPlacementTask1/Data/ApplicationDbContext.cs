using CapitalPlacementTask1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask1.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<ProgramEntity> Programs { get; set; }
        public DbSet<ApplicationEntity> Applications { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            :base(options)
        {
             _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            
            var dbConfig = _config.GetSection("CosmoConfig");
            optionsBuilder.UseCosmos
                (
                    dbConfig["AccountEndpoint"], 
                    dbConfig["AccountKey"], 
                    dbConfig["DatabaseName"]
                );

        }
    }
}
