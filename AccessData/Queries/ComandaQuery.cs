using Domain.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AccessData.Queries
{
    public interface IComandaQuery
    {
        public Comanda GetComanda(Guid comandaId);
        public Comanda GetComandaByDate(DateTime date);
        public List<Comanda> GetComandaByDateList(DateTime date);
        public Comanda GetComandaByFilter<T>(T filter, string key);
        public List<Comanda> GetComandaByFilterList<T>(List<T> filterTypeList, string key);
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

        public Comanda GetComandaByDate(DateTime date)
        {
            DateTime nextDay = date.AddDays(1);
            Comanda comanda = _db.Query("Comandas").Where("Date", ">=", date).Where("Date", "<", nextDay).FirstOrDefault<Comanda>();

            return comanda;
        }

        public List<Comanda> GetComandaByDateList(DateTime date)
        {
            DateTime nextDay = date.AddDays(1);
            List<Comanda> comandas = _db.Query("Comandas").Where("Date", ">=", date).Where("Date", "<", nextDay).Get<Comanda>().ToList();

            return comandas;
        }

        public Comanda GetComandaByFilter<T>(T filter, string key)
        {
            Comanda comanda = _db.Query("Comandas").Where(key, filter).FirstOrDefault<Comanda>();

            return comanda;
        }

        public List<Comanda> GetComandaByFilterList<T>(List<T> filterTypeList, string key)
        {
            // Busca todas las Comandas en la base de datos que tengan una key
            // con nombre de 'key' y donde el valor de la key es de tipo T

            List<Comanda> comandas = new();
            filterTypeList.ForEach(filter =>
            {
                Comanda comanda = GetComandaByFilter(filter, key);
                if (comanda != null) comandas.Add(comanda);
            });

            return comandas;
        }

    }
}
