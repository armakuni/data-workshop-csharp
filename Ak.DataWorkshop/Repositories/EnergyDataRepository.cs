namespace Ak.DataWorkshop.Repositories;

using AutoFixture;

public interface IEnergyDataRepository
{
    IEnumerable<EnergyDataRecord> ListAll();
}

public class EnergyDataRepository : IEnergyDataRepository
{
    private readonly Fixture fixture = new ();

    public IEnumerable<EnergyDataRecord> ListAll()
    {
        while (true)
        {
            yield return this.RandomEnergyDataRecord();
        }
    }

    private EnergyDataRecord RandomEnergyDataRecord()
    {
        return this.fixture.Build<EnergyDataRecord>().Create();
    }
}
