using AccessData.Queries;
using Domain.Commands;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;

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

        public GetMercaderiaDTO AddMercaderia(AddMercaderiaDTO mercaderiaDTO)
        {
            TipoMercaderia tipoMercaderia = _queryTipoMercaderia.GetTipoMercaderia(mercaderiaDTO.TipoMercaderiaId);
            if (tipoMercaderia == null) throw new InvalidIdentifier("tipoMercaderiaId");

            Mercaderia mercaderia = new()
            {
                Nombre = mercaderiaDTO.Nombre,
                Ingredientes = mercaderiaDTO.Ingredientes,
                Preparacion = mercaderiaDTO.Preparacion,
                TipoMercaderiaId = mercaderiaDTO.TipoMercaderiaId,
                Precio = mercaderiaDTO.Precio,
                Imagen = mercaderiaDTO.Imagen
            };
            _repository.Add(mercaderia);

            GetMercaderiaDTO responseMercaderia = new()
            {
                Id = mercaderia.Id,
                Nombre = mercaderiaDTO.Nombre,
                Ingredientes = mercaderiaDTO.Ingredientes,
                Preparacion = mercaderiaDTO.Preparacion,
                TipoMercaderiaId = mercaderiaDTO.TipoMercaderiaId,
                Precio = mercaderiaDTO.Precio,
                Imagen = mercaderiaDTO.Imagen
            };

            return responseMercaderia;
        }

        public GetMercaderiaDTO GetMercaderia(int id)
        {
            GetMercaderiaDTO mercaderia = _query.GetMercaderia(id);
            if (mercaderia == null) throw new InvalidIdentifier();

            return mercaderia;
        }

        public List<GetMercaderiaDTO> GetMercaderias()
        {
            List<GetMercaderiaDTO> mercaderias = _query.GetMercaderias();

            return mercaderias;
        }

        public GetMercaderiaDTO GetMercaderiaByFilter<T>(T filter, string key)
        {
            return _query.GetFirstMercaderiaByFilter(filter, key);
        }

        public List<GetMercaderiaDTO> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key)
        {
            return _query.GetMercaderiaByFilterList(filterTypeList, key);
        }

        public List<GetMercaderiaDTO> GetMercaderiasByTipos(List<int> tiposId)
        {
            return _query.GetMercaderiasByTipos(tiposId);
        }
        public void RemoveMercaderia(int id)
        {
            GetMercaderiaDTO mercaderia = _query.GetMercaderia(id);
            if (mercaderia == null) throw new InvalidIdentifier();

            Mercaderia merc = new()
            {
                Id = id,
                Imagen = mercaderia.Imagen,
                Ingredientes = mercaderia.Ingredientes,
                Nombre = mercaderia.Nombre,
                Precio = mercaderia.Precio,
                Preparacion = mercaderia.Preparacion,
                TipoMercaderiaId = mercaderia.TipoMercaderiaId
            };

            _repository.Remove(merc);
        }

        public GetMercaderiaDTO UpdateMercaderia(int mercaderiaId, AddMercaderiaDTO mercaderiaDTO)
        {
            GetMercaderiaDTO m = _query.GetMercaderia(mercaderiaId);
            if (m == null)
            {
                throw new InvalidIdentifier("mercaderia");
            }

            Mercaderia mercaderia = new()
            {
                Id = mercaderiaId,
                Nombre = mercaderiaDTO.Nombre,
                Ingredientes = mercaderiaDTO.Ingredientes,
                Preparacion = mercaderiaDTO.Preparacion,
                TipoMercaderiaId = mercaderiaDTO.TipoMercaderiaId,
                Precio = mercaderiaDTO.Precio,
                Imagen = mercaderiaDTO.Imagen
            };
            _repository.Update(mercaderia);

            GetMercaderiaDTO responseMercaderia = new()
            {
                Id = mercaderiaId,
                Nombre = mercaderiaDTO.Nombre,
                Ingredientes = mercaderiaDTO.Ingredientes,
                Preparacion = mercaderiaDTO.Preparacion,
                TipoMercaderiaId = mercaderiaDTO.TipoMercaderiaId,
                Precio = mercaderiaDTO.Precio,
                Imagen = mercaderiaDTO.Imagen
            };
            return responseMercaderia;
        }
    }
}
