﻿// <auto-generated />
using System;
using AccessData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccessData.Migrations
{
    [DbContext(typeof(RestaurantContext))]
    partial class RestaurantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Comanda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormaEntregaId")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormaEntregaId");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("Domain.Entities.ComandaMercaderia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ComandaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MercaderiaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.HasIndex("MercaderiaId");

                    b.ToTable("ComandaMercaderias");
                });

            modelBuilder.Entity("Domain.Entities.FormaEntrega", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("FormaEntregas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Salon"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Delivery"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Pedidos Ya"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Mercaderia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Preparation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("TipoMercaderiaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoMercaderiaId");

                    b.ToTable("Mercaderias");
                });

            modelBuilder.Entity("Domain.Entities.TipoMercaderia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TipoMercaderias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Entrada"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Minutas"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Pastas"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Parrilla"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Pizzas"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Sandwich"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Ensaladas"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Bebidas"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Cerveza Artesanal"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Postres"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Comanda", b =>
                {
                    b.HasOne("Domain.Entities.FormaEntrega", "FormaEntrega")
                        .WithMany("Comanda")
                        .HasForeignKey("FormaEntregaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormaEntrega");
                });

            modelBuilder.Entity("Domain.Entities.ComandaMercaderia", b =>
                {
                    b.HasOne("Domain.Entities.Comanda", "Comanda")
                        .WithMany("ComandaMercaderias")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Mercaderia", "Mercaderia")
                        .WithMany()
                        .HasForeignKey("MercaderiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comanda");

                    b.Navigation("Mercaderia");
                });

            modelBuilder.Entity("Domain.Entities.Mercaderia", b =>
                {
                    b.HasOne("Domain.Entities.TipoMercaderia", "TipoMercaderia")
                        .WithMany()
                        .HasForeignKey("TipoMercaderiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoMercaderia");
                });

            modelBuilder.Entity("Domain.Entities.Comanda", b =>
                {
                    b.Navigation("ComandaMercaderias");
                });

            modelBuilder.Entity("Domain.Entities.FormaEntrega", b =>
                {
                    b.Navigation("Comanda");
                });
#pragma warning restore 612, 618
        }
    }
}
