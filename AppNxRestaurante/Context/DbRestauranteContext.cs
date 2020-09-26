using System;
using AppNxRestaurante.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppNxRestaurante.Context
{
    public partial class DbRestauranteContext : DbContext
    {
        public DbRestauranteContext()
        {
        }

        public DbRestauranteContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCamarero> TCamarero { get; set; }
        public virtual DbSet<TCliente> TCliente { get; set; }
        public virtual DbSet<TCocinero> TCocinero { get; set; }
        public virtual DbSet<TDetalleFactura> TDetalleFactura { get; set; }
        public virtual DbSet<TFactura> TFactura { get; set; }
        public virtual DbSet<TMesa> TMesa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=localhost;port=3306;user=root;password=1000Ker.;database=db_nx_restaurante;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TCamarero>(entity =>
            {
                entity.HasKey(e => e.IdCamarero);

                entity.ToTable("t_camarero", "db_nx_restaurante");

                entity.Property(e => e.IdCamarero)
                    .HasColumnName("id_camarero")
                    .HasColumnType("int(18)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BActivo)
                    .HasColumnName("b_activo")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.FCreacion).HasColumnName("f_creacion");

                entity.Property(e => e.FModificacion).HasColumnName("f_modificacion");

                entity.Property(e => e.VApellido1)
                    .IsRequired()
                    .HasColumnName("v_apellido1")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VApellido2)
                    .HasColumnName("v_apellido2")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VNombre)
                    .IsRequired()
                    .HasColumnName("v_nombre")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("t_cliente", "db_nx_restaurante");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente")
                    .HasColumnType("int(18)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BActivo)
                    .HasColumnName("b_activo")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.FCreacion).HasColumnName("f_creacion");

                entity.Property(e => e.FModificacion).HasColumnName("f_modificacion");

                entity.Property(e => e.VApellido1)
                    .IsRequired()
                    .HasColumnName("v_apellido1")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VApellido2)
                    .HasColumnName("v_apellido2")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VNombre)
                    .IsRequired()
                    .HasColumnName("v_nombre")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TCocinero>(entity =>
            {
                entity.HasKey(e => e.IdCocinero);

                entity.ToTable("t_cocinero", "db_nx_restaurante");

                entity.Property(e => e.IdCocinero)
                    .HasColumnName("id_cocinero")
                    .HasColumnType("int(18)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BActivo)
                    .HasColumnName("b_activo")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.FCreacion).HasColumnName("f_creacion");

                entity.Property(e => e.FModificacion).HasColumnName("f_modificacion");

                entity.Property(e => e.VApellido1)
                    .IsRequired()
                    .HasColumnName("v_apellido1")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VApellido2)
                    .HasColumnName("v_apellido2")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VNombre)
                    .IsRequired()
                    .HasColumnName("v_nombre")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TDetalleFactura>(entity =>
            {
                entity.HasKey(e => e.IdDetalleFactura);

                entity.ToTable("t_detalle_factura", "db_nx_restaurante");

                entity.HasIndex(e => e.IdCocinero)
                    .HasName("fk_tcocinero_idx");

                entity.HasIndex(e => e.IdFactura)
                    .HasName("fk_tfactura_idx");

                entity.Property(e => e.IdDetalleFactura)
                    .HasColumnName("id_detalle_factura")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BActivo)
                    .HasColumnName("b_activo")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.DImporte)
                    .HasColumnName("d_importe")
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.DValor)
                    .HasColumnName("d_valor")
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.FCreacion).HasColumnName("f_creacion");

                entity.Property(e => e.FModificacion).HasColumnName("f_modificacion");

                entity.Property(e => e.IdCocinero)
                    .HasColumnName("id_cocinero")
                    .HasColumnType("int(18)");

                entity.Property(e => e.IdFactura)
                    .HasColumnName("id_factura")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.VPlato)
                    .IsRequired()
                    .HasColumnName("v_plato")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCocineroNavigation)
                    .WithMany(p => p.TDetalleFactura)
                    .HasForeignKey(d => d.IdCocinero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tCocinero");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.TDetalleFactura)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tFactura");
            });

            modelBuilder.Entity<TFactura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.ToTable("t_factura", "db_nx_restaurante");

                entity.HasIndex(e => e.IdCamarero)
                    .HasName("id_camarero_idx");

                entity.HasIndex(e => e.IdCliente)
                    .HasName("id_cliente_idx");

                entity.HasIndex(e => e.IdMesa)
                    .HasName("id_mesa_idx");

                entity.Property(e => e.IdFactura)
                    .HasColumnName("id_factura")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BActivo)
                    .HasColumnName("b_activo")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.FCreacion).HasColumnName("f_creacion");

                entity.Property(e => e.FFactura).HasColumnName("f_factura");

                entity.Property(e => e.FModificacion).HasColumnName("f_modificacion");

                entity.Property(e => e.IdCamarero)
                    .HasColumnName("id_camarero")
                    .HasColumnType("int(18)");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente")
                    .HasColumnType("int(18)");

                entity.Property(e => e.IdMesa)
                    .IsRequired()
                    .HasColumnName("id_mesa")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCamareroNavigation)
                    .WithMany(p => p.TFactura)
                    .HasForeignKey(d => d.IdCamarero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tcamarero");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TFactura)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tcliente");

                entity.HasOne(d => d.IdMesaNavigation)
                    .WithMany(p => p.TFactura)
                    .HasForeignKey(d => d.IdMesa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tmesa");
            });

            modelBuilder.Entity<TMesa>(entity =>
            {
                entity.HasKey(e => e.IdMesa);

                entity.ToTable("t_mesa", "db_nx_restaurante");

                entity.Property(e => e.IdMesa)
                    .HasColumnName("id_mesa")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.NMaxComensales)
                    .HasColumnName("n_maxComensales")
                    .HasColumnType("int(11)");

                entity.Property(e => e.VUbicacion)
                    .HasColumnName("v_ubicacion")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
