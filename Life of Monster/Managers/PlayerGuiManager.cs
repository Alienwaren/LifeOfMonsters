using NLog;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Life_of_Monster.Managers
{
    public class PlayerGuiManager
    {
        public PlayerGuiManager()
        {
        }
        public bool GuiSheetsToMemory()
        {
            try
            {
                List<string> GuiSheets = Directory.GetFiles(BaseDirForGuiFiles).ToList<string>();   
                if(GuiSheets.Count > 0)
                {
                    foreach (string item in GuiSheets)
                    {
                        int dotLocation = item.IndexOf('.');
                        string FileNameWithoutPath = item.Substring(9, dotLocation - 9);
                        Texture sheetTexture = new Texture(item);
                        GuiSpriteSheets.Add(item, sheetTexture);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Fatal("Could not load Gui Files to memory because: {0}", e.Message);
                return false;
            }
            return true;
        }
        public bool PrepareGuiElements()
        {
            try
            {
                List<string> GuiDescriptionFiles = Directory.GetFiles(BaseDirForGuiDescFiles).ToList<string>();
                if(GuiDescriptionFiles.Count > 0)
                {
                    foreach (string item in GuiDescriptionFiles)
                    {
                        XDocument root = XDocument.Load(item);
                        XElement GuiRoot = root.Element("gui");
                        XElement SourceSheet = GuiRoot.Element("sourceSheet");
                        XElement TextArea = GuiRoot.Element("textarea");
                        string SourceSheetStr = Regex.Replace(SourceSheet.Value, @"\s+", string.Empty);
                        //TODO: Make GUI Objects
                    }
                }
            }
            catch (Exception e)
            {
                logger.Fatal("Could not load Gui Files to memory because: {0}", e.Message);
                return false;
            }
            return true;
        }
        private Dictionary<string, Texture> GuiSpriteSheets = new Dictionary<string, Texture>();
        public Dictionary<string, Sprite> GuiControls { get; set; }
        private const string BaseDirForGuiFiles = "GUIFiles";
        private const string BaseDirForGuiDescFiles = BaseDirForGuiFiles + "\\GUIDescriptionFiles";
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
