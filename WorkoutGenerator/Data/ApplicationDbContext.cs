using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WorkoutGenerator.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<FitnessProgram> Programs { get; set; }
        public DbSet<YoutubeVideoQuery> YoutubeVideoQueries { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FitnessProgram>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(x => x.FitnessPrograms)
                .HasForeignKey(f => f.ApplicationUserId)
                .HasConstraintName("ApplicationUserId")
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ApplicationUser>().HasMany<FitnessProgram>().WithOne(x => x.ApplicationUser);
            base.OnModelCreating(modelBuilder);
        }
    }
}