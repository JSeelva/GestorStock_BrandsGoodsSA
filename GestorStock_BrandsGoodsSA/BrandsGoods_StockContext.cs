using System;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

#nullable disable

namespace GestorStock_BrandsGoodsSA.Models
{
    public partial class BrandsGoods_StockContext : DbContext
    {
        public BrandsGoods_StockContext()
        {
        }

        public BrandsGoods_StockContext(DbContextOptions<BrandsGoods_StockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestDetail> RequestDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                return;
            }

            string pathToContentRoot = Directory.GetCurrentDirectory();
            
            string json = Path.Combine(pathToContentRoot, "appsettings.json");


            if (!File.Exists(json))
            {
                string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(pathToContentRoot)
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = configurationBuilder.Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BrandsGoods_Stock"));

            base.OnConfiguring(optionsBuilder);

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.ArticleId).HasColumnName("articleID");

                entity.Property(e => e.ArticleAmount).HasColumnName("articleAmount");

                entity.Property(e => e.ArticleCode).HasColumnName("articleCode");

                entity.Property(e => e.ArticleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("articleName");

                entity.Property(e => e.ArticlePrice).HasColumnName("articlePrice");

                entity.Property(e => e.ArticleState).HasColumnName("articleState");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId).HasColumnName("clientID");

                entity.Property(e => e.ClientCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("clientCode")
                    .IsFixedLength(true);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("clientName");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.RequestId).HasColumnName("requestID");

                entity.Property(e => e.ClientId).HasColumnName("clientID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Client");
            });

            modelBuilder.Entity<RequestDetail>(entity =>
            {
                entity.ToTable("RequestDetail");

                entity.Property(e => e.RequestDetailId).HasColumnName("requestDetailID");

                entity.Property(e => e.ArticleId).HasColumnName("articleID");

                entity.Property(e => e.ArticleQuantity).HasColumnName("articleQuantity");

                entity.Property(e => e.RequestId).HasColumnName("requestID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.RequestDetails)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestDetail_Article");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestDetails)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestDetail_Request");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.UserName, "IX_User")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("passWord")
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.Property(e => e.UserNumber).HasColumnName("userNumber");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
