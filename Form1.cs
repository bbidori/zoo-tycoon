using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpriteLibrary;


namespace zoo_tycoon
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            SpriteController sprite;
            sprite = new SpriteController(cloture1);

            Sprite Animal = new Sprite(new Point(0, 512), sprite, Properties.Resources.zoo_tileset, 32, 32, 500, 8);
            Animal.SetName("ours");

            Animal.AutomaticallyMoves = true;
            Animal.CannotMoveOutsideBox = true;
            Animal.SetSpriteDirectionDegrees(180);
            Animal.MovementSpeed = 30;

            Animal.PutBaseImageLocation(new Point(0, 0));

            

        }

    }
}
