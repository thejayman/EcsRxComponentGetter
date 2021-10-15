using EcsRx.Components;
using EcsRx.Components.Database;
using EcsRx.Entities;
using EcsRx.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EcsRx.Plugins.ComponentGetter
{
    public class ComponentGetter<T>
        where T : class, IComponent
    {

        public int ComponentId { get; }

        public ComponentGetter(int _index) => ComponentId = _index;

        public bool TryGet(IEntity _entity, out T _comp)
        {
            if (Has(_entity))
            {
                _comp = Get(_entity);
                return true;
            }
            else
            {
                _comp = null;
                return false;
            }
        }

        public bool Has(IEntity _entity)
        {
            return _entity.HasComponent(ComponentId);
        }

        public T Get(IEntity _entity)
        {
            return _entity.GetComponent<T>(ComponentId);
        }

        public void Remove(IEntity _entity)
        {
            _entity.RemoveComponents(ComponentId);
        }
    }

    static public class ComponentGetterExtensions
    {
        static public ref T New<T>(this ComponentGetter<T> _getter, IEntity _entity)
            where T : class, IComponent, new()
        {
            return ref _entity.AddComponent<T>(_getter.ComponentId);
        }
    }
}
