using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo_tycoon
{
    public class VisitorSpritePayload : SpritePayload
    {
        public bool Interactible = false;
        public DateTime LastTrashThrowedTime = DateTime.UtcNow;
        public int caseDeplacement = 0;
    }
}
