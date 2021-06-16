using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IComandaService
    {
        public GetComandaDTO AddComanda(AddComandaDTO comanda);
        public GetComandaDTO GetComanda(string comandaGuidStr);
        public GetComandaDTO GetComandaByDate(string date);
        public List<GetComandaDTO> GetComandaByDateList(string strDate);
        public GetComandaDTO GetComandaByFilter<T>(T filter, string key);
        public List<GetComandaDTO> GetComandaByFilterList<T>(List<T> filterTypeList, string key);
    }
}
