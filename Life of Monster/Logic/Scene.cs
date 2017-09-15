using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Life_of_Monster.GUI;
using Life_of_Monster.Logic;
using Life_of_Monster.Managers;
namespace Life_of_Monster.Logic
{
    public class Scene : Entity
    {
        public Scene()
        {
            Background = new Sprite();
            Layers = new List<object>();
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
    }
}
