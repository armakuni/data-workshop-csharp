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
                var averagingActorProps = resolver.Props<AveragingActor>();
                var averagingActor = system.ActorOf(averagingActorProps, "averaging-actor");

                var ukRenewablesConsumptionActorProps = GetRenewableConsumptionActor.Props(averagingActor);
                var ukRenewablesConsumptionActor = system.ActorOf(ukRenewablesConsumptionActorProps, "uk-renewable-consumption");

                var usaRenewablesConsumptionActorProps = GetRenewableConsumptionActor.Props(averagingActor);
                var usaRenewablesConsumptionActor = system.ActorOf(usaRenewablesConsumptionActorProps, "usa-renewable-consumption");

                var countrySplitterActorProps = resolver.Props<CountrySplitterActor>(ukRenewablesConsumptionActor, usaRenewablesConsumptionActor);
                var countrySplitterActor = system.ActorOf(countrySplitterActorProps, "country-splitter");

                var repo = resolver.GetService<EnergyDataRepository>();
                var energySpoutActorProps = EnergyDataSpoutActor.Props(countrySplitterActor, repo);
                var energySpoutActor = system.ActorOf(energySpoutActorProps, "spout");

                var addingActorProps = resolver.Props<AddingActor>();
                var addingActor = system.ActorOf(addingActorProps, "adding-actor");
                registry.Register<AddingActor>(addingActor);
                registry.Register<EnergyDataSpoutActor>(energySpoutActor);
                registry.Register<CountrySplitterActor>(countrySplitterActor);
            });
    });
});

var host = hostBuilder.Build();

await host.RunAsync().ConfigureAwait(true);
