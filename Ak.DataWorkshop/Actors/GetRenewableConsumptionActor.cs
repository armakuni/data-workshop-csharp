namespace Ak.DataWorkshop.Actors;

using Repositories;

public class GetRenewableConsumptionActor : ReceiveActor
{
    public GetRenewableConsumptionActor(IActorRef actorRef)
    {
        this.Receive<EnergyDataRecord>(message =>
        {
            actorRef.Tell(message.RenewablesConsumption);
        });
    }

    public static Props Props(IActorRef probeRef)
    {
        return Akka.Actor.Props.Create(() => new GetRenewableConsumptionActor(probeRef));
    }
}
