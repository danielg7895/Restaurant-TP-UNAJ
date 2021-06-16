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
            #region default mercaderia
            modelBuilder.Entity<Mercaderia>().HasData(new Mercaderia[]
            {
                new Mercaderia {
                    Id = 1,
                    Nombre = "Pollo a la plancha",
                    Precio = 655,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854775730096701460/unknown.png",
                    Ingredientes = "pollo, salsa, condimentos",
                    Preparacion = "Filetear pollo, agregarle condimentos y salsa y cocinarlo en la plancha.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 2,
                    Nombre = "Pollo a la parrilla",
                    Precio = 989,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854775798062252082/unknown.png",
                    Ingredientes = "pollo, salsa, condimentos",
                    Preparacion = "Condimentar pollo, agregarle salsa y cocinarlo en la parrilla.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 3,
                    Nombre = "Hamburguesa carne mechada",
                    Precio = 1021,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854775952034758677/unknown.png",
                    Ingredientes = "hamburguesa, remolacha, cebolla, queso, pan",
                    Preparacion = "Mechar carne, agregar condimentos, queso y preparar hamburguesa.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 4,
                    Nombre = "Papas fritas 500 g",
                    Precio = 800,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854776154422247474/unknown.png",
                    Ingredientes = "Papas, aceite",
                    Preparacion = "Pelar papas, cortarlas y cocinarlas en aceite hirviendo.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 5,
                    Nombre = "Papas fritas con cheddar verdeo y tocino",
                    Precio = 1200,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854776245363671101/unknown.png",
                    Ingredientes = "Papas, aceite, verdeo, cheddar",
                    Preparacion = "Pelar papas, cortarlas y cocinarlas en aceite hirviendo. Sacarlas una vez crujientes, agregar verdeo y cheddar.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 6,
                    Nombre = "Hamburguesa vegana mexicana",
                    Precio = 655,
                    Imagen = "https://media.discordapp.net/attachments/790717330447138847/854776334689501224/unknown.png",
                    Ingredientes = "Carne mexicana, chile, salsa mexicana, cheddar.",
                    Preparacion = "Cocinar la carne de hamburguesa, agregar chile, salsa, cheddar y preparar Hamburguesa.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 7,
                    Nombre = "Cocacola 500 ml",
                    Precio = 100,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854776543822086174/unknown.png",
                    Ingredientes = "Azucar, colorante, conservantes y azucar.",
                    Preparacion = "No corresponde",
                    TipoMercaderiaId = 2
                    },
                new Mercaderia {
                    Id = 8,
                    Nombre = "Agua mineral 500 ml",
                    Precio = 50,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854776771798630450/unknown.png",
                    Ingredientes = "Agua",
                    Preparacion = "No corresponde",
                    TipoMercaderiaId = 2
                    },
                new Mercaderia {
                    Id = 9,
                    Nombre = "Papas rejilla 500 g",
                    Precio = 800,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854777078977265754/unknown.png",
                    Ingredientes = "Papas, sal.",
                    Preparacion = "Pelar papas, cortarlas y cocinarlas en aceite hirviendo.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 10,
                    Nombre = "Cerveza Corona",
                    Precio = 400,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854777163018665994/unknown.png",
                    Ingredientes = "Cebada malteada, arroz y/o maíz, lúpulo, levadura, ácido ascórbico como antioxidante y alginato de propilenglicol.",
                    Preparacion = "No corresponde",
                    TipoMercaderiaId = 2
                    },
                new Mercaderia {
                    Id = 11,
                    Nombre = "Milanesa napolitana con papas",
                    Precio = 1100,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854777297978130473/unknown.png",
                    Ingredientes = "Carne, pan rallado, condimentos, queso, salsa, tocino, verdeo, papas, sal.",
                    Preparacion = "Preparar milanesa, cocinarla, agregarle queso, verdeo, tocino y salsa. Servir con papas fritas.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 12,
                    Nombre = "Lasagna",
                    Precio = 700,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854777518740471868/unknown.png",
                    Ingredientes = "Masa lasagna, ricota, queso, salsa, condimentos.",
                    Preparacion = "Las verduras las cortamos en trocitos pequeños para que se junten bien en la salsa. Las zanahorias las cortamos lo más fino (...)",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 13,
                    Nombre = "Tarta de verduras vegana",
                    Precio = 800,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854777652156956712/unknown.png",
                    Ingredientes = "Granos de choclo, morron rojo, queso de soja, masa tarta sin huevo, condimentos, verdeo.",
                    Preparacion = "Mezclar granos, morron, queso de soja, condimentos y verdeo, unirlos con suplemento de huevo y cocinar en la masa de tarta.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 14,
                    Nombre = "Empanadas veganas",
                    Precio = 655,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854777861609357342/unknown.png",
                    Ingredientes = "Soja o lentejas o garbanzos y tapa para empanada sin huevos.",
                    Preparacion = "Preparar relleno con la legumbre seleccionada, hacer el repulgue y cocinar.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 15,
                    Nombre = "Postre helado",
                    Precio = 700,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854778015502434344/unknown.png",
                    Ingredientes = "Helado, salsa helado",
                    Preparacion = "Servir helado y agregar salsa chocolate.",
                    TipoMercaderiaId = 3
                    },
                new Mercaderia {
                    Id = 16,
                    Nombre = "Jugo maracuya",
                    Precio = 300,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854778173500686366/unknown.png",
                    Ingredientes = "Maracuya",
                    Preparacion = "Exprimir maracuya, servir frio.",
                    TipoMercaderiaId = 2
                    },
                new Mercaderia {
                    Id = 17,
                    Nombre = "Jugo naranja",
                    Precio = 300,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854778278325911633/unknown.png",
                    Ingredientes = "Naranjas",
                    Preparacion = "Exprimir naranjas, servir frio.",
                    TipoMercaderiaId = 2
                    },
                new Mercaderia {
                    Id = 18,
                    Nombre = "Asado vegano",
                    Precio = 1300,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854778714918092827/unknown.png",
                    Ingredientes = "Seitan, carne vegana, soja",
                    Preparacion = "Preparar la carne vegana, asar, condimentar y servir.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 19,
                    Nombre = "Empanadas de seitan",
                    Precio = 600,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854778900793786408/unknown.png",
                    Ingredientes = "Seitan, condimentos, tapa empanada sin huevo.",
                    Preparacion = "Preparar relleno con seitan, hacer repulgue, cocinar y servir.",
                    TipoMercaderiaId = 1
                    },
                new Mercaderia {
                    Id = 20,
                    Nombre = "Guiso de lentejas",
                    Precio = 900,
                    Imagen = "https://cdn.discordapp.com/attachments/790717330447138847/854779018784276550/unknown.png",
                    Ingredientes = "Lentejas, salsa, chorizo colorado, papas, arroz.",
                    Preparacion = "Remojar lentejas 12 horas, pelar y cortar papas, preparar la salsa (...)",
                    TipoMercaderiaId = 1
                    },
            });
            #endregion

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


