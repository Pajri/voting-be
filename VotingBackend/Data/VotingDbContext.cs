using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Models;

namespace VotingBackend.Data
{
    public class VotingDbContext : DbContext
    {
        public VotingDbContext()
        {

        }

        public VotingDbContext(DbContextOptions<VotingDbContext> options) : base(options)
        {

        }

        DbSet<Category> Categories;
        DbSet<User> Users;
        DbSet<Voting> Votings;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Name).HasColumnName("name").IsRequired();

                entity.HasMany(e => e.Votings).WithOne(e => e.Category);
            });

            builder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .IsRequired();

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .IsRequired();

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .IsRequired();

                entity.HasMany(e => e.Votings).WithMany(e => e.Voters);
            });

            builder.Entity<Voting>(entity =>
            {
                entity.ToTable("votings");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .IsRequired();

                entity.Property(e => e.VotersCount)
                    .HasColumnName("voters_count")
                    .IsRequired();

                entity.Property(e => e.DueDate)
                    .HasColumnName("due_date")
                    .IsRequired();

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .IsRequired();

                entity.HasOne(e => e.Category)
                    .WithMany(e => e.Votings)
                    .HasForeignKey(e => e.CategoryId)
                    .HasConstraintName("fk_voting_category");

                entity.HasMany(e => e.Voters).WithMany(e => e.Votings);
            });
        }

        public DbSet<VotingBackend.Models.Category> Category { get; set; }

        public DbSet<VotingBackend.Models.Voting> Voting { get; set; }
    }
}
