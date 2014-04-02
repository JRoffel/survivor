﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Survivor.Core
{
    internal class Spawner<T> where T : Item, new()
    {
        internal Spawner(ItemType itemType)
        {
            this.itemType = itemType;
        }

        internal int MaxItemCount
        {
            get;
            set;
        }

        internal int MinStrength
        {
            get;
            set;
        }

        internal int MaxStrength
        {
            get;
            set;
        }

        internal void Spawn(Arena arena)
        {
            var items = from i in arena.Items
                        where i.Type == itemType
                        select i;

            if (items.Count() < MaxItemCount)
            {
                int x, y;

                do
                {
                    x = random.Next(arena.Width);
                    y = random.Next(arena.Height);
                } while (arena.IsOccupied(x, y) || arena.IsCloseToCreature(x, y));

                var item = new T()
                {
                    Type = itemType,
                    X = x,
                    Y = y,
                    Arena = arena
                };

                arena.InternalItems.Add(item);
            }
        }

        private Random random = new Random();
        private ItemType itemType;
    }
}
