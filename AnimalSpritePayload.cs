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

        //niveau de faim dans chaque cloture (il faut qu'ils soient indépendants)
        public int NiveauFaimCloture1 = 100;
        public int NiveauFaimCloture2 = 100;
        public int NiveauFaimCloture3 = 100;
        public int NiveauFaimCloture4 = 100;

        public int PrixAchatChevre = 0;
        public int PrixAchatOurs = 0;
        public int PrixAchatRhinoceros = 0;
        public int PrixAchatLion = 0;

        //nombre d'animal dans chaque cloture
        public int nbrAnimalCloture1 = 0;
        public int nbrAnimalCloture2 = 0;
        public int nbrAnimalCloture3 = 0;
        public int nbrAnimalCloture4 = 0;
    }
}
