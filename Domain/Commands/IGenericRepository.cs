using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public interface IGenericRepository
    {
        public void Add<T>(T entity) where T : class;
    }
}
