using Ak.DataWorkshop.Repositories;

namespace Ak.DataWorkshop.Tests.Repositories;

public class EnergyDataRepositoryTests
{
    [Test]
    public void CanListAllEntries()
    {
        var subject = new EnergyDataRepository();
        subject.ListAll().Take(10).Should().HaveCount(10);
    }
}
