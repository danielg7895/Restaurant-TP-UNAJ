using AccessData;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class FormaEntregaService
    {
        public static FormaEntrega GetFormaEntrega(int formaEntregaId)
        {
            FormaEntrega formaEntrega = null;
            using (RestaurantContext context = new())
            {
                formaEntrega = context.FormaEntregas.Find(formaEntregaId);
            }
            return formaEntrega;
        }

        public static string GetDeliveryOptionsAsString()
        {
            List<FormaEntrega> formaEntregaList = null;
            using (RestaurantContext context = new()) { 
                formaEntregaList = context.FormaEntregas.ToList();
            }

            string formasDeEntrega = string.Join("\n", formaEntregaList.Select(o => $"{o.Id}. {o.Description} "));
            return formasDeEntrega;
        }


    }
}
