using Domain.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Data;
using System.Linq;

namespace AccessData.Queries
{
    public interface IComandaQuery
    {
        public Comanda GetComanda(Guid comandaId);
    }

    public class ComandaQuery : IComandaQuery
    {
        readonly QueryFactory _db;

        public ComandaQuery(IDbConnection connection, Compiler compiler)
        {
            _db = new QueryFactory(connection, compiler);
        }

        public Comanda GetComanda(Guid comandaId)
        {
            Comanda comanda = _db.Query("Comandas").Where("Id", comandaId).FirstOrDefault<Comanda>();
            
            return comanda;
        }

        /**
        public string GetComandasAsString()
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
        }**/

    }
}
