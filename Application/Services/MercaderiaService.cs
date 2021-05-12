using AccessData;
using AccessData.Queries;
using Domain.Commands;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class MercaderiaService : IMercaderiaService
    {
        private readonly IGenericRepository _repository;
        private readonly IMercaderiaQuery _query;

        public MercaderiaService(IGenericRepository repository, IMercaderiaQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public Mercaderia AddMercaderia(AddMercaderiaDTO mercaderiaDTO)
        {
            Mercaderia mercaderia = new()
            {
                Name = mercaderiaDTO.Name,
                Ingredients = mercaderiaDTO.Ingredients,
                Preparation = mercaderiaDTO.Preparation,
                TipoMercaderiaId = mercaderiaDTO.TipoMercaderiaId,
                Price = mercaderiaDTO.Price,
                Image = mercaderiaDTO.Image
            };
            _repository.Add(mercaderia);

            return mercaderia;
        }

        public Mercaderia GetMercaderia(int id)
        {
            return _query.GetMercaderia(id);
        }

        public Mercaderia GetMercaderiaByFilter<T>(T filter, string key)
        {
            return _query.GetMercaderiaByFilter(filter, key);
        }

        public List<Mercaderia> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key)
        {
            return _query.GetMercaderiaByFilterList(filterTypeList, key);
        }

    }
}
