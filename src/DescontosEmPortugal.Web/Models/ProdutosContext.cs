using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DescontosEmPortugal.Web.Models
{
    public partial class ProdutosContext : DbContext
    {
        public ProdutosContext()
        {
        }

        public ProdutosContext(DbContextOptions<ProdutosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Preco> Preco { get; set; }
        public virtual DbSet<PrecoVariacoes> PrecoVariacoes { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SitesAverificar> SitesAverificar { get; set; }
        public virtual DbSet<Website> Website { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Produtos;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Categori__7D8FE3B29D58BDC8")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Preco>(entity =>
            {
                entity.HasKey(e => e.IdPreco)
                    .HasName("PK__Preco__7A22FD1244CCF549");

                entity.Property(e => e.IdPreco).HasColumnName("ID_Preco");

                entity.Property(e => e.DataPrecoMaisBaixo)
                    .HasColumnName("Data_Preco_Mais_Baixo")
                    .HasColumnType("date");

                entity.Property(e => e.NewProduct).HasColumnName("New_Product");

                entity.Property(e => e.PrecoAtual).HasColumnName("Preco_Atual");

                entity.Property(e => e.PrecoMaisBaixo).HasColumnName("Preco_MaisBaixo");

                entity.Property(e => e.PrecoMaisBaixoFlag).HasColumnName("Preco_MaisBaixo_flag");
            });

            modelBuilder.Entity<PrecoVariacoes>(entity =>
            {
                entity.HasKey(e => e.IdVariacao)
                    .HasName("PK__Preco_Va__0A0792F206C57DFA");

                entity.ToTable("Preco_Variacoes");

                entity.Property(e => e.IdVariacao).HasColumnName("ID_Variacao");

                entity.Property(e => e.DataAlteracao)
                    .HasColumnName("Data_Alteracao")
                    .HasColumnType("date");

                entity.Property(e => e.IdPreco).HasColumnName("ID_Preco");

                entity.HasOne(d => d.IdPrecoNavigation)
                    .WithMany(p => p.PrecoVariacoes)
                    .HasForeignKey(d => d.IdPreco)
                    .HasConstraintName("FK__Preco_Var__ID_Pr__0E04126B");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");

                entity.Property(e => e.IdPesquisa).HasColumnName("ID_Pesquisa");

                entity.Property(e => e.IdPreco).HasColumnName("ID_Preco");

                entity.Property(e => e.Imagem)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__ID_Cate__5F492382");

                entity.HasOne(d => d.IdPesquisaNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdPesquisa)
                    .HasConstraintName("FK__Product__ID_Pesq__61316BF4");

                entity.HasOne(d => d.IdPrecoNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdPreco)
                    .HasConstraintName("FK__Product__ID_Prec__45544755");
            });

            modelBuilder.Entity<SitesAverificar>(entity =>
            {
                entity.HasKey(e => e.IdPesquisa)
                    .HasName("PK__SitesAVe__21E540BB4A6DB009");

                entity.ToTable("SitesAVerificar");

                entity.Property(e => e.IdPesquisa).HasColumnName("ID_Pesquisa");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");

                entity.Property(e => e.IdWebsite).HasColumnName("ID_Website");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.SitesAverificar)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__SitesAVer__ID_Ca__59904A2C");

                entity.HasOne(d => d.IdWebsiteNavigation)
                    .WithMany(p => p.SitesAverificar)
                    .HasForeignKey(d => d.IdWebsite)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SitesAVer__ID_We__5A846E65");
            });

            modelBuilder.Entity<Website>(entity =>
            {
                entity.HasKey(e => e.IdWebsite)
                    .HasName("PK__Website__9F8BC5ADFCE46DCE");

                entity.HasIndex(e => e.SiteUrl)
                    .HasName("UQ__Website__486ADA60FE7B7A1C")
                    .IsUnique();

                entity.Property(e => e.IdWebsite).HasColumnName("ID_Website");

                entity.Property(e => e.SiteUrl)
                    .HasColumnName("SiteURL")
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
