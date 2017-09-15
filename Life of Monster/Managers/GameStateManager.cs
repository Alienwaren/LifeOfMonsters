using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_of_Monster.Managers
{
    public static class GameStateManager
    {
        static GameStateManager()
        {
            ActualScene = "MainMenu";
        }
        public static string ActualScene { get; set; }

        public static GAMESTATES GameState { get; set; }
    }
    public enum GAMESTATES
    {
        RUNNING = 1,
        HALT = -1
    }
}
