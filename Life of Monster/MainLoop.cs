using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using NLog;
namespace Life_of_Monster
{
    public class MainLoop
    {
        public MainLoop()
        {
            
        }
        private bool Init()
        {
            try
            {
                window = new RenderWindow(new VideoMode(800, 600), "Life of Monsters");
                window.Closed += Window_Closed;
                logger.Log(LogLevel.Trace, "Opened window");

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Fatal, "Cannot open window");
                return false;
            }
            return true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            logger.Log(LogLevel.Trace, "Closing window..");
            window.Close();
        }

        public int Loop()
        {
            int returnCode = -1;
            if (Init())
            {
                returnCode = 0;
                while(window.IsOpen())
                {
                    window.DispatchEvents();
                    window.Clear();
                    window.Display();
                }
            }
            return returnCode;
        }
        private RenderWindow window;
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
