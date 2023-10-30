using Ak.DataWorkshop.Actors;
using Akka.Actor;
using Akka.TestKit.NUnit;

namespace Ak.DataWorkshop.Tests.Actors;

public class AddingActorTests : TestKit
{


    [Test]
    public void Adding()
    {
        var subject = this.Sys.ActorOf<AddingActor>();

        subject.Tell(1.0, ActorRefs.Nobody);
        subject.Tell(3.0, ActorRefs.Nobody);

        this.WithinAsync(TimeSpan.FromSeconds(5), async () =>
        {
            (await subject.Ask<double>(new GetTotal())).Should().Be(4);
        });
    }
}
