using Domain.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;

namespace AccessData.Queries
{
    public interface IMercaderiaQuery
    {
        public Mercaderia GetMercaderia(int id);
        public Mercaderia GetMercaderiaByFilter<T>(T filter, string key);
        public List<Mercaderia> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key);
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

        public Mercaderia GetMercaderiaByFilter<T>(T filter, string key)
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
                Mercaderia mercaderia = GetMercaderiaByFilter(filter, key);
                if (mercaderia != null) mercaderias.Add(mercaderia);
            });

            return mercaderias;
        }
    }
}
