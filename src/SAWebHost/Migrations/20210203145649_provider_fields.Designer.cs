﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SAWebHost.Data;

namespace SAWebHost.Migrations
{
    [DbContext(typeof(SAWebHostContext))]
    [Migration("20210203145649_provider_fields")]
    partial class provider_fields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("SAWebHost.Data.Dto.AccessTokenEntry", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedOn")
                        .IsRequired()
                        .HasColumnType("nvarchar(48)");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("int");

                    b.Property<string>("OrganizationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RefreshTokenExpiresIn")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("ProviderId");

                    b.ToTable("AccessTokenEntry", "fan");
                });

            modelBuilder.Entity("SAWebHost.Data.Dto.LinkedInProvider", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorizationEndpoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Scope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenEndpoint")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationName")
                        .IsUnique();

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("LinkedInProvider", "fan");
                });

            modelBuilder.Entity("SAWebHost.Data.Dto.Organization", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Organization", "fan");
                });

            modelBuilder.Entity("SAWebHost.Data.Dto.AccessTokenEntry", b =>
                {
                    b.HasOne("SAWebHost.Data.Dto.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SAWebHost.Data.Dto.LinkedInProvider", null)
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
