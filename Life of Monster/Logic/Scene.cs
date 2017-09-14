using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace Life_of_Monster.Logic
{
    public class Scene : Entity
    {
        public Scene()
        {
            Background = new Sprite();
            Layers = new List<object>();
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
                    }
                }
            }
        }
        public RenderWindow Target { get; set; }
        public Sprite Background { get; set; }
        public List<object> Layers { get; set; }
    }
}
