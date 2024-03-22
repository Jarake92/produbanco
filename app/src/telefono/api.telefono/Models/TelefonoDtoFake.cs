using Bogus;

namespace api.telefono.Models;

internal class TelefonoDtoFake
{
    private readonly Faker<Telefono> _fakerTelefono = new();

    internal TelefonoDtoFake()
    {
        _fakerTelefono.StrictMode(true);

        _fakerTelefono.RuleFor(t => t.Id, f => Guid.NewGuid());
        _fakerTelefono.RuleFor(t => t.IdCliente, f => f.Random.Guid());
        _fakerTelefono.RuleFor(t => t.Numero, f => f.Phone.PhoneNumber("0#-###-###-###"));
        _fakerTelefono.RuleFor(t => t.Operadora, f => f.PickRandom<Operadora>());
        _fakerTelefono.RuleFor(t => t.Tipo, f => f.PickRandom<TipoTelefono>());
    }

    internal IEnumerable<Telefono> Generar(int elementos)
    {
        return _fakerTelefono.Generate(elementos);
    }
}