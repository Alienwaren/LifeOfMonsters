using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
namespace Life_of_Monster.Logic
{
    public class Renderable : Entity
    {
        public Renderable(int id, string name = "")
            : base()
        {
            Body = new Sprite();
            this.ID = id;
            this.Name = name;
        }
        public void Draw()
        {
            target.Draw(Body);
        }
        public void Draw(Vector2f pos)
        {
            Body.Position = pos;
            target.Draw(Body);
        }
        public Sprite Body { get; set; }
        public RenderWindow target { get; set; }
    }
}
