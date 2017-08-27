using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
namespace Life_of_Monster.Managers
{
    public class SceneManager
    {
        public SceneManager()
        {
            
        }
        public bool ReadSceneFilesToMemory()
        {
            List<string> directoriesInSceneFolder = new List<String>(Directory.GetFiles(baseSceneFilesPath));
            for (int i = 0; i < directoriesInSceneFolder.Count; i++)
            {
                string actualFilename = directoriesInSceneFolder[i];
                logger.Log(LogLevel.Trace, "Loading scene file: {0}", actualFilename);
                int dotLocation = actualFilename.IndexOf('.');
                string scenename = actualFilename.Substring(11, dotLocation-11);
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(actualFilename);
                    XmlNodeList root = doc.GetElementsByTagName("scene");
                }
                catch (Exception e)
                {
                    logger.Log(LogLevel.Fatal, "Cannot load scene file: {0}, because of: {1}", actualFilename, e.Message);
                    return false;
                } 
            }
            return true;

            

        }
        private const string baseSceneFilesPath = "SceneFiles";
        public TextureManager texturesManager{ private get; set; }
        public Dictionary<string, Logic.Scene> Scenes { get; private set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();

    }
}
