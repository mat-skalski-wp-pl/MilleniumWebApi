using System.Collections.Generic;
using System.Threading.Tasks;
using MilleniumWebApi.Models;

namespace MilleniumWebApi.Services.Interfaces
{
    public interface IMyService
    {
        Task<ICollection<Person>> GetAllPeopleAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task<Person> AddPersonAsync(Person person);
        Task<ICollection<Person>> DeletePersonAsync(int id);
        Task<Person> UpdatePerson(int id, Person person);
    }
}
