namespace Ak.DataWorkshop.Actors;

public sealed class AddingActor : ReceiveActor
{
    private readonly ILoggingAdapter log = Context.GetLogger();

    private double total;

    public AddingActor()
    {
        this.Receive<double>(message =>
        {
            this.total += message;
            this.log.Info($"Total is {this.total}");
        });

        this.Receive<GetTotal>(_ =>
        {
            this.Sender.Tell(this.total);
        });
    }
}

public sealed class GetTotal
{
}
