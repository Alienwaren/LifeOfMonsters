using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Life_of_Monster.Logic;
using SFML.Graphics;
using SFML.Window;
namespace Life_of_Monster.GUI
{
    public class TextButton : Entity
    {
        public TextButton()
        {
            WasClicked = false;
            buttonText = new Sprite();
        }
        public event EventHandler ButtonClicked;
        protected virtual void OnButtonClicked(EventArgs e)
        {
            if(GameRenderWindow != null && buttonText != null)
            {
                EventHandler handler = ButtonClicked;
                handler(this, e);

            }
        }
        public void GetMousePos()
        {
            Vector2i mousePos = Mouse.GetPosition(GameRenderWindow);
            FloatRect buttonRect = buttonText.GetGlobalBounds();
            if (buttonRect.Contains(mousePos.X, mousePos.Y))
            {
                if(Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    if(!WasClicked)
                    {
                        OnButtonClicked(new EventArgs());
                    }
                    WasClicked = true;
                }else
                {
                    WasClicked = false;
                }
            }
        }
        public void DrawButton()
        {
            if(GameRenderWindow != null)
            {
                GameRenderWindow.Draw(buttonText);
            }
        }
        public Sprite buttonText { get; set; }
        public RenderWindow GameRenderWindow{ get; set; }
        public bool WasClicked { get; set; }
        
    }
}
