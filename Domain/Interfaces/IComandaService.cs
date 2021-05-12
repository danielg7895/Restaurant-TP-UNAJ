using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IComandaService
    {
        public Comanda AddComanda(AddComandaDTO comanda);
        public Comanda GetComanda(string comandaGuidStr);

    }
}
