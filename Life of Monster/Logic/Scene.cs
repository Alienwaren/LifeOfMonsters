using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Life_of_Monster.GUI;
using Life_of_Monster.Logic;
using Life_of_Monster.Characters;
using Life_of_Monster.Managers;
namespace Life_of_Monster.Logic
{
    public class Scene : Entity
    {
        public Scene()
        {
            Background = new Sprite();
            Layers = new List<object>();
            DisplayPlayerControls = false;
        }
        public void InitEvents()
        {
            foreach (var item in Layers)
            {
                if (item is TextButton)
                {
                    TextButton temp = item as TextButton;
                    temp.ButtonClicked += ButtonClicked;
                    temp.GameRenderWindow = Target;
                }
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            if(sender is TextButton)
            {
                TextButton temp = sender as TextButton;
                if(temp.Name == "ExitToWindows")
                {
                    GameStateManager.GameState = GAMESTATES.HALT;
                }else if(temp.Name == "StartGame")
                {
                    GameStateManager.ActualScene = "TestMeeting";
                }
            }
        }

        public void DrawScene()
        {
            if(Target != null)
            {
                Sprite bg = new Sprite(Background);
                Target.Draw(bg);
                for (int i = 0; i < Layers.Count; i++)
                {
                    if(Layers[i] is Sprite)
                    {
                        Sprite tempObj = Layers[i] as Sprite;
                        if(tempObj != null)
                        {
                            Target.Draw(tempObj);
                        }
                    }else if(Layers[i] is TextButton)
                    {
                        TextButton tempObj = Layers[i] as TextButton;
                        if(tempObj != null)
                        {
                            Target.Draw(tempObj.buttonText);
                        }
                    }else if(Layers[i] is Character)
                    {
                        Character tempCharacter = Layers[i] as Character;
                        if (tempCharacter != null)
                        {
                            Target.Draw(tempCharacter.Body);
                        }
                    }
                }
            }
        }
        public void DispatchSceneEvents()
        {
            foreach (var item in Layers)
            {
                if(item is TextButton)
                {
                    TextButton temp = item as TextButton;
                    temp.GetMousePos();
                }

            }
        }
        public RenderWindow Target { get; set; }
        public Sprite Background { get; set; }
        public List<object> Layers { get; set; }
        public bool DisplayPlayerControls { get; set; }
    }
}
