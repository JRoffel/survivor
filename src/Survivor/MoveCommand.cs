﻿using System;
using System.Linq;

namespace Survivor
{
    public class MoveCommand : Command
    {
        public MoveCommand(CreatureState creature, Direction direction)
            : base(creature)
        {
            this.direction = direction;
        }

        public override void Do(Arena arena)
        {
            int x = Creature.X;
            int y = Creature.Y;

            if (direction == Direction.Up)
            {
                y--;
            }

            if (direction == Direction.Down)
            {
                y++;
            }

            if (direction == Direction.Left)
            {
                x--;
            }

            if (direction == Direction.Right)
            {
                x++;
            }

            if (x >= 0 && x < arena.Width && y >= 0 && y < arena.Height)
            {
                Creature.X = x;
                Creature.Y = y;

                PickUpItems(arena);
            }
        }

        private void PickUpItems(Arena arena)
        {
            var healthPack = (from h in arena.HealthPacks
                              where h.X == Creature.X && h.Y == Creature.Y
                              select h).FirstOrDefault();

            if (healthPack != null)
            {
                Creature.Health += healthPack.Health;
                
                var message = String.Format(
                    "{0} picks up a health pack and receives {1} HP.",
                    Creature.Name,
                    healthPack.Health);
                arena.Log.Add(message);

                arena.HealthPacks.Remove(healthPack);

            }
        }

        private Direction direction;
    }
}
