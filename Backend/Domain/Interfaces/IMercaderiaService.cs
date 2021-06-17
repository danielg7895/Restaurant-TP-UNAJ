using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IMercaderiaService
    {
        public GetMercaderiaDTO AddMercaderia(AddMercaderiaDTO mercaderiaDTO);
        public GetMercaderiaDTO GetMercaderia(int id);
        public List<GetMercaderiaDTO> GetMercaderias();
        public List<GetTipoMercaderiaDTO> GetTiposMercaderia();
        public GetMercaderiaDTO GetMercaderiaByFilter<T>(T filter, string key);
        public List<GetMercaderiaDTO> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key);
        public List<GetMercaderiaDTO> GetMercaderiasByTipos(List<int> tiposId);
        public void RemoveMercaderia(int id);
        public GetMercaderiaDTO UpdateMercaderia(int mercaderiaId, AddMercaderiaDTO mercaderiaDTO);
    }
}
