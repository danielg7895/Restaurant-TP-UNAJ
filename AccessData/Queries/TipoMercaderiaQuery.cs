using Domain.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Data;

namespace AccessData.Queries
{
    public interface ITipoMercaderiaQuery
    {
        public TipoMercaderia GetTipoMercaderia(int id);
    }

    public class TipoMercaderiaQuery : ITipoMercaderiaQuery
    {

        private readonly QueryFactory _db;

        public TipoMercaderiaQuery(IDbConnection connection, Compiler compiler)
        {
            _db = new QueryFactory(connection, compiler);
        }

        public TipoMercaderia GetTipoMercaderia(int id)
        {
            return _db.Query("TipoMercaderias").Where("id", id).FirstOrDefault<TipoMercaderia>();
        }
    }
}
