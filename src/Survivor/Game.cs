﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Survivor
{
    public class Game
    {
        public Game()
        {
            arena = new Arena(80, 25);
            arena.Creatures.Add(new TestCreature(10, 2));
            arena.Creatures.Add(new TestCreature(4, 5));

            healthSpawner = new Spawner();
            healthSpawner.MaxItemCount = 10;
            healthSpawner.MinStrength = 5;
            healthSpawner.MaxStrength = 5;

            weaponSpawner = new Spawner();
            weaponSpawner.MaxItemCount = 4;
            weaponSpawner.MinStrength = 2;
            weaponSpawner.MaxStrength = 7;
        }

        public int FramesPerSecond
        {
            get;
            set;
        }

        public void Run()
        {
            int fps = FramesPerSecond > 0 ? FramesPerSecond : 1;
            int delay = (int) (1000.0 / fps);
            
            var renderer = new Renderer();
            renderer.UpdateConsoleSize(arena);

            while (true)
            {
                UpdateCreatures();
                SpawnItems();

                renderer.Draw(arena);

                Thread.Sleep(delay);
            }
        }

        private void SpawnItems()
        {
            healthSpawner.Spawn(arena, arena.HealthPacks);
            weaponSpawner.Spawn(arena, arena.Weapons);
        }

        private void UpdateCreatures()
        {
            foreach (var creature in arena.Creatures)
            {
                if (!creature.HasCommands)
                {
                    creature.Update();
                }

                var command = creature.NextCommand();
                command.Do(arena);
            }
        }

        private Arena arena;
        private Spawner healthSpawner;
        private Spawner weaponSpawner;
    }
}
