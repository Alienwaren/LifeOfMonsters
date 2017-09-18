using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Life_of_Monster.Logic;
using Life_of_Monster.Managers;
using SFML.Graphics;
using SFML.Window;

namespace Life_of_Monster.Characters
{
    public class Character : Renderable
    {

        public Character(int id = 0, string characterSurname = "", string name = "")
            : base(id, name)
        {
            CharacterName = name;
            CharacterAffection = 0;
        }
       
        public string CharacterName { get; set; }
        public string CharacterSurname { get; set; }
        public int CharacterAffection { get; set; }
        public string Species { get; set; }
      


    }
}
