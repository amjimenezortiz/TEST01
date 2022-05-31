using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetPersons(PersonQueryFilter filters);
        Task<Person> GetPerson(int id);
        Task InsertPerson(Person person);
        Task UpdatePerson(Person person);
        Task<bool> DeletePerson(int id);
    }
}
