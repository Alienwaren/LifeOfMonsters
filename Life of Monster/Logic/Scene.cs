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
            Layers = new List<Sprite>();
        }
        void DrawScene()
        {
            if(Target != null)
            {
                Sprite bg = new Sprite(Background);
                Target.Draw(bg);
                for (int i = 0; i < Layers.Count; i++)
                {
                    Target.Draw(Layers[i]);
                }
            }
        }
        public RenderWindow Target { get; set; }
        public Sprite Background { get; set; }
        public List<Sprite> Layers { get; set; }
    }
}
