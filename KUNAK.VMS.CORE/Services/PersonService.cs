using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PersonService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public IEnumerable<Person> GetPersons(PersonQueryFilter filters)
        {
            var Persons = _unitOfWork.PersonRepository.GetAll();

            return Persons;
        }
        public Task<Person> GetPerson(int id)
        {
            return _unitOfWork.PersonRepository.GetById(id);
        }
        public async Task InsertPerson(Person person)
        {
            await _unitOfWork.PersonRepository.Add(person);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdatePerson(Person person)
        {
            _unitOfWork.PersonRepository.Update(person);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeletePerson(int id)
        {
            await _unitOfWork.PersonRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
