﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survivor.Core
{
    internal class ItemFinder
    {
        internal ItemFinder(Arena arena)
        {
            this.arena = arena;
        }

        internal int MaxDistance
        {
            get;
            set;
        }

        internal IEnumerable<Item> FindNear(Creature creature)
        {
            return from item in arena.Items
                   where CalculateDistance(creature, item) <= MaxDistance
                   select item;
        }

        private int CalculateDistance(Creature creature, Item item)
        {
            int xDistance = Math.Abs(creature.X - item.X);
            int yDistance = Math.Abs(creature.Y - item.Y);
            return xDistance + yDistance;
        }

        private Arena arena;
    }
}
