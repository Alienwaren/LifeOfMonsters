using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using NLog;
using Life_of_Monster.Logic;
using Life_of_Monster.Managers;
using System.IO;
namespace Life_of_Monster
{
    public class MainLoop
    {
        public MainLoop()
        {
            textureManager.BasePathForImages = "IMG";
        }
      
        private bool Init()
        {
            try
            {
                window = new RenderWindow(new VideoMode(800, 600), "Life of Monsters");
                window.Closed += Window_Closed;
                window.SetFramerateLimit(60);
                logger.Log(LogLevel.Trace, "Opened window");
                sceneManager.texturesManager = textureManager;
                sceneManager.TargetRenderWindow = window;

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Fatal, "Cannot open window");
                return false;
            }
            if(textureManager.loadAllTextures() && sceneManager.ReadSceneFilesToMemory())
            {
                return true;
            }
            return false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            logger.Log(LogLevel.Trace, "Closing window and exiting");
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
                    sceneManager.DrawScene("MainMenu");
                    window.Display();
                }
            }
            return returnCode;
        }
        private RenderWindow window;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TextureManager textureManager = new TextureManager();
        private SceneManager sceneManager = new SceneManager();
    }
}
