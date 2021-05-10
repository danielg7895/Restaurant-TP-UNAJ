using AccessData;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class ComandaService
    {

        public static string AddComanda()
        {

            List<Mercaderia> mercaderias = MercaderiaService.ChooseMercaderias();
            if (!mercaderias.Any()) return "La comanda debe tener al menos 1 mercaderia agregada.\n";

            Console.WriteLine("Ingresa la forma de entrega: ");
            Console.WriteLine(FormaEntregaService.GetDeliveryOptionsAsString());
            int formaEntregaId = int.Parse(Console.ReadLine());
            FormaEntrega formaEntrega = null;
            using (RestaurantContext context = new())
            {
                formaEntrega = context.FormaEntregas.Find(formaEntregaId);
                if (formaEntrega == null) return "No existe forma de entrega con el id ingresado.";
            }

            int totalPrice = mercaderias.Sum(merc => merc.Price);
            Comanda comanda = new()
            {
                FormaEntregaId = formaEntregaId,
                TotalPrice = totalPrice,
                Date = DateTime.Now
            };

            List<ComandaMercaderia> cms = new();
            mercaderias.ForEach(m =>
            {
                ComandaMercaderia cm = new()
                {
                    Comanda = comanda,
                    ComandaId = comanda.Id,
                    MercaderiaId = m.Id
                };
                cms.Add(cm);
            });

            comanda.ComandaMercaderias = cms;
            using(RestaurantContext _context = new())
            {
                _context.Comandas.Add(comanda);
                _context.SaveChanges();
            }

            return "La comanda fue agregada a la base de datos correctamente.";
        }

        public static Comanda GetById(Guid comandaId)
        {
            Comanda comanda = null;
            using (RestaurantContext context = new())
            {
                comanda = context.Comandas.Find(comandaId);
            }
            return comanda;
        }

        public static string GetComandasAsString()
        {
            List<Comanda> comandas = new();
            using (RestaurantContext _context = new())
            {
                comandas = _context.Comandas.ToList();
            }

            if (!comandas.Any()) return "No hay ninguna comanda registrada.";


            string message = "";
            comandas.ForEach(c =>
            {
                List<ComandaMercaderia> comandaMercaderias = ComandaMercaderiaService.GetComandaMercaderias(c.Id);
                FormaEntrega formaEntrega = FormaEntregaService.GetFormaEntrega(c.FormaEntregaId);

                message += $"------------- ID Comanda: {c.Id} -------------\n";
                if (comandaMercaderias != null)
                {
                    message += $"Listado de mercaderias de la comanda:\n";
                    comandaMercaderias.ForEach(cm =>
                    {
                        message += $"Nombre: {cm.Mercaderia.Name}\n" +
                        $"Tipo: {cm.Mercaderia.TipoMercaderia.Description}\n";
                    });
                }

                message += $"Precio Total: {c.TotalPrice}\n" +
                $"Forma de Entrega: {formaEntrega.Description}\n" +
                $"---------------------------------\n";
            });

            return $"############ Lista de comandas registradas ############ \n{message}\n################################################\n";
        }

    }
}
