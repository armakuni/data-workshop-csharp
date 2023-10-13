namespace Ak.DataWorkshop.Tests.Actors;

using Ak.DataWorkshop.Actors;
using System.Threading.Channels;

public class AddingActorTests
{
    [Test]
    public async Task Adding()
    {
        var inbox = Channel.CreateUnbounded<object>();
        var actorRef = Channel.CreateUnbounded<double>();

        var subject = new AddingActor(inbox.Reader, actorRef.Writer);
        var actorTask = Task.Factory.StartNew(async () => await subject.Start());

        await inbox.Writer.WriteAsync(123);
        await inbox.Writer.WriteAsync(333);
        await inbox.Writer.WriteAsync("stop");

        await actorTask;

        (await actorRef.Reader.ReadAsync()).Should().Be(123);
        (await actorRef.Reader.ReadAsync()).Should().Be(333);
    }
}
