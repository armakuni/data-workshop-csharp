using Ak.DataWorkshop.Actors;
using Ak.DataWorkshop.Repositories;
using Akka.TestKit.NUnit;
using AutoFixture;
using Moq;

namespace Ak.DataWorkshop.Tests.Actors;

public class EnergyDataSpoutActorTests : TestKit
{

    [Test]
    public void OutputsFromRepository()
    {
        var fixture = new Fixture();

        var mockRepository = new Mock<IEnergyDataRepository>();
        var expected = new List<EnergyDataRecord>
        {
            fixture.Create<EnergyDataRecord>(),
            fixture.Create<EnergyDataRecord>(),
            fixture.Create<EnergyDataRecord>(),
            fixture.Create<EnergyDataRecord>(),
        };

        mockRepository.Setup(static repo => repo.ListAll())
            .Returns(expected);
        var probe = this.CreateTestProbe();
        var props = EnergyDataSpoutActor.Props(probe.Ref, mockRepository.Object);
        this.Sys.ActorOf(props);

        this.Within(TimeSpan.FromSeconds(5), () =>
        {
            expected.ForEach(item => probe.ExpectMsg(item));
        });
    }
}
