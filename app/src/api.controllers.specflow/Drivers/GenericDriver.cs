using shared.comun.hetoas;
using shared.comun.hetoas.extensions;

namespace api.controllers.specflow.Drivers;

public class GenericDriver<T> where T : class
{
    private readonly DataShaper<T> _dataShaper;

    public GenericDriver(DataShaper<T> dataShaper)
    {
        _dataShaper = dataShaper;
    }

    public List<ShapedEntity> GetShapedEntities(IEnumerable<T> entities, string fieldsString)
    {
        return _dataShaper.ShapeData(entities, fieldsString).ToList();
    }
    
    public ShapedEntity GetShapedEntity(T entity, string fieldsString)
    {
        return _dataShaper.ShapeData(entity, fieldsString);
    }
}