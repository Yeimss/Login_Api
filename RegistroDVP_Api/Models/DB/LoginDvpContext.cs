using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RegistroDVP_Api.Models.DB;

public partial class LoginDvpContext : DbContext
{
    public LoginDvpContext()
    {
    }

    public LoginDvpContext(DbContextOptions<LoginDvpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Identificador).HasName("PK__Personas__F2374EB1FB7EDD2D");

            entity.HasIndex(e => e.Email, "UQ__Personas__A9D10534ACFB27E3").IsUnique();

            entity.HasIndex(e => e.NumeroIdentificacion, "UQ__Personas__FCA68D9100C7BE98").IsUnique();

            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("smalldatetime");
            entity.Property(e => e.IdentificacionCompleta)
                .HasMaxLength(73)
                .IsUnicode(false)
                .HasComputedColumnSql("((([NumeroIdentificacion]+' (')+[TipoIdentificacion])+')')", false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(201)
                .IsUnicode(false)
                .HasComputedColumnSql("(concat([Nombres],' ',[Apellidos]))", false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Identificador).HasName("PK__Usuario__F2374EB110E96147");

            entity.ToTable("Usuario");

            entity.Property(e => e.Identificador).ValueGeneratedNever();
            entity.Property(e => e.FechaCreacion).HasColumnType("smalldatetime");
            entity.Property(e => e.Pass)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdentificadorNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.Identificador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Person");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
