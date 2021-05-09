using AccessData;
using Domain.Entities;

namespace Application.Services
{
    public class TipoMercaderiaService
    {

        public static TipoMercaderia GetById(int tipoMercaderiaId)
        {
            TipoMercaderia tipoMercaderia = null;
            using(RestaurantContext context = new())
            {
                tipoMercaderia = context.TipoMercaderias.Find(tipoMercaderiaId);
            }

            return tipoMercaderia;
        }

    }
}
