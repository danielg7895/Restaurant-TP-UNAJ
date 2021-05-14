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
        private readonly IComandaMercaderiaQuery _comandaMercaderiaQuery;

        public ComandaService(IGenericRepository repo, IComandaQuery query, IMercaderiaQuery mercaderiaQuery, IComandaMercaderiaQuery comandaMercaderiaQuery)
        {
            _repository = repo;
            _query = query;
            _mercaderiaQuery = mercaderiaQuery;
            _comandaMercaderiaQuery = comandaMercaderiaQuery;
        }

        public ComandaResponseDTO AddComanda(AddComandaDTO comandaDTO)
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

            List<int> comandaMercaderiasId = new();
            mercaderias.ForEach(mercaderia =>
            {
                ComandaMercaderia comandaMercaderia = new()
                {
                    ComandaId = comanda.Id,
                    MercaderiaId = mercaderia.Id
                };

                _repository.Add(comandaMercaderia);
                comandaMercaderiasId.Add(comandaMercaderia.Id);
            });

            ComandaResponseDTO comandaResponseDTO = new()
            {
                Id = comanda.Id,
                ComandaMercaderiasId = comandaMercaderiasId,
                Date = comanda.Date,
                FormaEntregaId = comanda.FormaEntregaId,
                TotalPrice = comanda.TotalPrice
            };

            return comandaResponseDTO;
        }

        public ComandaResponseDTO GetComanda(string comandaGuidStr)
        {
            Guid comandaGuid = new(comandaGuidStr);
            Comanda comanda = _query.GetComanda(comandaGuid);
            if (comanda == null) throw new InvalidIdentifier();

            List<ComandaMercaderia> comandaMercaderias = _comandaMercaderiaQuery.GetComandaMercaderiasByFilter(comanda.Id, "ComandaId");
            List<int> comandaMercaderiasId = new();
            comandaMercaderias.ForEach(cm =>
            {
                comandaMercaderiasId.Add(cm.Id);
            });

            ComandaResponseDTO comandaResponseDTO = new()
            {
                Id = comanda.Id,
                FormaEntregaId = comanda.FormaEntregaId,
                TotalPrice = comanda.TotalPrice,
                Date = comanda.Date,
                ComandaMercaderiasId = comandaMercaderiasId
            };

            return comandaResponseDTO;
        }

        public ComandaResponseDTO GetComandaByDate(string strDate)
        {
            Comanda comanda;
            ComandaResponseDTO comandaResponseDTO;
            try
            {
                strDate = strDate.Split(" ")[0];
                DateTime date = DateTime.Parse(strDate);

                comanda = _query.GetComandaByDate(date);

                List<ComandaMercaderia> comandaMercaderias = _comandaMercaderiaQuery.GetComandaMercaderiasByFilter(comanda.Id, "ComandaId");
                List<int> comandaMercaderiasId = new();
                comandaMercaderias.ForEach(cm =>
                {
                    comandaMercaderiasId.Add(cm.Id);
                });

                comandaResponseDTO = new()
                {
                    Id = comanda.Id,
                    FormaEntregaId = comanda.FormaEntregaId,
                    TotalPrice = comanda.TotalPrice,
                    Date = comanda.Date,
                    ComandaMercaderiasId = comandaMercaderiasId
                };
            }
            catch
            {
                throw new InvalidDate();
            }

            return comandaResponseDTO;
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
