using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_of_Monster.Managers
{
    public class TextureManager
    {
        public TextureManager()
        {
            Textures = new Dictionary<string, Texture>();
        }
        public TextureManager(string basePath)
        {
            Textures = new Dictionary<string, Texture>();
            BasePathForImages = basePath;
        }
        public string BasePathForImages { get; set; }
        public Dictionary<string, Texture> Textures { get; set; }
    }
}
