using NLog;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        public bool loadAllTextures()
        {
            try
            {
                List<string> files = new List<string>(Directory.GetFiles(this.BasePathForImages));
                for (int i = 0; i < files.Count; i++)
                {
                    string actualPath = files[i];
                    int dotLocation = actualPath.IndexOf('.');
                    string ImgName = actualPath.Substring(4, dotLocation - 4); ///four because directory wont change also all textures will be in png
                    Texture tempTexture = new Texture(actualPath);
                    Textures.Add(ImgName, tempTexture);
                    logger.Log(LogLevel.Trace, "Reading and adding image to Manager: {0}, name: {1}", actualPath, ImgName);
                }

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Fatal, "Cannot read image, exiting because: {0}", e.Message);
                return false;
            }
            return true;
        }
        public string BasePathForImages { get; set; }
        public Dictionary<string, Texture> Textures { get; set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();

    }
}
