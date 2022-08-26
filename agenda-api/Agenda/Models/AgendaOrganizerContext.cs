using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agenda.Models
{
    public partial class AgendaOrganizerContext : DbContext
    {
        public AgendaOrganizerContext()
        {
        }

        public AgendaOrganizerContext(DbContextOptions<AgendaOrganizerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgendaItem> AgendaItems { get; set; } = null!;
        public virtual DbSet<AgendaRoleMatrix> AgendaRoleMatrices { get; set; } = null!;
        public virtual DbSet<AgendaType> AgendaTypes { get; set; } = null!;
        public virtual DbSet<Approver> Approvers { get; set; } = null!;
        public virtual DbSet<ApproverRole> ApproverRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AgendaOrganizer;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgendaItem>(entity =>
            {
                entity.HasKey(e => e.AgendaId);

                entity.ToTable("AgendaItem");

                entity.Property(e => e.AgendaItem1)
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasColumnName("AgendaItem");
            });

            modelBuilder.Entity<AgendaRoleMatrix>(entity =>
            {
                entity.HasKey(e => new { e.AgendaType, e.RoleId });

                entity.ToTable("AgendaRoleMatrix");
            });

            modelBuilder.Entity<AgendaType>(entity =>
            {
                entity.ToTable("AgendaType");

                entity.Property(e => e.AgendaTypeId).ValueGeneratedNever();

                entity.Property(e => e.AgendaDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Approver>(entity =>
            {
                entity.Property(e => e.ApproverName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Approvers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Approvers_ApproverRoles");
            });

            modelBuilder.Entity<ApproverRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
