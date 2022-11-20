﻿using BlackLink_Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Database.SQLConnection
{
    public class BlackLinkDbContext : IdentityDbContext<User>
    {
        public DbSet<Interest> Interests { get; set; }
        public DbSet<InterestUser> InterestUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }
        public DbSet<StoryView> StoryViews { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Groub> Groubs { get; set; }
        public DbSet<GroubBlog> GroubBlogs { get; set; }
        public DbSet<GroubUser> GroubUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryEntityRealted> CategoryEntityRealteds { get; set; }

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
            modelBuilder.Entity<User>().HasMany(user => user.Followers);
            modelBuilder.Entity<User>().HasMany(user => user.BlockUsers);
            base.OnModelCreating(modelBuilder);
        }
    }
}