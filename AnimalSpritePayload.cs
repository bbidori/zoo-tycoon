using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo_tycoon
{
    public class AnimalSpritePayload : SpritePayload
    {
        public bool Interactible = true;
        public int NiveauFaim = 100;
        public int PrixAchat = 0;
        public string TypeAnimalInterieur = "";
    }
}
