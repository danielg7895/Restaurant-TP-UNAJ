using AccessData.Queries;
using Domain.Commands;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public class ComandaService : IComandaService
    {
        private readonly IGenericRepository _repository;
        private readonly IComandaQuery _query;
        private readonly IMercaderiaQuery _mercaderiaQuery;

        public ComandaService(IGenericRepository repo, IComandaQuery query, IMercaderiaQuery mercaderiaQuery)
        {
            _repository = repo;
            _query = query;
            _mercaderiaQuery = mercaderiaQuery;
        }

        public Comanda AddComanda(AddComandaDTO comandaDTO)
        {
            List<Mercaderia> mercaderias = _mercaderiaQuery.GetMercaderiaByFilterList(comandaDTO.MercaderiasIds, "id");
            if (mercaderias.Count == 0)
            {
                throw new InvalidIdentifier();
            }

            int totalPrice = 0;
            mercaderias.ForEach(m =>
            {
                totalPrice += m.Price;
            });

            Comanda comanda = new()
            {
                Date = DateTime.Now,
                FormaEntregaId = comandaDTO.FormaEntregaId,
                TotalPrice = totalPrice
            };
            _repository.Add(comanda);


            mercaderias.ForEach(mercaderia =>
            {
                ComandaMercaderia comandaMercaderia = new()
                {
                    ComandaId = comanda.Id,
                    MercaderiaId = mercaderia.Id
                };

                _repository.Add(comandaMercaderia);
            });


            return comanda;
        }

        public Comanda GetComanda(string comandaGuidStr)
        {
            Guid comandaGuid = new(comandaGuidStr);
            return _query.GetComanda(comandaGuid);
        }

        public Comanda GetComandaByDate(string strDate)
        {
            Comanda comanda;
            try
            {
                strDate = strDate.Split(" ")[0];
                DateTime date = DateTime.Parse(strDate);

                comanda = _query.GetComandaByDate(date);
            }
            catch
            {
                throw new InvalidDate();
            }
            return comanda;
        }

        public List<Comanda> GetComandaByDateList(string strDate)
        {
            List<Comanda> comandas;
            try
            {
                strDate = strDate.Split(" ")[0];
                DateTime date = DateTime.Parse(strDate);

                comandas = _query.GetComandaByDateList(date);
            }
            catch
            {
                throw new InvalidDate();
            }
            return comandas;
        }

        public Comanda GetComandaByFilter<T>(T filter, string key)
        {
            return _query.GetComandaByFilter(filter, key);
        }

        public List<Comanda> GetComandaByFilterList<T>(List<T> filterTypeList, string key)
        {
            return _query.GetComandaByFilterList(filterTypeList, key);
        }

    }
}
