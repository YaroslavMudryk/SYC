using Microsoft.EntityFrameworkCore;
using SYC.Core.Models;
using SYC.Core.Storage.EntityFramework;

namespace SYC.Core.Storage.Repositories
{
    public class SqlServerPersonRepository : IPersonRepository
    {

        private readonly SqlServerPeopleDbContext _db;
        public SqlServerPersonRepository(SqlServerPeopleDbContext db)
        {
            _db = db;
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            await _db.People.AddAsync(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public ValueTask DisposeAsync()
        {
            return _db.DisposeAsync();
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _db.People.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _db.People.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemovePersonAsync(int id)
        {
            var person = await GetPersonByIdAsync(id);
            if (person == null)
                return;
            _db.People.Remove(person);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Person>> SearchPeopleAsync(string query)
        {
            var people = await _db.People
                .Where(x => x.FirstName.Contains(query) ||
                x.LastName.Contains(query) ||
                x.MiddleName.Contains(query) ||
                x.Description.Contains(query))
                .OrderBy(x => x.Id)
                .ToListAsync();

            if (people != null && people.Count > 0)
                people = people.DistinctBy(x => x.Id).ToList();

            return people;
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            _db.People.Update(person);
            await _db.SaveChangesAsync();
            return person;
        }
    }
}