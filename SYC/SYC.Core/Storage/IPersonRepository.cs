using SYC.Core.Models;

namespace SYC.Core.Storage
{
    public interface IPersonRepository
    {
        Task<List<Person>> SearchPeopleAsync(string query);
        Task<List<Person>> GetAllPeopleAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task<Person> CreatePersonAsync(Person person);
        Task<Person> UpdatePersonAsync(Person person);
        Task RemovePersonAsync(int id);
    }
}