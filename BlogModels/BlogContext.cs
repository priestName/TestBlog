using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlogModels
{
    public partial class BlogContext : DbContext
    {
        public BlogContext()
        {
        }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<Administrator> Administrator { get; set; }
        //public virtual DbSet<Banner> Banner { get; set; }
        //public virtual DbSet<BrowseLog> BrowseLog { get; set; }
        public virtual DbSet<ContentList> ContentList { get; set; }
        //public virtual DbSet<GadgetList> GadgetList { get; set; }
        //public virtual DbSet<NewsList> NewsList { get; set; }
        //public virtual DbSet<OperationLog> OperationLog { get; set; }
        //public virtual DbSet<SystemParam> SystemParam { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=47.106.232.163;Initial Catalog=Blog;User ID=sa;Password=000;MultipleActiveResultSets=True;Application Name=EntityFramework");
            }
        }

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<BrowseLog>(entity =>
            {
                entity.Property(e => e.Equipment).IsRequired();

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ContentList>(entity =>
            {
                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.HeadImg)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsShow)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastTime).HasColumnType("datetime");

                entity.Property(e => e.Source).HasMaxLength(100);

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TypeValue)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<GadgetList>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(200);

                entity.Property(e => e.HeadImg).HasMaxLength(100);

                entity.Property(e => e.IsShow)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LinkAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NewsList>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.HeadImg).HasMaxLength(100);

                entity.Property(e => e.IsShow)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Source).HasMaxLength(100);

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OperationLog>(entity =>
            {
                entity.Property(e => e.Count).IsRequired();

                entity.Property(e => e.LoginIp)
                    .HasColumnName("LoginIP")
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SystemParam>(entity =>
            {
                entity.Property(e => e.Editable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ParameKey)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ParameType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ParameValues)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion
    }
}
