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
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Fatal, "Cannot open window");
                returnCode = -2;
                return false;
            }
            if(textureManager.loadAllTextures() && sceneManager.ReadSceneFilesToMemory())
            {
                return true;
            }
            returnCode = -3;
            return false;
        }
        private void GuiUpdate(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(btn != null)
            {
                btn.GetMousePos();
            }
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
                Scene mainMenu = sceneManager.Scenes["MainMenu"];
                //btn = new TextButton
                //{
                //    GameRenderWindow = window,
                //    buttonText = new Sprite(textureManager.Textures["Options"])
                //};
                //btn.ButtonClicked += Btn_ButtonClicked;
                //btn.buttonText.Position = new Vector2f(300, 100);
                //btn.buttonText.Origin = new Vector2f(btn.buttonText.GetGlobalBounds().Height / 2, btn.buttonText.GetGlobalBounds().Width / 2);
                returnCode = 0;
                while(window.IsOpen())
                {
                    window.DispatchEvents();
                    window.Clear();
                    if (mainMenu != null)
                    {
                        mainMenu.DrawScene();
                    }
                    //  btn.DrawButton();
                    window.Display();
                }
            }
            return returnCode;
        }

        private void Btn_ButtonClicked(object sender, EventArgs e)
        {
            TextButton EventBtn = sender as TextButton;
            logger.Info("test");
        }

        private RenderWindow window;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TextureManager textureManager = new TextureManager();
        private SceneManager sceneManager = new SceneManager();
        int returnCode = -1;
        GameClock clock = new GameClock(88, "GameClock", 100);
        TextButton btn;
    }
}
