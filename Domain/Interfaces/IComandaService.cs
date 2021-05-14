using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IComandaService
    {
        public ComandaResponseDTO AddComanda(AddComandaDTO comanda);
        public ComandaResponseDTO GetComanda(string comandaGuidStr);
        public ComandaResponseDTO GetComandaByDate(string date);
        public List<Comanda> GetComandaByDateList(string strDate);
        public Comanda GetComandaByFilter<T>(T filter, string key);
        public List<Comanda> GetComandaByFilterList<T>(List<T> filterTypeList, string key);
    }
}
