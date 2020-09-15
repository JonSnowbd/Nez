﻿using DefaultEcs;
using DefaultEcs.System;

namespace Nez
{
    /// <summary>
    /// Use this when the update system does not require a specific set of entities, but instead runs logic
    /// on static state or manages its own <c>EntitySet</c>
    /// </summary>
    public class StaticUpdateSystem : ISystem<GiaScene>
    {
        protected World CurrentWorld;

        bool _isEnabled = true;
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        public StaticUpdateSystem()
        {
            CurrentWorld = Gia.Current.World;
        }
        public StaticUpdateSystem(World world)
        {
            CurrentWorld = world;
        }

        public virtual void Update(GiaScene state)
        {
        }

        public void Dispose()
        {
        }
    }
}
