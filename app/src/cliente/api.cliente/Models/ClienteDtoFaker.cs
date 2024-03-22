using Bogus;

namespace api.cliente.Models;

public class ClienteDtoFaker
{
    private readonly Faker<Cliente> _fakerCliente = new();

    public ClienteDtoFaker()
    {
        var dateInicio = DateTime.Now.AddYears(-100);
        var dateFin = DateTime.Now.AddYears(-18);

        _fakerCliente.RuleFor(c => c.Id, f => Guid.NewGuid());
        _fakerCliente.RuleFor(c => c.Name, f => f.Name.FirstName());
        _fakerCliente.RuleFor(c => c.LastName, f => f.Name.LastName());
        _fakerCliente.RuleFor(c => c.DateBirth, f => f.Date.Between(dateInicio, dateFin));
    }

    internal IEnumerable<Cliente> Generar(int elementos)
    {
        return _fakerCliente.Generate(elementos);
    }
}