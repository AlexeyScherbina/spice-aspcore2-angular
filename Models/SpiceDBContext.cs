using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Spice.Models
{
    public partial class SpiceDBContext : IdentityDbContext<User>
    {

        public virtual DbSet<CatCell> CatCell { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CatMatrix> CatMatrix { get; set; }
        public virtual DbSet<Practice> Practice { get; set; }
        public virtual DbSet<ProcCell> ProcCell { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcMatrix> ProcMatrix { get; set; }
        public virtual DbSet<User> User { get; set; }


        public SpiceDBContext(DbContextOptions<SpiceDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CatCell>(entity =>
            {
                entity.HasKey(e => e.CellId);

                entity.HasIndex(e => new { e.Row, e.Col })
                    .HasName("IX_CatCell")
                    .IsUnique();

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.Cat1Id).HasColumnName("Cat1ID");

                entity.Property(e => e.Cat2Id).HasColumnName("Cat2ID");

                entity.Property(e => e.CatMatrixId).HasColumnName("CatMatrixID");

                entity.HasOne(d => d.CatMatrix)
                    .WithMany(p => p.CatCell)
                    .HasForeignKey(d => d.CatMatrixId)
                    .HasConstraintName("FK_CatCell_CatMatrix");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CatMatrix>(entity =>
            {
                entity.Property(e => e.CatMatrixId).HasColumnName("CatMatrixID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CatMatrix)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CatMatrix_User");
            });

            modelBuilder.Entity<Practice>(entity =>
            {
                entity.Property(e => e.PracticeId).HasColumnName("PracticeID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.Practice)
                    .HasForeignKey(d => d.ProcessId)
                    .HasConstraintName("FK_Practice_Process");
            });

            modelBuilder.Entity<ProcCell>(entity =>
            {
                entity.HasKey(e => e.CellId);

                entity.HasIndex(e => new { e.Row, e.Col })
                    .HasName("IX_ProcCell")
                    .IsUnique();

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.Proc1Id).HasColumnName("Proc1ID");

                entity.Property(e => e.Proc2Id).HasColumnName("Proc2ID");

                entity.Property(e => e.ProcMatrixId).HasColumnName("ProcMatrixID");

                entity.HasOne(d => d.ProcMatrix)
                    .WithMany(p => p.ProcCell)
                    .HasForeignKey(d => d.ProcMatrixId)
                    .HasConstraintName("FK_ProcCell_ProcMatrix");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.Property(e => e.ProcessId)
                    .HasColumnName("ProcessID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Process)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Process_Category");
            });

            modelBuilder.Entity<ProcMatrix>(entity =>
            {
                entity.Property(e => e.ProcMatrixId).HasColumnName("ProcMatrixID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProcMatrix)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ProcMatrix_Category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProcMatrix)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ProcMatrix_User");
            });

            modelBuilder.Entity<User>(entity =>
            {

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                
            });
            
        }
    }
}
