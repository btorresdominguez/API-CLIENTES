using ApiClientes.Models;
using ApiClientes.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // ========== DbSets Autenticación ==========
        public DbSet<User> Usuarios => Set<User>();
        public DbSet<UserTokens> UsuarioTokens => Set<UserTokens>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRol> UsuarioRoles => Set<UserRol>();

        // ========== DbSets Clientes ==========
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =======================
            // Configuración Cliente
            // =======================
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(e => e.Identificacion).IsUnique();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("GETDATE()");
            });

            // =======================
            // Configuración Usuarios
            // =======================
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Ignorar propiedad calculada o lista en memoria
            modelBuilder.Entity<User>()
                .Ignore(u => u.Role);

            // Configuración de UsuarioRol con clave compuesta
            modelBuilder.Entity<UserRol>()
                .HasKey(ur => new { ur.IdUsuario, ur.IdRol });

            modelBuilder.Entity<UserRol>()
                .HasOne(ur => ur.Usuario)
                .WithMany(u => u.UsuarioRoles)
                .HasForeignKey(ur => ur.IdUsuario);

            modelBuilder.Entity<UserRol>()
                .HasOne(ur => ur.Rol)
                .WithMany(r => r.UsuarioRoles)
                .HasForeignKey(ur => ur.IdRol);

            // Relación UsuarioTokens ? Usuario
            modelBuilder.Entity<UserTokens>()
                .HasOne(ut => ut.Usuario)
                .WithMany(u => u.UsuarioTokens)
                .HasForeignKey(ut => ut.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
