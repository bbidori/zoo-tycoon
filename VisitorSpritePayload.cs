using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo_tycoon
{
    public class VisitorSpritePayload : SpritePayload
    {
        public bool Interactible = false;
        public DateTime LastTrashThrowedTime = DateTime.UtcNow;
        public Point lastPosition = new Point (0,0); 
        public int caseDeplacement = 0;
    }
}
