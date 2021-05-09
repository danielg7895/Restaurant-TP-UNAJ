using AccessData;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class MercaderiaService
    {
        public static string AddMercaderia()
        {
            Console.WriteLine("Ingresa nombre del producto a registrar.");
            string productName = Console.ReadLine();

            Console.WriteLine("Ingresa el tipo de mercaderia: ");
            Console.WriteLine(GetTiposMercaderiaAsString());
            int tipoMercaderiaId = int.Parse(Console.ReadLine());
            using (RestaurantContext context = new()) { 
                bool tipoMercaderiaExists = context.TipoMercaderias.Find(tipoMercaderiaId) != null;
                if (!tipoMercaderiaExists) return "El ID del tipo de mercaderia ingresado no es valido";
            }

            Console.WriteLine("Ingresa el precio del producto");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingresa los ingredientes");
            string ingredients = Console.ReadLine();

            Console.WriteLine("Ingresa la preparacion");
            string preparation = Console.ReadLine();

            Console.WriteLine("Ingresa la url de la imagen");
            string imageUrl = Console.ReadLine();

            Mercaderia mercaderia = new()
            {
                Name = productName,
                TipoMercaderiaId = tipoMercaderiaId,
                Price = price,
                Ingredients = ingredients,
                Preparation = preparation,
                Image = imageUrl
            };
            using (RestaurantContext context = new())
            {
                context.Mercaderias.Add(mercaderia);
                context.SaveChanges();
            }

            return $"La mercaderia {productName} fue agregada a la base de datos correctamente.";
        }

        public static Mercaderia GetById(int mercaderiaId)
        {
            Mercaderia mercaderia = null;
            using (RestaurantContext context = new())
            {
                mercaderia = context.Mercaderias.Find(mercaderiaId);
            }

            if (mercaderia != null)
            {
                mercaderia.TipoMercaderia = TipoMercaderiaService.GetById(mercaderia.TipoMercaderiaId);
            }

            return mercaderia;
        }

        public static string GetMercaderiasAsString()
        {
            List<Mercaderia> mercaderias;
            using (RestaurantContext context = new())
            {
                mercaderias = context.Mercaderias.ToList();

            }
            if (!mercaderias.Any()) return null;

            string mercs = string.Join("", mercaderias.Select(o =>
            $"------------- ID: {o.Id} -------------\n" +
            $"Nombre: {o.Name} \nPrecio: {o.Price} \nIngredientes: {o.Ingredients} \nPreparacion: {o.Preparation} \nImagen: {o.Image}\n" +
            $"---------------------------------\n"
            ));

            return $"########################\nLista de mercaderias registradas: \n{mercs}########################\n";
        }

        public static List<Mercaderia> ChooseMercaderias()
        {
            List<Mercaderia> choosedMercaderias = new();

            while (true)
            {
                string mercaderias = GetMercaderiasAsString();
                if (mercaderias == null)
                {
                    Console.WriteLine("No hay mercaderias registradas!");
                    break;
                }
                Console.WriteLine(mercaderias);
                Console.WriteLine("Ingresa el ID de la mercaderia a agregar a la comanda.");
                try
                {
                    int mercaderiaId = int.Parse(Console.ReadLine());
                    Mercaderia choosedMercaderia;
                    using (RestaurantContext context = new())
                    {
                        choosedMercaderia = context.Mercaderias.Find(mercaderiaId);
                    }

                    if (choosedMercaderia == null)
                    {
                        Console.WriteLine("El ID ingresado es incorrecto, no se encontro una mercaderia con ese ID.");
                        continue;
                    }

                    choosedMercaderias.Add(choosedMercaderia);
                }
                catch (Exception)
                {
                    Console.WriteLine("Formato incorrecto, se aceptan solo numeros.");
                    continue;
                }

                Console.WriteLine("Agregar otra mercaderia a la comanda? 's' para agregar, cualquier otra tecla para finalizar");
                if (Console.ReadLine().ToLower() != "s") break;
            }

            return choosedMercaderias;
        }

        public static string GetTiposMercaderiaAsString()
        {
            List<TipoMercaderia> tipoMercaderia;
            using (RestaurantContext context = new()) {
                tipoMercaderia = context.TipoMercaderias.ToList();
            }

            string tiposMercaderia = string.Join("\n", tipoMercaderia.Select(o => $"{o.Id}. {o.Description} "));
            return tiposMercaderia;
        }

    }
}
