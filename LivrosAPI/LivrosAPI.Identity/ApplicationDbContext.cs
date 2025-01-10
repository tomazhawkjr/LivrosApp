using LivrosAPI.Application.Models.Application;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LivrosAPI.Identity
{
    public class ApplicationDbContext
    : IdentityDbContext<
        ApplicationUser, ApplicationRole, string>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.HasMany(e => e.Claims)
              .WithOne()
              .HasForeignKey(uc => uc.UserId)
              .IsRequired();

                b.HasMany(e => e.Logins)
              .WithOne()
              .HasForeignKey(ul => ul.UserId)
              .IsRequired();

                b.HasMany(e => e.Tokens)
              .WithOne()
              .HasForeignKey(ut => ut.UserId)
              .IsRequired();

                b.HasMany(e => e.UserRoles)
              .WithOne()
              .HasForeignKey(ur => ur.UserId)
              .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.HasMany(e => e.UserRoles)
              .WithOne(e => e.Role)
              .HasForeignKey(ur => ur.RoleId)
              .IsRequired();
            });

            modelBuilder.Entity<Client>(b =>
            {
                b.Property<string>("Id")
              .HasColumnType("nvarchar(450)");

                b.Property<bool>("Active")
              .HasColumnType("bit");

                b.Property<string>("AllowedOrigin")
              .HasColumnType("nvarchar(100)")
              .HasMaxLength(100);

                b.Property<string>("Name")
              .IsRequired()
              .HasColumnType("nvarchar(100)")
              .HasMaxLength(100);

                b.Property<int>("RefreshTokenLifeTime")
              .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("Clients");
            });

            modelBuilder.Entity<RefreshToken>(b =>
            {
                b.Property<string>("Token")
              .HasColumnType("nvarchar(450)");

                b.Property<string>("ClientId")
              .IsRequired()
              .HasColumnType("nvarchar(50)")
              .HasMaxLength(50);

                b.Property<DateTime>("ExpiresUtc")
              .HasColumnType("datetime2");

                b.Property<DateTime>("IssuedUtc")
              .HasColumnType("datetime2");

                b.Property<string>("UserName")
              .IsRequired()
              .HasColumnType("nvarchar(50)")
              .HasMaxLength(50);

                b.HasKey("Token");

                b.ToTable("RefreshTokens", tb => tb.HasTrigger("RefreshToken_Insert"));
            });

        }
    }
}
