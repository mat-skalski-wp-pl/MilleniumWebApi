using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MilleniumWebApi.Models;
using MilleniumWebApi.Services.Interfaces;

namespace MilleniumWebApi.Services
{
    public class MyService : IMyService
    {
        private readonly ICollection<Person> People = new List<Person>()
        {
            new Person() {Id = 1, FirstName = "Harrison", LastName = "Ford"},
            new Person() {Id = 2, FirstName = "George", LastName = "Clooney"},
            new Person() {Id = 3, FirstName = "Robert", LastName = "De Niro"},
        };


        //zakładam że w aplikacji produkcyjnej operacja na danych będą wykonywane asynchronicznie
        public async Task<ICollection<Person>> GetAllPeopleAsync()
        {
            try
            {
                return await Task.Run(() => People);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            try
            {
                var result = await Task.Run(() => People.FirstOrDefault(f=>f.Id == id));

                if (result == null)
                {
                    return null;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
            try
            {
                if(People.Select(s=>s.Id).Contains(person.Id))
                {
                    person.Id = People.Max(m => m.Id) + 1;
                }
                
                await Task.Run(() => People.Add(person));

                return person;

            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<Person>> DeletePersonAsync(int id)
        {
            try
            {
                var person = await Task.Run(() => People.FirstOrDefault(f => f.Id == id));

                if (person == null)
                {
                    return null;
                }

                await Task.Run(() => People.Remove(person));
                return People;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Person> UpdatePerson(int id, Person person)
        {
            try
            {
                var existingPerson = await Task.Run(() =>People.FirstOrDefault(f => f.Id == id));

                if (person == null)
                {
                    return null;
                }

                if(String.IsNullOrEmpty(person.FirstName))
                {
                    person.FirstName = existingPerson.FirstName;
                }  
                
                if(String.IsNullOrEmpty(person.LastName))
                {
                    person.LastName = existingPerson.LastName;
                }

                person.Id = id;

                await Task.Run(() => People.Remove(existingPerson));
                await Task.Run(() => People.Add(person));
                return person;
            }
            catch
            {
                throw;
            }
        }
    }
}
