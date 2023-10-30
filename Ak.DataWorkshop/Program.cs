#pragma warning disable SA1200
using Ak.DataWorkshop.Actors;
using Ak.DataWorkshop.Repositories;
using Akka.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#pragma warning restore SA1200

var hostBuilder = new HostBuilder();

hostBuilder.ConfigureServices(static (_, services) =>
{
    services.AddSingleton(new EnergyDataRepository());
    services.AddAkka("MyActorSystem", static (builder, _) =>
    {
        builder
            .WithActors(static (system, registry, resolver) =>
            {
                var addingActorProps = resolver.Props<AddingActor>();
                var addingActor = system.ActorOf(addingActorProps, "adding-actor");

                addingActor.Tell(0.5);
                addingActor.Tell(0.1);
                addingActor.Tell(0.4);
                addingActor.Tell(10.0);
                registry.Register<AddingActor>(addingActor);
            });
    });
});

var host = hostBuilder.Build();

await host.RunAsync().ConfigureAwait(true);
