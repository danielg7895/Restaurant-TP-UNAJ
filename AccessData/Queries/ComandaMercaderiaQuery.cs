using Domain.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AccessData.Queries
{
    public interface IComandaMercaderiaQuery
    {
        public List<ComandaMercaderia> GetComandaMercaderiasByFilter<T>(T filter, string key);
    }
    public class ComandaMercaderiaQuery : IComandaMercaderiaQuery
    {
        readonly QueryFactory _db;

        public ComandaMercaderiaQuery(IDbConnection connection, Compiler compiler)
        {
            _db = new QueryFactory(connection, compiler);
        }

        public List<ComandaMercaderia> GetComandaMercaderiasByFilter<T>(T filter, string key)
        {
            List<ComandaMercaderia> comandaMercaderia = _db.Query("ComandaMercaderias").Where(key, filter).Get<ComandaMercaderia>().ToList();

            return comandaMercaderia;
        }
    }
}
