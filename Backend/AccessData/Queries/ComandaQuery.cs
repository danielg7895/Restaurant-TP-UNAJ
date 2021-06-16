using Domain.DTOs;
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
        public GetComandaDTO GetComanda(Guid comandaId);
        public List<GetComandaDTO> GetComandas();
        public GetComandaDTO GetComandaByDate(DateTime date);
        public List<GetComandaDTO> GetComandaByDateList(DateTime date);
        public GetComandaDTO GetComandaByFilter<T>(T filter, string key);
        public List<GetComandaDTO> GetComandaByFilterList<T>(List<T> filterTypeList, string key);
    }

    public class ComandaQuery : IComandaQuery
    {
        readonly QueryFactory _db;

        public ComandaQuery(IDbConnection connection, Compiler compiler)
        {
            _db = new QueryFactory(connection, compiler);
        }

        public GetComandaDTO GetComanda(Guid comandaId)
        {
            Comanda comanda = _db.Query("Comandas").Where("Id", comandaId).FirstOrDefault<Comanda>();
            if (comanda == null) return null;

            GetComandaDTO comandaDTO = new()
            {
                Dia = comanda.Date,
                FormaEntrega = comanda.FormaEntregaId,
                Id = comanda.Id,
                PrecioTotal = comanda.TotalPrice
            };

            return comandaDTO;
        }

        public List<GetComandaDTO> GetComandas()
        {
            List<Comanda> comandas = _db.Query("Comandas").Get<Comanda>().ToList();
            if (comandas == null) return null;

            List<GetComandaDTO> comandasDTO = new();
            comandas.ForEach((comanda) =>
            {
                GetComandaDTO comandaDTO = new()
                {
                    Dia = comanda.Date,
                    FormaEntrega = comanda.FormaEntregaId,
                    Id = comanda.Id,
                    PrecioTotal = comanda.TotalPrice
                };
                comandasDTO.Add(comandaDTO);
            });

            return comandasDTO;
        }

        public GetComandaDTO GetComandaByDate(DateTime date)
        {
            DateTime nextDay = date.AddDays(1);
            GetComandaDTO comanda = _db.Query("Comandas").Where("Date", ">=", date).Where("Date", "<", nextDay).FirstOrDefault<GetComandaDTO>();

            return comanda;
        }

        public List<GetComandaDTO> GetComandaByDateList(DateTime date)
        {
            DateTime nextDay = date.AddDays(1);
            List<Comanda> comandas = _db.Query("Comandas").Where("Date", ">=", date).Where("Date", "<", nextDay).Get<Comanda>().ToList();
            if (comandas == null) return null;

            List<GetComandaDTO> comandasDTO = new();
            comandas.ForEach((comanda) =>
            {
                GetComandaDTO comandaDTO = new()
                {
                    Dia = comanda.Date,
                    FormaEntrega = comanda.FormaEntregaId,
                    Id = comanda.Id,
                    PrecioTotal = comanda.TotalPrice
                };
                comandasDTO.Add(comandaDTO);
            });

            return comandasDTO;
        }

        public GetComandaDTO GetComandaByFilter<T>(T filter, string key)
        {
            GetComandaDTO comanda = _db.Query("Comandas").Where(key, filter).FirstOrDefault<GetComandaDTO>();

            return comanda;
        }

        public List<GetComandaDTO> GetComandaByFilterList<T>(List<T> filterTypeList, string key)
        {

            List<GetComandaDTO> comandas = new();
            filterTypeList.ForEach(filter =>
            {
                GetComandaDTO comanda = GetComandaByFilter(filter, key);
                if (comanda != null) comandas.Add(comanda);
            });

            return comandas;
        }

    }
}
