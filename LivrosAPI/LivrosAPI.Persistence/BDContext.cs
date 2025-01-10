using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Entities;
using LivrosAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LivrosAPI.Persistence
{
    public class BDContext(DbContextOptions<BDContext> options) : DbContext(options)
    {
        public DbSet<Erro> Erros { get; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<FormaCompra> FormaCompras { get; set; }
        public DbSet<LivroAssunto> LivroAssuntos { get; set; }
        public DbSet<LivroAutor> LivroAutores { get; set; }
        public DbSet<LivroValor> LivroValores { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>(entity =>
            {
                entity.ToTable(nameof(Livro));
                entity.HasKey(l => l.Id);
                
                entity.Property(l => l.Id)
                .ValueGeneratedOnAdd();

                entity.Property(l => l.Titulo)
                    .HasMaxLength(40)
                    .IsRequired();

                entity.Property(l => l.Editora)
                    .HasMaxLength(40)
                    .IsRequired();

                entity.Property(l => l.Edicao)
                    .IsRequired();

                entity.Property(l => l.AnoPublicacao)
                    .HasMaxLength(4)
                    .IsRequired();

                entity.Property(fc => fc.DataCriacao)
                   .HasColumnType(nameof(ESqlServerDataType.SmallDateTime))
                   .IsRequired();

                entity.HasMany(l => l.Autores)
                    .WithMany(la => la.Livros)
                    .UsingEntity<LivroAutor>(
                            l => l.HasOne<Autor>().WithMany().HasForeignKey(e => e.IdAutor),
                            r => r.HasOne<Livro>().WithMany().HasForeignKey(e => e.IdLivro)
                     );

                entity.HasMany(l => l.Assuntos)
                    .WithMany(la => la.Livros)
                    .UsingEntity<LivroAssunto>(
                         l => l.HasOne<Assunto>().WithMany().HasForeignKey(e => e.IdAssunto),
                         r => r.HasOne<Livro>().WithMany().HasForeignKey(e => e.IdLivro)
                    );

                entity.HasMany(l => l.LivroValores)
                   .WithOne(lv => lv.Livro)
                   .HasForeignKey(l => l.IdLivro)
                   .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(l => l.LivroAssuntos)
                  .WithOne(lv => lv.Livro)
                  .HasForeignKey(l => l.IdLivro)
                  .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(l => l.LivroAutores)
                  .WithOne(lv => lv.Livro)
                  .HasForeignKey(l => l.IdLivro)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable(nameof(Autor));
                entity.HasKey(a => a.Id);

                entity.Property(l => l.Id)
               .ValueGeneratedOnAdd();

                entity.Property(a => a.Nome)
                    .HasMaxLength(40)
                    .IsRequired();

                entity.Property(fc => fc.DataCriacao)
                   .HasColumnType(nameof(ESqlServerDataType.SmallDateTime))
                   .IsRequired();

                entity.HasMany(a => a.Livros)
                    .WithMany(la => la.Autores)
                    .UsingEntity<LivroAutor>(
                         l => l.HasOne<Livro>().WithMany().HasForeignKey(e => e.IdLivro),
                         r => r.HasOne<Autor>().WithMany().HasForeignKey(e => e.IdAutor)
                    );                   
            });
           
            modelBuilder.Entity<Assunto>(entity =>
            {
                entity.ToTable(nameof(Assunto));
                entity.HasKey(a => a.Id);

                entity.Property(l => l.Id)
               .ValueGeneratedOnAdd();

                entity.Property(a => a.Descricao)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(fc => fc.DataCriacao)
                   .HasColumnType(nameof(ESqlServerDataType.SmallDateTime))
                   .IsRequired();

                entity.HasMany(a => a.Livros)
                    .WithMany(la => la.Assuntos)
                    .UsingEntity<LivroAssunto>(
                         l => l.HasOne<Livro>().WithMany().HasForeignKey(e => e.IdLivro),
                         r => r.HasOne<Assunto>().WithMany().HasForeignKey(e => e.IdAssunto)
                    );   
                
            });

            modelBuilder.Entity<LivroAssunto>(entity =>
            {
                entity.ToTable(nameof(LivroAssunto));
                entity.HasKey(a => a.Id);

                entity.Property(l => l.Id)
               .ValueGeneratedOnAdd();
            
                modelBuilder.Entity<LivroAssunto>()
                    .HasOne(la => la.Livro)
                    .WithMany(l => l.LivroAssuntos)
                    .HasForeignKey(la => la.IdLivro)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<LivroAssunto>()
                    .HasOne(la => la.Assunto)
                    .WithMany(a => a.LivroAssuntos)
                    .HasForeignKey(la => la.IdAssunto)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<LivroAutor>(entity =>
            {
                entity.ToTable(nameof(LivroAutor));
                entity.HasKey(a => a.Id);

                entity.Property(l => l.Id)
               .ValueGeneratedOnAdd();
             
                modelBuilder.Entity<LivroAutor>()
                    .HasOne(la => la.Livro)
                    .WithMany(l => l.LivroAutores)
                    .HasForeignKey(la => la.IdLivro)
                    .OnDelete(DeleteBehavior.Cascade); ;

                modelBuilder.Entity<LivroAutor>()
                    .HasOne(la => la.Autor)
                    .WithMany(a => a.LivroAutores)
                    .HasForeignKey(la => la.IdAutor)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<LivroValor>(entity =>
            {
                entity.ToTable(nameof(LivroValor));
                entity.HasKey(a => a.Id);

                entity.Property(l => l.Id)
               .ValueGeneratedOnAdd();

                entity.Property(l => l.Valor)
               .HasColumnType("decimal(10,2)");

                modelBuilder.Entity<LivroValor>()
                    .HasOne(la => la.Livro)
                    .WithMany(l => l.LivroValores)
                    .HasForeignKey(la => la.IdLivro)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<LivroValor>()
                    .HasOne(la => la.FormaCompra)
                    .WithMany(a => a.LivroValores)
                    .HasForeignKey(la => la.IdFormaCompra)
                    .OnDelete(DeleteBehavior.Cascade); ;

            });

            modelBuilder.Entity<FormaCompra>(entity =>
            {
                entity.ToTable(nameof(FormaCompra));
                entity.HasKey(fc => fc.Id);

                entity.Property(l => l.Id)
               .ValueGeneratedOnAdd();

                entity.Property(fc => fc.Denominacao)
                    .IsRequired();

                entity.Property(fc => fc.DataCriacao)
                    .HasColumnType(nameof(ESqlServerDataType.SmallDateTime))
                    .IsRequired();

                entity.HasMany(l => l.LivroValores)
                 .WithOne(lv => lv.FormaCompra)
                 .HasForeignKey(l => l.IdFormaCompra)
                 .OnDelete(DeleteBehavior.Cascade)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Erro>(entity =>
            {
                entity.ToTable(nameof(Erro));
                entity.HasKey(a => a.Id);

                entity.Property(l => l.Id)
               .ValueGeneratedOnAdd();

            });

        }


        public async Task<int> ExecuteProcedureAsync(string procedure)
        {
            return await Database.ExecuteSqlRawAsync(procedure);
        }

       
    }



}
