namespace Ak.DataWorkshop.Actors;

public sealed class StdDevActor : ReceiveActor
{
    private readonly ILoggingAdapter log = Context.GetLogger();
    private List<double> messages = new List<double>();
    private double stdDev;

    public StdDevActor(int window)
    {
        this.Receive<double>(message =>
        {
            this.messages.Add(message);
            if (this.messages.Count > window)
            {
                this.messages.RemoveAt(0);
            }

            var average = this.messages.Average();
            var sum = this.messages.Sum(d => Math.Pow(d - average, 2));
            this.stdDev = Math.Sqrt(sum / this.messages.Count);
            this.log.Info($"StdDev is {this.stdDev}");
        });

        this.Receive<GetStdDev>(_ =>
        {
            this.Sender.Tell(this.stdDev);
        });
    }

    public static Props Props(int window)
    {
        return Akka.Actor.Props.Create(() => new StdDevActor(window));
    }
}

public sealed class GetStdDev
{
}
