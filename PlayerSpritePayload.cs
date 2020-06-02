using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo_tycoon
{
    public class PlayerSpritePayload : SpritePayload
    {
        public Direction lastdirection = Direction.none;
        public bool Interactible = false;
        public double Solde = 100;
        public int nombreAnimaux = 0;
        public double Jour = 1;
        public string nameLastClotureInteracted = "-";
    }
}
