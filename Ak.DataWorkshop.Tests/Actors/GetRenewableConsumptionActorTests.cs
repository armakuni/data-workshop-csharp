using Ak.DataWorkshop.Actors;
using Ak.DataWorkshop.Repositories;
using Akka.Actor;
using Akka.TestKit.NUnit;
using AutoFixture;

namespace Ak.DataWorkshop.Tests.Actors;

public class GetRenewableConsumptionActorTests : TestKit
{
    [Test]
    public void GetRenewableConsumption()
    {
        var fixture = new Fixture();

        var probe = this.CreateTestProbe();
        var fieldPickingActor = GetRenewableConsumptionActor.Props(probe.Ref);
        var subject = this.Sys.ActorOf(fieldPickingActor);

        var record = fixture.Create<EnergyDataRecord>();

        subject.Tell(record);

        this.Within(TimeSpan.FromSeconds(5), () =>
        {
            probe.ExpectMsg<double>(msg => msg.Should().Be(record.RenewablesConsumption));
        });
    }
}
