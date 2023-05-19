using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace N5.Api.Models;

public partial class N5Context : DbContext
{
    public N5Context()
    {
    }

    public N5Context(DbContextOptions<N5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<TipoPermiso> TipoPermisos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //        => optionsBuilder.UseSqlServer("server=localhost; database=N5; Encrypt=false; integrated security=true;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permisos__3214EC07C6C5C523");

            entity.Property(e => e.ApellidoEmpleado).HasColumnType("text");
            entity.Property(e => e.FechaPermiso).HasColumnType("date");
            entity.Property(e => e.NombreEmpleado).HasColumnType("text");

            entity.HasOne(d => d.TipoPermisoNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.TipoPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TipoPermisos");
        });

        modelBuilder.Entity<TipoPermiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoPerm__3214EC07753FC758");

            entity.Property(e => e.Descripcion).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
