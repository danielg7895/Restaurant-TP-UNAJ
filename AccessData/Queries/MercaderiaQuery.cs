using Domain.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AccessData.Queries
{
    public interface IMercaderiaQuery
    {
        public Mercaderia GetMercaderia(int id);
        public Mercaderia GetFirstMercaderiaByFilter<T>(T filter, string key);
        public List<Mercaderia> GetMercaderiasByFilter<T>(T filter, string key);
        public List<Mercaderia> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key);
        public List<Mercaderia> GetMercaderiasByTipos(List<int> tiposId);
    }

    public class MercaderiaQuery : IMercaderiaQuery
    {
        readonly QueryFactory _db;

        public MercaderiaQuery(IDbConnection connection, Compiler compiler)
        {
            _db = new QueryFactory(connection, compiler);
        }


        public Mercaderia GetMercaderia(int id)
        {
            Mercaderia mercaderia = _db.Query("Mercaderias").Where("id", id).FirstOrDefault<Mercaderia>();
            
            return mercaderia;
        }

        public Mercaderia GetFirstMercaderiaByFilter<T>(T filter, string key)
        {
            Mercaderia mercaderia = _db.Query("Mercaderias").Where(key, filter).FirstOrDefault<Mercaderia>();

            return mercaderia;
        }

        public List<Mercaderia> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key)
        {
            // Busca todas las mercaderias en la base de datos que tengan una key
            // con nombre de 'key' y donde el valor de la key es de tipo T

            List<Mercaderia> mercaderias = new();
            filterTypeList.ForEach(filter =>
            {
                Mercaderia mercaderia = GetFirstMercaderiaByFilter(filter, key);
                if (mercaderia != null) mercaderias.Add(mercaderia);
            });

            return mercaderias;
        }

        public List<Mercaderia> GetMercaderiasByTipos(List<int> tiposId)
        {
            List<Mercaderia> mercaderias = new();
            tiposId = tiposId.Distinct().ToList();
            tiposId.ForEach(tipoId =>
            {
                List<Mercaderia> mercaderia = GetMercaderiasByFilter(tipoId, "TipoMercaderiaId");
                if (mercaderia != null)
                {
                    mercaderias = mercaderias.Union(mercaderia).ToList();
                }
            });

            return mercaderias;
        }

        public List<Mercaderia> GetMercaderiasByFilter<T>(T filter, string key)
        {
            List<Mercaderia> mercaderias = _db.Query("Mercaderias").Where(key, filter).Get<Mercaderia>().ToList();

            return mercaderias;
        }
    }
}
