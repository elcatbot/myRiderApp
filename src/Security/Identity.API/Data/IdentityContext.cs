
namespace myRideApp.Identity.Data;

public class IdentityContext : IdentityDbContext<AppUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
    
    public DbSet<AppUser>? User { get; set; }
    public DbSet<RefreshToken>? RefreshTokens { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<RefreshToken>(entity =>
    //     {
    //         entity.HasKey(e => e.Id);
    //         entity.Property(e => e.Token).IsRequired();
    //         entity.Property(e => e.ExpiresAt).IsRequired();
    //         entity.Property(e => e.IsRevoked).HasDefaultValue(false);
    //         entity.Property(e => e.CreatedAt).HasDefaultValue(new DateTime());
    //     });
    // }
}
