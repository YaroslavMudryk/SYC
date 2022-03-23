using SYC.Core.Models;
using System.Text.Json;
namespace SYC.Core.Storage.Repositories
{
    public class JsonPersonRepository : IPersonRepository
    {
        private string _path = "People.json";
        private List<Person> _people;

        public JsonPersonRepository()
        {
            Download();
        }

        private void Download()
        {
            if (!File.Exists(_path))
            {
                File.Create(_path);
                _people = new List<Person>();
                return;
            }
            var sr = new StreamReader(_path);
            var content = sr.ReadToEnd();
            var people = JsonSerializer.Deserialize<List<Person>>(content);
            _people = people == null || people.Count == 0 ? new List<Person>() : people;
            sr.Dispose();
        }

        private async Task UploadAsync()
        {
            if (File.Exists(_path))
                File.Delete(_path);
            var sw = new StreamWriter(_path);
            var jsonContent = JsonSerializer.Serialize(_people);
            await sw.WriteAsync(jsonContent);
            sw.Dispose();
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            _people.Add(person);
            await UploadAsync();
            return person;
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await Task.FromResult(_people.OrderBy(s => s.Id).ToList());
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await Task.FromResult(_people.FirstOrDefault(s => s.Id == id));
        }

        public async Task RemovePersonAsync(int id)
        {
            var person = await GetPersonByIdAsync(id);
            _people.Remove(person);
            await UploadAsync();
        }

        public async Task<List<Person>> SearchPeopleAsync(string query)
        {
            var commonResult = new List<Person>();

            var peopleByNames = _people
                .Where(x => x.FirstName.Contains(query) ||
                x.LastName.Contains(query) ||
                x.MiddleName.Contains(query) ||
                x.Description.Contains(query))
                .ToList();
            commonResult.AddRange(peopleByNames);

            commonResult.AddRange(_people.Where(x => x.Phones.FirstOrDefault(s => s.Contains(query)) != null));

            commonResult.AddRange(_people.Where(x => x.Emails.FirstOrDefault(s => s.Contains(query)) != null));

            commonResult.AddRange(_people.Where(x => x.BankCards.FirstOrDefault(s => s.Contains(query)) != null));

            commonResult.AddRange(_people.Where(x => x.CarNumbers.FirstOrDefault(s => s.Contains(query)) != null));

            commonResult.DistinctBy(s => s.Id);
            commonResult = commonResult.OrderBy(x => x.Id).ToList();
            return await Task.FromResult(commonResult);
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            var currentPerson = await GetPersonByIdAsync(person.Id);
            _people.Remove(currentPerson);
            _people.Add(person);
            await UploadAsync();
            return person;
        }
    }
}