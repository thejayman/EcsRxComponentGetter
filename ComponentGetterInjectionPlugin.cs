using EcsRx.Executor.Handlers;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Plugins;
using EcsRx.Systems;
using System;
using System.Collections.Generic;
using System.Text;

using static EcsRx.Infrastructure.Extensions.IDependencyContainerExtensions;

namespace EcsRx.Plugins.ComponentGetter
{
    public class ComponentGetterInjectionPlugin : IEcsRxPlugin
    {
        public string Name => "Component Getter Injector";

        public Version Version => new Version("1.0.0");

        public IEnumerable<ISystem> GetSystemsForRegistration(IDependencyContainer _container)
            => new ISystem[0];

        public void SetupDependencies(IDependencyContainer _container)
        {
            _container.Bind<IConventionalSystemHandler, ComponentGetterInjectionHandler>();
        }
    }
}
