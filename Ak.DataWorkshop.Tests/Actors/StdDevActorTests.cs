using Ak.DataWorkshop.Actors;
using Akka.Actor;
using Akka.TestKit.NUnit;

namespace Ak.DataWorkshop.Tests.Actors;

public class StdDevActorTests : TestKit
{

    [Test]
    public void WindowsStandardDev()
    {
        var props = StdDevActor.Props(3);
        var subject = this.Sys.ActorOf(props);

        subject.Tell(99999.0, ActorRefs.Nobody);
        subject.Tell(10.0, ActorRefs.Nobody);
        subject.Tell(5.0, ActorRefs.Nobody);
        subject.Tell(0.0, ActorRefs.Nobody);

        this.WithinAsync(TimeSpan.FromSeconds(5), async () =>
        {
            (await subject.Ask<double>(new GetStdDev())).Should().Be(5);
        });
    }
}
