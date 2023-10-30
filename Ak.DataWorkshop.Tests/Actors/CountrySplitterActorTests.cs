using Ak.DataWorkshop.Actors;
using Ak.DataWorkshop.Repositories;
using Akka.Actor;
using Akka.TestKit.NUnit;

namespace Ak.DataWorkshop.Tests.Actors;

public class CountrySplitterActorTests : TestKit
{

    [Test]
    public void CountrySplitting()
    {
        var unitedKingdomProbe = this.CreateTestProbe();
        var unitedStatesTestProbe = this.CreateTestProbe();


        var countrySplitterActorProps = CountrySplitterActor.Props(unitedKingdomProbe.Ref, unitedStatesTestProbe.Ref);
        var subject = this.Sys.ActorOf(countrySplitterActorProps);

        this.Within(TimeSpan.FromSeconds(3), () =>
        {
            subject.Tell(new EnergyDataRecord { IsoCode = ThreeCharacterIsoCode.Gbr, RenewablesConsumption = 1 }, ActorRefs.NoSender);

            subject.Tell(new EnergyDataRecord { IsoCode = ThreeCharacterIsoCode.Gbr, RenewablesConsumption = 3 }, ActorRefs.NoSender);
            subject.Tell(new EnergyDataRecord { IsoCode = ThreeCharacterIsoCode.Usa, RenewablesConsumption = 3 }, ActorRefs.NoSender);

            unitedKingdomProbe.ExpectMsgAllOf(new EnergyDataRecord { IsoCode = ThreeCharacterIsoCode.Gbr, RenewablesConsumption = 1 },
                new EnergyDataRecord { IsoCode = ThreeCharacterIsoCode.Gbr, RenewablesConsumption = 3 });
            unitedStatesTestProbe.ExpectMsg(new EnergyDataRecord { IsoCode = ThreeCharacterIsoCode.Usa, RenewablesConsumption = 3 });
        });
    }
}
