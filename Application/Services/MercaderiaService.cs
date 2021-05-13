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
        private readonly ITipoMercaderiaQuery _queryTipoMercaderia;

        public MercaderiaService(IGenericRepository repository, IMercaderiaQuery query, ITipoMercaderiaQuery queryTipoMercaderia)
        {
            _repository = repository;
            _query = query;
            _queryTipoMercaderia = queryTipoMercaderia;
        }

        public Mercaderia AddMercaderia(AddMercaderiaDTO mercaderiaDTO)
        {
            TipoMercaderia tipoMercaderia = _queryTipoMercaderia.GetTipoMercaderia(mercaderiaDTO.TipoMercaderiaId);
            if (tipoMercaderia == null) throw new InvalidIdentifier("tipoMercaderiaId");

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
            Mercaderia mercaderia = _query.GetMercaderia(id);
            if (mercaderia == null) throw new InvalidIdentifier("id");

            return mercaderia;
        }

        public Mercaderia GetMercaderiaByFilter<T>(T filter, string key)
        {
            return _query.GetFirstMercaderiaByFilter(filter, key);
        }

        public List<Mercaderia> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key)
        {
            return _query.GetMercaderiaByFilterList(filterTypeList, key);
        }

        public List<Mercaderia> GetMercaderiasByTipos(List<int> tiposId)
        {
            return _query.GetMercaderiasByTipos(tiposId);
        }
        public void RemoveMercaderia(int id)
        {
            Mercaderia mercaderia = _query.GetMercaderia(id);
            if (mercaderia == null) throw new InvalidIdentifier();

            _repository.Remove(mercaderia);
        }

        public Mercaderia UpdateMercaderia(UpdateMercaderiaDTO mercaderiaDTO)
        {
            // falta validar tipomnercaderia, precio maximo, id mercaderia.
            Mercaderia mercaderia = new()
            {
                Id = mercaderiaDTO.Id,
                Name = mercaderiaDTO.Name,
                Ingredients = mercaderiaDTO.Ingredients,
                Preparation = mercaderiaDTO.Preparation,
                TipoMercaderiaId = mercaderiaDTO.TipoMercaderiaId,
                Price = mercaderiaDTO.Price,
                Image = mercaderiaDTO.Image
            };
            _repository.Update(mercaderia);

            return mercaderia;
        }
    }
}
