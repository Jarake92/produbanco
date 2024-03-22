using Bogus;

namespace api.direccion.Models;

internal class DireccionDtoFaker
{
    private readonly Faker<Direccion> _fakerDireccion = new();

    public DireccionDtoFaker()
    {
        _fakerDireccion.StrictMode(true);
        _fakerDireccion.RuleFor(d => d.Id, f => Guid.NewGuid());
        _fakerDireccion.RuleFor(t => t.IdCliente, f => f.Random.Guid());
        _fakerDireccion.RuleFor(d => d.Canton, f => f.Address.City());
        _fakerDireccion.RuleFor(d => d.Provincia, f => f.Address.State());
        _fakerDireccion.RuleFor(d => d.CallePrincipal, f => f.Address.StreetAddress());
    }


    internal IEnumerable<Direccion> Generar(int elementos)
    {
        return _fakerDireccion.Generate(elementos);
    }
}