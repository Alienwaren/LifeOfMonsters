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
            desiredSize = new Vector2f();
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
        private void CalculateAndSetScale(Vector2f newSize)
        {
            Vector2f oldSize = new Vector2f(this.Body.Texture.Size.X, this.Body.Texture.Size.Y);
            desiredSize = newSize;
            Vector2f scaleFactor = new Vector2f(newSize.X / oldSize.X, newSize.Y / oldSize.Y);
            this.Body.Scale = scaleFactor;
            this.Body.Origin = new Vector2f(this.Body.GetGlobalBounds().Width / 2, this.Body.GetGlobalBounds().Height / 2);
        }
        public Sprite Body { get; set; }
        public RenderWindow target { get; set; }

        private Vector2f desiredSize;

        public Vector2f size
        {
            get { return desiredSize; }
            set { CalculateAndSetScale(value); }
        }
    }
}
