using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessData
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<ComandaMercaderia> ComandaMercaderias { get; set; }
        public DbSet<FormaEntrega> FormaEntregas { get; set; }
        public DbSet<TipoMercaderia> TipoMercaderias { get; set; }
        public DbSet<Mercaderia> Mercaderias { get; set; }
        public object TipoMercaderia { get; set; }

        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Comanda
            modelBuilder.Entity<Comanda>().HasKey(e => e.Id);
            modelBuilder.Entity<Comanda>().Property(e => e.FormaEntregaId).IsRequired();

            // ComandaMercaderia
            modelBuilder.Entity<ComandaMercaderia>().Property(e => e.MercaderiaId).IsRequired();
            modelBuilder.Entity<ComandaMercaderia>().HasKey(e => e.Id);

            // Mercaderia
            modelBuilder.Entity<Mercaderia>().Property(e => e.Nombre).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Mercaderia>().Property(e => e.TipoMercaderiaId).IsRequired();
            modelBuilder.Entity<Mercaderia>().Property(e => e.Precio).IsRequired();
            modelBuilder.Entity<Mercaderia>().Property(e => e.Ingredientes).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Mercaderia>().Property(e => e.Preparacion).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Mercaderia>().Property(e => e.Imagen).IsRequired().HasMaxLength(255);

            // TipoMercaderia
            modelBuilder.Entity<TipoMercaderia>().Property(e => e.Description).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<TipoMercaderia>().HasData(new TipoMercaderia[]
            {
                new TipoMercaderia { Id = 1, Description = "Platos" },
                new TipoMercaderia { Id = 2, Description = "Bebidas" },
                new TipoMercaderia { Id = 3, Description = "Postres" }
            });

            // FormaEntrega
            modelBuilder.Entity<FormaEntrega>().Property(e => e.Description).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<FormaEntrega>().HasData(new FormaEntrega[]
            {
                new FormaEntrega {Id = 1, Description = "Salon"},
                new FormaEntrega {Id = 2, Description = "Delivery"},
                new FormaEntrega {Id = 3, Description = "Pedidos Ya"},
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}


