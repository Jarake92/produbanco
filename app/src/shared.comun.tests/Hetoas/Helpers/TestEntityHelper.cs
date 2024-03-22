using shared.comun.hetoas;
using shared.comun.hetoas.extensions;

namespace shared.comun.tests.Hetoas.Helpers;

public class TestEntityHelper : EntityHelper<TestEntity>
{
    public TestEntityHelper(ISortHelper<TestEntity> sortHelper, IDataShaper<TestEntity> dataShaper)
        : base(sortHelper, dataShaper)
    {
    }
}