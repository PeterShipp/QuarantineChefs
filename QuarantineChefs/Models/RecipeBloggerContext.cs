using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuarantineChefs.Models
{
    public partial class RecipeBloggerContext : DbContext
    {
        public RecipeBloggerContext()
        {
        }

        public RecipeBloggerContext(DbContextOptions<RecipeBloggerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogArticle> BlogArticle { get; set; }
        public virtual DbSet<BlogComments> BlogComments { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RecipeComments> RecipeComments { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=RecipeBlogger;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogArticle>(entity =>
            {
                entity.Property(e => e.BlogText)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BlogComments>(entity =>
            {
                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogComments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__BlogComme__BlogI__52593CB8");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.BlogComments)
                    .HasForeignKey(d => d.UserProfileId)
                    .HasConstraintName("FK__BlogComme__UserP__5165187F");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.RecipeText)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RecipeComments>(entity =>
            {
                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeComments)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__RecipeCom__Recip__5812160E");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.RecipeComments)
                    .HasForeignKey(d => d.UserProfileId)
                    .HasConstraintName("FK__RecipeCom__UserP__571DF1D5");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserAccountId)
                    .IsRequired()
                    .IsUnicode(false);
            });
        }
    }
}
