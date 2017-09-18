using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Life_of_Monster.Characters;
using System.Xml;
using System.IO;
using Life_of_Monster.Logic;
using SFML.Graphics;
using System.Text.RegularExpressions;
using NLog;
using SFML.Window;

namespace Life_of_Monster.Managers
{
    public class CharacterManager
    {
        public CharacterManager()
        {
            GameCharacters = new Dictionary<string, Character>();
        }
        public bool LoadAllCharactersToMemory()
        {
           if(textureManager != null)
           {
                List<string> characterFilesInDirectory = Directory.GetFiles(BasePathForCharacters).ToList();
                for (int i = 0; i < characterFilesInDirectory.Count; i++)
                {
                    string actualFilename = characterFilesInDirectory[i];
                    logger.Log(LogLevel.Trace, "Loading character file: {0}", actualFilename);
                    int dotLocation = actualFilename.IndexOf('.');
                    string characterName = actualFilename.Substring(15, dotLocation - 15);
                    Character tempCharacter = new Character();
                    try
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(actualFilename);
                        XmlElement characterRoot = document["character"];
                        string characterNameStr = Regex.Replace(characterRoot["name"].InnerText, @"\s+", string.Empty);
                        string characterSurnameStr = Regex.Replace(characterRoot["surname"].InnerText, @"\s+", string.Empty);
                        string characterTextureNameStr = Regex.Replace(characterRoot["texture"].InnerText, @"\s+", string.Empty);
                        string affectionStr = characterRoot["affection"].InnerText;
                        int affection = 0;
                        int.TryParse(affectionStr, out affection);
                        string speciesStr = Regex.Replace(characterRoot["species"].InnerText, @"\s+", string.Empty);
                        XmlNodeList sizes = characterRoot["size"].ChildNodes;    
                        Vector2f tempVector = new Vector2f();
                        string xStr = Regex.Replace(sizes[0].InnerText, @"\s+", string.Empty);
                        string yStr = Regex.Replace(sizes[1].InnerText, @"\s+", string.Empty);
                        float.TryParse(xStr, out tempVector.X);
                        float.TryParse(yStr, out tempVector.Y);
                        tempCharacter.Body = new Sprite(textureManager.Textures[characterTextureNameStr]);
                        tempCharacter.CharacterAffection = affection;
                        tempCharacter.CharacterName = characterNameStr;
                        tempCharacter.CharacterSurname = characterSurnameStr;
                        tempCharacter.target = this.target;
                        tempCharacter.size = tempVector;
                        GameCharacters.Add(characterNameStr, tempCharacter);
                        
                    }
                    catch (Exception e)
                    {
                        logger.Log(LogLevel.Fatal, "Cannot parse character file: {0}, because of: {1}", actualFilename, e.Message);
                        return false;
                    }
                }
                return true;
           }
           return false;
        }
        public void DrawCharacter(string name)
        {
            Character character = new Character();
            if(GameCharacters.TryGetValue(name, out character))
            {
                character.Draw();
            }
        }
        public const string BasePathForCharacters = "CharacterFiles";
        public Dictionary<string, Character> GameCharacters { get; private set; }
        public TextureManager textureManager { private get; set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public RenderWindow target { private get; set; }

    }
}
