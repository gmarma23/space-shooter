using SpaceShooter.core;
using SpaceShooter.gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    internal static class GameClient
    {
        private static GameFrame gameFrame;
        private static Gameplay game;

        public static void StartGame()
        {
            gameFrame = new GameFrame();
            game = new Gameplay();
        }
    }
}
