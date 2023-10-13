namespace Ak.DataWorkshop.Actors;

using System.Threading.Channels;

public sealed class AddingActor
{
    private readonly ChannelReader<object> input;
    private readonly ChannelWriter<double> actorRef;

    public AddingActor(ChannelReader<object> input, ChannelWriter<double> actorRef)
    {
        this.input = input;
        this.actorRef = actorRef;
    }

    public async Task Start()
    {
        while (true)
        {
            var message = await this.input.ReadAsync();

            if (message.Equals("stop"))
            {
                return;
            }

            Console.WriteLine("Hello, world!");
        }
    }
}
