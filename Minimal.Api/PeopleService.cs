namespace Minimal.Api;

public record Person(string FullName);

public class PeopleService
{
    private readonly List<Person> _persons = new()
    {
        new Person("Rafael Milewski"),
        new Person("Jane Doe"),
        new Person("Nick Chapsas")
    };
    public IEnumerable<Person> Search(string searchTerm)
    {
        return _persons.Where(x => x.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

    }
}