using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int? id);
        IEnumerable<T> GetAll();
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int? id);

    }
}
