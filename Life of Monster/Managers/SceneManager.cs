using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Life_of_Monster.Logic;
using SFML.Graphics;
using System.Text.RegularExpressions;
using Life_of_Monster.GUI;
namespace Life_of_Monster.Managers
{
    public class SceneManager
    {
        public SceneManager()
        {
            Scenes = new Dictionary<string, Scene>();
        }
        
        public bool ReadSceneFilesToMemory()
        {
            List<string> xmlSceneFilesInDirectory = new List<String>(Directory.GetFiles(baseSceneFilesPath));
            for (int i = 0; i < xmlSceneFilesInDirectory.Count; i++)
            {
                string actualFilename = xmlSceneFilesInDirectory[i];
                logger.Log(LogLevel.Trace, "Loading scene file: {0}", actualFilename);
                int dotLocation = actualFilename.IndexOf('.');
                string scenename = actualFilename.Substring(11, dotLocation-11);
                Scene tempScene = new Scene();
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(actualFilename);
                    XmlNodeList root = doc.GetElementsByTagName("scene");
                    XmlNode scene = root[0]; //we are in scene tag
                    XmlNode background = scene["background"]; //now in background tag
                    XmlNode backgroundTexture = background["texture"]; //now in background -> texture tag
                    XmlNodeList origins = ((XmlElement)backgroundTexture).GetElementsByTagName("origin");
                    XmlNodeList positions = ((XmlElement)backgroundTexture).GetElementsByTagName("position");
                    int layerCount = int.Parse(scene["layerCount"].InnerText);
                    string sceneName = scene["name"].InnerText;
                    sceneName = Regex.Replace(sceneName, @"\s+", string.Empty); ///remove whites
                    /*
                     * we are now extracting bg data
                     */
                    if (texturesManager != null)
                    {
                        string backgroundTextureName = backgroundTexture["name"].InnerText;
                        backgroundTextureName = Regex.Replace(backgroundTextureName, @"\s+", string.Empty);
                        tempScene.Background = new Sprite(texturesManager.Textures[backgroundTextureName]);
                        if (origins[0].Attributes[0].Value == "x") //first provided is x
                        {
                            string xStr = origins[0].InnerText;
                            xStr = Regex.Replace(xStr, @"\s+", string.Empty);
                            string yStr = origins[1].InnerText;
                            yStr = Regex.Replace(yStr, @"\s+", string.Empty);
                            int x = int.Parse(xStr);
                            int y = int.Parse(yStr);
                            tempScene.Background.Origin = new SFML.Window.Vector2f(x, y);

                        }
                        else //second provided is x then
                        {
                            string xStr = origins[1].InnerText;
                            xStr = Regex.Replace(xStr, @"\s+", string.Empty);
                            string yStr = origins[0].InnerText;
                            yStr = Regex.Replace(yStr, @"\s+", string.Empty);
                            int x = int.Parse(xStr);
                            int y = int.Parse(yStr);
                            tempScene.Background.Origin = new SFML.Window.Vector2f(x, y);
                        }
                        if (positions[0].Attributes[0].Value == "x") //first provided is x
                        {
                            string xStr = positions[0].InnerText;
                            xStr = Regex.Replace(xStr, @"\s+", string.Empty);
                            string yStr = positions[1].InnerText;
                            yStr = Regex.Replace(yStr, @"\s+", string.Empty);
                            int x = int.Parse(xStr);
                            int y = int.Parse(yStr);
                            tempScene.Background.Position = new SFML.Window.Vector2f(x, y);

                        }
                        else //second provided is x then
                        {
                            string xStr = positions[1].InnerText;
                            xStr = Regex.Replace(xStr, @"\s+", string.Empty);
                            string yStr = positions[0].InnerText;
                            yStr = Regex.Replace(yStr, @"\s+", string.Empty);
                            int x = int.Parse(xStr);
                            int y = int.Parse(yStr);
                            tempScene.Background.Position = new SFML.Window.Vector2f(x, y);
                        }
                       

                    }
                    for (int j = 0;  j < layerCount;  j++)
                    {
                        XmlNodeList layers = doc.GetElementsByTagName("layer");
                        XmlNode layer = layers[j];
                        XmlNode layerTexture = layer["texture"];
                        string layerTextureName = layerTexture["name"].InnerText;
                        layerTextureName = Regex.Replace(layerTextureName, @"\s+", string.Empty);
                        XmlNodeList layerType = ((XmlElement)layer).GetElementsByTagName("type");
                        string layerTypeStr = layerType[0].InnerText;
                        layerTypeStr = Regex.Replace(layerTypeStr, @"\s+", string.Empty);
                        if (layerTypeStr == "Image")
                        {
                            Sprite tempomarySprite = new Sprite(texturesManager.Textures[layerTextureName]);
                            tempScene.Layers.Add(tempomarySprite);
                        }
                        else if (layerTypeStr == "TextButton")
                        {
                            TextButton tempButton = new TextButton();
                            tempButton.buttonText.Texture = texturesManager.Textures[layerTextureName];
                            tempScene.Layers.Add(tempButton);
                        }

                        XmlNodeList layerOrigins = ((XmlElement)layerTexture).GetElementsByTagName("origin");
                        XmlNodeList layerPositions = ((XmlElement)layerTexture).GetElementsByTagName("position");
                        if (layerOrigins[0].Attributes[0].Value == "x") //first provided is x
                        {
                            string xStr = layerOrigins[0].InnerText;
                            xStr = Regex.Replace(xStr, @"\s+", string.Empty);
                            string yStr = layerOrigins[1].InnerText;
                            yStr = Regex.Replace(yStr, @"\s+", string.Empty);
                            int x = int.Parse(xStr);
                            int y = int.Parse(yStr);
                            object tempObj = tempScene.Layers[j];
                            if(tempObj is Sprite)
                            {
                                Sprite temp = tempObj as Sprite;
                                if (temp != null)
                                {
                                    temp.Origin = new SFML.Window.Vector2f(x, y);
                                }
                                tempScene.Layers[j] = temp;

                            }else if(tempObj is TextButton)
                            {
                                TextButton temp = tempObj as TextButton;
                                if (temp != null)
                                {
                                    temp.buttonText.Origin = new SFML.Window.Vector2f(x, y);
                                }
                                tempScene.Layers[j] = temp;
                            }
                        }
                        else //second provided is x then
                        {
                            string xStr = layerPositions[1].InnerText;
                            xStr = Regex.Replace(xStr, @"\s+", string.Empty);
                            string yStr = layerPositions[0].InnerText;
                            yStr = Regex.Replace(yStr, @"\s+", string.Empty);
                            int x = int.Parse(xStr);
                            int y = int.Parse(yStr);
                            object tempObj = tempScene.Layers[j];
                            if (tempObj is Sprite)
                            {
                                Sprite temp = tempObj as Sprite;
                                if (temp != null)
                                {
                                    temp.Origin = new SFML.Window.Vector2f(x, y);
                                }
                                tempScene.Layers[j] = temp;

                            }
                            else if (tempObj is TextButton)
                            {
                                TextButton temp = tempObj as TextButton;
                                if (temp != null)
                                {
                                    temp.buttonText.Origin = new SFML.Window.Vector2f(x, y);
                                }
                                tempScene.Layers[j] = temp;
                            }
                        }
                        if (layerPositions[0].Attributes[0].Value == "x") //first provided is x
                        {
                            string xStr = layerPositions[0].InnerText;
                            xStr = Regex.Replace(xStr, @"\s+", string.Empty);
                            string yStr = layerPositions[1].InnerText;
                            yStr = Regex.Replace(yStr, @"\s+", string.Empty);
                            int x = int.Parse(xStr);
                            int y = int.Parse(yStr);
                            object tempObj = tempScene.Layers[j];
                            if (tempObj is Sprite)
                            {
                                Sprite temp = tempObj as Sprite;
                                if (temp != null)
                                {
                                    temp.Position = new SFML.Window.Vector2f(x, y);
                                }
                                tempScene.Layers[j] = temp;

                            }
                            else if (tempObj is TextButton)
                            {
                                TextButton temp = tempObj as TextButton;
                                temp.Name = layerTextureName;
                                if (temp != null)
                                {
                                    temp.buttonText.Position = new SFML.Window.Vector2f(x, y);
                                }
                                tempScene.Layers[j] = temp;
                            }

                        }
                        else //second provided is x then
                        {
                            string xStr = layerPositions[1].InnerText;
                            xStr = Regex.Replace(xStr, @"\s+", string.Empty);
                            string yStr = layerPositions[0].InnerText;
                            yStr = Regex.Replace(yStr, @"\s+", string.Empty);
                            int x = int.Parse(xStr);
                            int y = int.Parse(yStr);
                            object tempObj = tempScene.Layers[j];
                            if (tempObj is Sprite)
                            {
                                Sprite temp = tempObj as Sprite;
                                if (temp != null)
                                {
                                    temp.Position = new SFML.Window.Vector2f(x, y);
                                }
                                tempScene.Layers[j] = temp;

                            }
                            else if (tempObj is TextButton)
                            {
                                TextButton temp = tempObj as TextButton;
                                temp.Name = layerTextureName;
                                if (temp != null)
                                {
                                    temp.buttonText.Position = new SFML.Window.Vector2f(x, y);
                                }
                                tempScene.Layers[j] = temp;
                            }
                        }
                    }
                    tempScene.Target = TargetRenderWindow;
                    tempScene.InitEvents();
                    
                    Scenes.Add(sceneName, tempScene);
                    
                }
                catch (Exception e)
                {
                    logger.Log(LogLevel.Fatal, "Cannot load scene file: {0}, because of: {1}", actualFilename, e.Message);
                    return false;
                } 
            }
           
            return true;
        }
        public void DrawActiveScene()
        {
            if (!String.IsNullOrEmpty(GameStateManager.ActualScene))
            {
                Scene scene;
                Scenes.TryGetValue(GameStateManager.ActualScene, out scene);
                if (scene != null)
                {
                    scene.DrawScene();
                }
            }
        }
        public void DispatchActiveSceneEvents()
        {
            if (!String.IsNullOrEmpty(GameStateManager.ActualScene))
            {
                Scene scene;
                Scenes.TryGetValue(GameStateManager.ActualScene, out scene);
                if (scene != null)
                {
                    scene.DispatchSceneEvents();
                } 
            }
        }
        private const string baseSceneFilesPath = "SceneFiles";
        public TextureManager texturesManager{ private get; set; }
        public Dictionary<string, Logic.Scene> Scenes { get; private set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public RenderWindow TargetRenderWindow { get; set; }
    }
}
