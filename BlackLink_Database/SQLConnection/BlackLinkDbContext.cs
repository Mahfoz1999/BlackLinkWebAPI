using BlackLink_Models.Models;
using BlackLink_Models.Models.Files;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Database.SQLConnection;

public class BlackLinkDbContext : IdentityDbContext<User>
{
    #region Files Entity
    public DbSet<UserPhoto> UserPhotos { get; set; }
    #endregion
    public DbSet<Interest> Interests { get; set; }
    public DbSet<InterestUser> InterestUsers { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogComment> BlogComments { get; set; }
    public DbSet<Story> Stories { get; set; }

    public BlackLinkDbContext(DbContextOptions<BlackLinkDbContext> options) : base(options)
    { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(user => user.NickName).IsUnique();
        modelBuilder.Entity<User>().HasMany(user => user.Blogs).WithOne(blog => blog.User);
        base.OnModelCreating(modelBuilder);
    }
}
