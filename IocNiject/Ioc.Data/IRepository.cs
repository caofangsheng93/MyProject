using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ioc.Core;

namespace Ioc.Data
{
    public interface IRepository<T> where T:BaseEntity 
    {
        T GetByID(object id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> Table { get; }
    }
}
