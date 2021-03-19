using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SATokenRegisterApi.Data.Dto;

namespace SATokenRegisterApi.Data
{
    public class TokenRegistryContext : DbContext
    {
        public TokenRegistryContext(DbContextOptions<TokenRegistryContext> options)
            : base(options)
        {
        }

        public DbSet<Organization> Organization { get; set; }

        public DbSet<LinkedInProvider> LinkedInProvider { get; set; }

        public DbSet<AccessTokenEntry> AccessTokenEntry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LinkedInProvider>(p =>
            {
                p.ToTable("LinkedInProvider", "fan");
                p.HasKey(k => k.Id);

                p.Property(n => n.ApplicationName).IsRequired();
                p.HasIndex(i => i.ApplicationName).IsUnique();

                p.Property(n => n.ClientId).IsRequired();
                p.HasIndex(i => i.ClientId).IsUnique();
            });

            modelBuilder.Entity<Organization>(p =>
            {
                p.ToTable("Organization", "fan");
                p.HasKey(k => k.Id);

                p.Property(n => n.Name).IsRequired();
                p.HasIndex(i => i.Name).IsUnique();
            });

            modelBuilder.Entity<AccessTokenEntry>(p =>
            {
                p.ToTable("AccessTokenEntry", "fan");
                p.HasKey(k => k.Id);

                p.HasOne<Organization>()
                    .WithMany()
                    .HasForeignKey(k => k.OrganizationId)
                    .OnDelete(DeleteBehavior.Cascade);

                p.HasOne<LinkedInProvider>()
                    .WithMany()
                    .HasForeignKey(k => k.ProviderId)
                    .OnDelete(DeleteBehavior.Cascade);

                p.Property(n => n.CreatedOn)
                    .IsRequired()
                    .HasConversion<string>();
            });

        }

    }
}
