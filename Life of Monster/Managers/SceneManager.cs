using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_of_Monster.Managers
{
    public class SceneManager
    {
        public SceneManager()
        {

        }
        private const string baseSceneFilesPath = "SceneFiles";
        public TextureManager texturesManager{ get; set; }
    }
}
