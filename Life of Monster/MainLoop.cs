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
using Life_of_Monster.GUI;
using Life_of_Monster.Characters;
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
            clock.GuiEventTimer.Elapsed += GuiUpdate;
            clock.GuiEventTimer.Start();
            try
            {
                window = new RenderWindow(new VideoMode(800, 600), "Life of Monsters");
                window.Closed += Window_Closed;
                window.SetFramerateLimit(60);
                logger.Log(LogLevel.Trace, "Opened window");
                sceneManager.texturesManager = textureManager;
                sceneManager.TargetRenderWindow = window;
                characterManager.textureManager = textureManager;
                characterManager.target = window;
                sceneManager.characterManager = characterManager;
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Fatal, "Cannot open window because of: {0}", e.Message);
                returnCode = -2;
                return false;
            }
            if (textureManager.loadAllTextures() && characterManager.LoadAllCharactersToMemory() && sceneManager.ReadSceneFilesToMemory() && playerGuiManager.GuiSheetsToMemory() && playerGuiManager.PrepareGuiElements())
            {
                GameStateManager.ActualScene = "MainMenu";
                return true;
            }
            returnCode = -3;
            return false;
        }
        private void GuiUpdate(object sender, System.Timers.ElapsedEventArgs e)
        {
            sceneManager.DispatchActiveSceneEvents();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            logger.Log(LogLevel.Trace, "Closing window and exiting");
            window.Close();
        }

        public int Loop()
        {    
            ///
            ///DO NOT ADD CODE BEFORE THIS vvvvvvvvvvvv
            ///          
            if (Init())
            {
                returnCode = 0;
                while(window.IsOpen())
                {
                    window.DispatchEvents();
                    window.Clear();
                    if(GameStateManager.GameState == GAMESTATES.HALT)
                    {
                        Window_Closed(this, EventArgs.Empty);
                    }
                    sceneManager.DrawActiveScene();
                    window.Display();
                }
            }
            return returnCode;
        }

        private RenderWindow window;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TextureManager textureManager = new TextureManager();
        private SceneManager sceneManager = new SceneManager();
        private CharacterManager characterManager = new CharacterManager();
        private PlayerGuiManager playerGuiManager = new PlayerGuiManager();
        int returnCode = -1;
        GameClock clock = new GameClock(88, "GameClock", 100);
    }
}
