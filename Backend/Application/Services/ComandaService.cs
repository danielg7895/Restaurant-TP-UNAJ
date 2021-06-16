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

        public GetComandaDTO AddComanda(AddComandaDTO comandaDTO)
        {
            List<GetMercaderiaDTO> mercaderias = _mercaderiaQuery.GetMercaderiaByFilterList(comandaDTO.Mercaderia, "id");
            if (mercaderias.Count == 0)
            {
                throw new InvalidIdentifier();
            }

            int totalPrice = 0;
            mercaderias.ForEach(m =>
            {
                totalPrice += m.Precio;
            });

            Comanda comanda = new()
            {
                Date = DateTime.Now,
                FormaEntregaId = comandaDTO.FormaEntrega,
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

            GetComandaDTO comandaResponseDTO = new()
            {
                Dia = comanda.Date,
                FormaEntrega = comanda.FormaEntregaId,
                Id = comanda.Id,
                PrecioTotal = comanda.TotalPrice
            };

            return comandaResponseDTO;
        }

        public GetComandaDTO GetComanda(string comandaGuidStr)
        {
            Guid comandaGuid;
            try
            {
                comandaGuid = new(comandaGuidStr);
            }
            catch { throw new InvalidGUID(); }
            GetComandaDTO comandaDTO = _query.GetComanda(comandaGuid);
            if (comandaDTO == null) throw new InvalidIdentifier();

            return comandaDTO;
        }

        public GetComandaDTO GetComandaByDate(string strDate)
        {
            GetComandaDTO comandaDTO;
            GetComandaDTO comandaResponseDTO;
            try
            {
                strDate = strDate.Split(" ")[0];
                DateTime date = DateTime.Parse(strDate);

                comandaDTO = _query.GetComandaByDate(date);

                List<ComandaMercaderia> comandaMercaderias = _comandaMercaderiaQuery.GetComandaMercaderiasByFilter(comandaDTO.Id, "ComandaId");
                List<int> comandaMercaderiasId = new();
                comandaMercaderias.ForEach(cm =>
                {
                    comandaMercaderiasId.Add(cm.Id);
                });

                comandaResponseDTO = new()
                {
                    Id = comandaDTO.Id,
                    FormaEntrega = comandaDTO.FormaEntrega,
                    PrecioTotal = comandaDTO.PrecioTotal,
                    Dia = comandaDTO.Dia,
                };
            }
            catch
            {
                throw new InvalidDate();
            }

            return comandaResponseDTO;
        }

        public List<GetComandaDTO> GetComandaByDateList(string strDate)
        {
            List<GetComandaDTO> comandas;
            try
            {
                DateTime date;
                if (strDate != "")
                {
                    strDate = strDate.Split(" ")[0];
                    date = DateTime.Parse(strDate);
                    comandas = _query.GetComandaByDateList(date);
                }
                else
                {
                    comandas = _query.GetComandas();
                }
            }
            catch
            {
                throw new InvalidDate();
            }
            return comandas;
        }

        public GetComandaDTO GetComandaByFilter<T>(T filter, string key)
        {
            return _query.GetComandaByFilter(filter, key);
        }

        public List<GetComandaDTO> GetComandaByFilterList<T>(List<T> filterTypeList, string key)
        {
            return _query.GetComandaByFilterList(filterTypeList, key);
        }

    }
}
