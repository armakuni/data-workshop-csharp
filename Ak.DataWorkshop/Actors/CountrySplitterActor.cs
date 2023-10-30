namespace Ak.DataWorkshop.Actors;

using Repositories;

public sealed class CountrySplitterActor : ReceiveActor
{
    public CountrySplitterActor(IActorRef unitedKingdom, IActorRef unitedStates)
    {
        this.Receive<EnergyDataRecord>(message =>
        {
            switch (message)
            {
#pragma warning disable SA1013 // This seems to be buggy, conflicts with not having a space in a case statement after the colon
                case { IsoCode: ThreeCharacterIsoCode.Gbr }:
#pragma warning restore SA1013 // This seems to be buggy, conflicts with not having a space in a case statement after the colon
                    unitedKingdom.Tell(message);
                    break;
#pragma warning disable SA1013 // This seems to be buggy, conflicts with not having a space in a case statement after the colon
                case { IsoCode: ThreeCharacterIsoCode.Usa }:
#pragma warning restore SA1013 // This seems to be buggy, conflicts with not having a space in a case statement after the colon
                    unitedStates.Tell(message);
                    break;
            }
        });
    }

    public static Props Props(IActorRef greatBritain, IActorRef usa)
    {
        return Akka.Actor.Props.Create(() => new CountrySplitterActor(greatBritain, usa));
    }
}
