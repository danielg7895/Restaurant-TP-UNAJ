using Domain.DTOs;
using Domain.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AccessData.Queries
{
    public interface IMercaderiaQuery
    {
        public GetMercaderiaDTO GetMercaderia(int id);
        public List<GetMercaderiaDTO> GetMercaderias();
        public List<GetTipoMercaderiaDTO> GetTiposMercaderia();
        public GetMercaderiaDTO GetFirstMercaderiaByFilter<T>(T filter, string key);
        public List<GetMercaderiaDTO> GetMercaderiasByFilter<T>(T filter, string key);
        public List<GetMercaderiaDTO> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key);
        public List<GetMercaderiaDTO> GetMercaderiasByTipos(List<int> tiposId);
    }

    public class MercaderiaQuery : IMercaderiaQuery
    {
        readonly QueryFactory _db;

        public MercaderiaQuery(IDbConnection connection, Compiler compiler)
        {
            _db = new QueryFactory(connection, compiler);
        }


        public GetMercaderiaDTO GetMercaderia(int id)
        {
            GetMercaderiaDTO mercaderia = _db.Query("Mercaderias").Where("id", id).FirstOrDefault<GetMercaderiaDTO>();
            
            return mercaderia;
        }

        public List<GetMercaderiaDTO> GetMercaderias()
        {
            List<GetMercaderiaDTO> mercaderia = _db.Query("Mercaderias").Get<GetMercaderiaDTO>().ToList();

            return mercaderia;
        }

        public GetMercaderiaDTO GetFirstMercaderiaByFilter<T>(T filter, string key)
        {
            GetMercaderiaDTO mercaderia = _db.Query("Mercaderias").Where(key, filter).FirstOrDefault<GetMercaderiaDTO>();

            return mercaderia;
        }

        public List<GetMercaderiaDTO> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key)
        {
            // Busca todas las mercaderias en la base de datos que tengan una key
            // con nombre de 'key' y donde el valor de la key es de tipo T

            List<GetMercaderiaDTO> mercaderias = new();
            filterTypeList.ForEach(filter =>
            {
                GetMercaderiaDTO mercaderia = GetFirstMercaderiaByFilter(filter, key);
                if (mercaderia != null) mercaderias.Add(mercaderia);
            });

            return mercaderias;
        }

        public List<GetMercaderiaDTO> GetMercaderiasByTipos(List<int> tiposId)
        {
            List<GetMercaderiaDTO> mercaderias = new();
            tiposId = tiposId.Distinct().ToList();
            tiposId.ForEach(tipoId =>
            {
                List<GetMercaderiaDTO> mercaderia = GetMercaderiasByFilter(tipoId, "TipoMercaderiaId");
                if (mercaderia != null)
                {
                    mercaderias = mercaderias.Union(mercaderia).ToList();
                }
            });

            return mercaderias;
        }

        public List<GetMercaderiaDTO> GetMercaderiasByFilter<T>(T filter, string key)
        {
            List<GetMercaderiaDTO> mercaderias = _db.Query("Mercaderias").Where(key, filter).Get<GetMercaderiaDTO>().ToList();

            return mercaderias;
        }

        public List<GetTipoMercaderiaDTO> GetTiposMercaderia()
        {
            List<GetTipoMercaderiaDTO> tiposMercaderia = _db.Query("TipoMercaderias").Get<GetTipoMercaderiaDTO>().ToList();
            
            return tiposMercaderia;
        }
    }
}
