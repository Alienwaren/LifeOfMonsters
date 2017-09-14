using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace Life_of_Monster.Logic
{
    public class GameClock : Entity
    {
        public GameClock(int id, string name, double tickLength)
        {
            ID = id;
            Name = name;
            GuiEventTimer = new Timer(tickLength);
            GuiEventTimer.AutoReset = true;
        }
        public Timer GuiEventTimer { get; set; }
        
    }
}
