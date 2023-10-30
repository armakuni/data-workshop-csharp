namespace Ak.DataWorkshop.Actors;

public sealed class AveragingActor : ReceiveActor
{
    private readonly ILoggingAdapter log = Context.GetLogger();
    private double average;
    private int count;
    private double sum;

    public AveragingActor()
    {
        this.Receive<double>(message =>
        {
            this.count += 1;
            this.sum += message;
            this.average = this.sum / this.count;
            this.log.Info($"Average is {this.average}");
        });

        this.Receive<GetAverage>(_ =>
        {
            this.Sender.Tell(this.average);
        });
    }

    public static Props Props()
    {
        return Akka.Actor.Props.Create<AveragingActor>();
    }
}

public sealed class GetAverage
{
}
