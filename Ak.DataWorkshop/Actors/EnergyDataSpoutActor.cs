namespace Ak.DataWorkshop.Actors;

using Repositories;

public sealed class EnergyDataSpoutActor : ReceiveActor
{
    private readonly ILoggingAdapter log = Context.GetLogger();

    public EnergyDataSpoutActor(IActorRef destination, IEnergyDataRepository repository)
    {
        this.Receive<Read>(_ =>
        {
            var energyDataRecords = repository.ListAll();
            foreach (var record in energyDataRecords)
            {
                this.log.Info($"Sending {record}");
                destination.Tell(record, ActorRefs.Nobody);
            }
        });
    }

    public static Props Props(IActorRef actorRef, IEnergyDataRepository repo)
    {
        return Akka.Actor.Props.Create(() => new EnergyDataSpoutActor(actorRef, repo));
    }

    protected override void PreStart()
    {
        this.Self.Tell(new Read());
    }
}

public sealed class Read
{
}
