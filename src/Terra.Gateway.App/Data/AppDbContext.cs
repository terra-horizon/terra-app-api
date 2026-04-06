using Microsoft.EntityFrameworkCore;

namespace Terra.Gateway.App.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<UserSettings> UserSettings { get; set; }
		public DbSet<VersionInfo> VersionInfos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
			new UserSettingsEntityConfiguration().Configure(modelBuilder.Entity<UserSettings>());
			new VersionInfoEntityConfiguration().Configure(modelBuilder.Entity<VersionInfo>());
		}
	}
}
