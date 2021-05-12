using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IMercaderiaService
    {
        public Mercaderia AddMercaderia(AddMercaderiaDTO mercaderiaDTO);

        public Mercaderia GetMercaderia(int id);
        public Mercaderia GetMercaderiaByFilter<T>(T filter, string key);
        public List<Mercaderia> GetMercaderiaByFilterList<T>(List<T> filterTypeList, string key);
    }
}
