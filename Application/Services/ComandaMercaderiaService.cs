using AccessData;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class ComandaMercaderiaService
    {
        public static List<ComandaMercaderia> GetComandaMercaderias(Guid comandaId)
        {
            List<ComandaMercaderia> comandaMercaderias = null;
            using (RestaurantContext context = new())
            {
                comandaMercaderias = context.ComandaMercaderias.Where(c => c.ComandaId == comandaId).ToList();
            }

            if (comandaMercaderias != null)
            {
                comandaMercaderias.ForEach(cm =>
                {
                    cm.Comanda = ComandaService.GetById(cm.ComandaId);
                    cm.Mercaderia = MercaderiaService.GetById(cm.MercaderiaId);
                });
            }

            return comandaMercaderias;
        }

    }
}
