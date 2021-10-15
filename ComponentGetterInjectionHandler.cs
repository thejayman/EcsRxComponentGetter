using EcsRx.Components.Lookups;
using EcsRx.Executor.Handlers;
using EcsRx.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EcsRx.Plugins.ComponentGetter
{
    public class ComponentGetterInjectionHandler : IConventionalSystemHandler
    {
        IComponentTypeLookup typeLookup;

        public ComponentGetterInjectionHandler(IComponentTypeLookup _typeLookup)
        {
            typeLookup = _typeLookup;
        }
        public bool CanHandleSystem(ISystem system)
        {
            return true;
        }

        public void DestroySystem(ISystem system)
        {

        }

        public void Dispose()
        {

        }

        public void SetupSystem(ISystem _system)
        {
            var _systemType = _system.GetType();
            var _fields = _systemType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(_pi =>
            {
                return _pi.FieldType.IsGenericType
                    && _pi.FieldType.GetGenericTypeDefinition() == typeof(ComponentGetter<>);
            });

            foreach (var _field in _fields)
            {
                var _fieldType = _field.FieldType;
                var _componentType = _fieldType.GenericTypeArguments[0];

                var _compIndex = this.typeLookup.GetComponentType(_componentType);

                var _compLookupType = typeof(ComponentGetter<>).MakeGenericType(_componentType);
                var _compLookupInstance = Activator.CreateInstance(_compLookupType, _compIndex);
                _field.SetValue(_system, _compLookupInstance);
            }
        }
    }
}
