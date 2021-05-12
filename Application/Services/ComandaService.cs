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

            Comanda comanda = new()
            {
                Date = DateTime.Now,
                FormaEntregaId = comandaDTO.FormaEntregaId,
                TotalPrice = comandaDTO.TotalPrice
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

    }
}
