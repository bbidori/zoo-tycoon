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
    public partial class ZooTycoon : Form
    {
        SpriteController TheGameController;  //The Sprite controller that draws all our sprites

        //Set initial values.  When each of these time-out, we allow ourselves to check for a keypress,
        //fire a torpedo, etc.  Start with them all as NOW, since the delay is not long at all
        DateTime LastMovement = DateTime.UtcNow;  //The last time we checked for a keypress.
        DateTime LastTorpedoL = DateTime.UtcNow;  //The last time the player shot left
        DateTime LastTorpedoR = DateTime.UtcNow;  //The last time the player shot Right
        DateTime LastTorpedoU = DateTime.UtcNow;  //The last time the player shot Up
        DateTime LastAddItem = DateTime.UtcNow - TimeSpan.FromSeconds(1); //We want to start by adding things
        DateTime LastHealTime = DateTime.UtcNow;


        Sprite PlayerSub = null;   //The player sub
        Direction LastDirection = Direction.none;  //The direction our player sub was going last
        public Point PlayerLastLocation;


        bool PlayingGame = false;
        bool isInteracting = false;

        DateTime GameStartTime = DateTime.UtcNow;
        FormInteraction formInteraction;

        public ZooTycoon()
        {
            InitializeComponent();
            //Set up the background.  We need to do this before making our sprite controller.
            MainDrawingArea.BackgroundImage = new Bitmap(Properties.Resources.background_zoo2, constants.BackgroundSize);
            MainDrawingArea.BackgroundImageLayout = ImageLayout.Stretch;

            //Make a new sprite controller using the PictureBox
            TheGameController = new SpriteController(MainDrawingArea, DoTick);

            //Load the sprites from the resources.  Only need to do this once
            loadSprites();
            PrintDirections();

        }
        /// <param name="Y">The Y position on the image</param>
        void PrintOneLine(Graphics G, string theText, int Y)
        {
            //The font to use
            Font stringFont = new Font("Consolas", 18);
            //Figure out how big the text is, X and Y.
            SizeF Big = G.MeasureString(theText, stringFont);
            //Find the place where to put it. Find the center of the image, then subtract half the width of the text
            int X = (MainDrawingArea.BackgroundImage.Width / 2) - (int)(Big.Width / 2);
            //Write it twice.  First, in white (and just a little offset)
            G.DrawString(theText, stringFont, Brushes.White, X - 1, Y - 1);
            //Then, write it in black, the main text
            G.DrawString(theText, stringFont, Brushes.Black, X, Y);
        }

        /// <summary>
        /// Print the directions for the game on the image background
        /// </summary>
        void PrintDirections()
        {
            //Make a copy of the image so we can write to it
            Image nImage = new Bitmap(Properties.Resources.Zoo_Tycoon_Opening, constants.BackgroundSize);

            MainDrawingArea.BackgroundImage = nImage;
            //Print the instructions for the game on the background
            using (Graphics G = Graphics.FromImage(nImage))
            {
                PrintOneLine(G, "Press ENTER to begin", 420);
            }
            Image nImageCopy = new Bitmap(nImage, constants.BackgroundSize);
            //Replace the background of the image.  We need to do this so remaining badguys do not erase the text
            //The background is what we use to write on top of the sprites when we erase them.
            if (TheGameController != null)
                TheGameController.ReplaceOriginalImage(nImageCopy);
            //Now, replace the foreground of the image.
            MainDrawingArea.BackgroundImage = nImage;
            MainDrawingArea.Invalidate();
        }
        
        private void loadSprites()
        {
            //the player's character
            Image oImage = Properties.Resources.Perso_Standing_Facing; //character is created facing us
            Sprite one = new Sprite(new Point(0, 0), TheGameController, oImage, (oImage.Width / 3), oImage.Height, 400, 3); //animation 0 = standing and facing us
            oImage = Properties.Resources.Perso_Standing_Left;
            one.AddAnimation(new Point(0, 0), Properties.Resources.Perso_Standing_Left, (oImage.Width / 3), oImage.Height, 400, 3);  //animation 1 = standing and facing left
            oImage = Properties.Resources.Perso_Standing_Right;
            one.AddAnimation(new Point(0, 0), Properties.Resources.Perso_Standing_Right, (oImage.Width / 3), oImage.Height, 400, 3); //animation 2 = standing and facing right
            oImage = Properties.Resources.Perso_Standing_Back;
            one.AddAnimation(Properties.Resources.Perso_Standing_Back, oImage.Width, oImage.Height); //animation 3 = standing and with it's back facing us
            one.SetSize(constants.CharacterSize);
            one.CannotMoveOutsideBox = true;
            one.SetName(SpriteName.PlayerSub.ToString());
            one.CheckBeforeMove += PlayerCheckBeforeMovement;


            Sprite Animal = new Sprite(new Point(0, 512), TheGameController, Properties.Resources.zoo_tileset, 32, 32, 800, 8);
            Animal.SetName(SpriteName.Ours.ToString());

            Animal.AutomaticallyMoves = false;
            Animal.CannotMoveOutsideBox = true;
            Animal.SetSpriteDirectionDegrees(180);
            Animal.MovementSpeed = 0;
            Animal.SetSize(constants.OursSize);
            //////////////////////////////////////////////////////////
            Sprite cloture = new Sprite(TheGameController, Properties.Resources.dev, constants.ClotureSize);
            cloture.SetName(SpriteName.Cloture.ToString());
            cloture.AutomaticallyMoves = false;
            cloture.CannotMoveOutsideBox = true;
            cloture.SetSize(constants.ClotureSize);
        }

        /// <summary>
        /// DoTick.  This is the main function of the game.  It happens every few milliseconds
        /// read the README file in the resources for a bigger description of how it works
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void DoTick(object Sender, EventArgs e)
        {
            //If the game is not currently playing (the instructions are visible)
            if (!isInteracting)
            {
                if (!PlayingGame)
                {

                    //All we do is "press Enter to start the game"
                    if (TheGameController.IsKeyPressed(Keys.Enter))
                        StartGame();
                    //Exit DoTick if we are not currently playing the game.
                    return;
                }


                //Check to see if the player has pressed a key, and process it.
                //Do sub movement and firing of player torpedoes
                CheckForKeyPress(Sender, e);
            }
        }

        private void CheckForKeyPress(object sender, EventArgs e)
        {
            TorpSpritePayload TempTSP = (TorpSpritePayload)PlayerSub.payload;
            //on garde les méthodes de shoot pour lorsqu'on va faire les interactions
            bool left = false;
            bool right = false;
            bool up = false;
            bool down = false;
            bool interact = false;
            bool didsomething = false;
            TimeSpan duration = DateTime.UtcNow - LastMovement;
            if (duration.TotalMilliseconds < 200)
                return;

            //pbOcean.Invalidate();

            //UpdateLevel(); //Change the game level if needed

            LastMovement = DateTime.Now;
            if (TheGameController.IsKeyPressed(Keys.A) || TheGameController.IsKeyPressed(Keys.Left))
            {
                left = true;
            }
            if (TheGameController.IsKeyPressed(Keys.S) || TheGameController.IsKeyPressed(Keys.Down))
            {
                down = true;
            }
            if (TheGameController.IsKeyPressed(Keys.D) || TheGameController.IsKeyPressed(Keys.Right))
            {
                right = true;
            }
            if (TheGameController.IsKeyPressed(Keys.W) || TheGameController.IsKeyPressed(Keys.Up))
            {
                up = true;
            }
            if (TheGameController.IsKeyPressed(Keys.X))
            {
                //The > key
                interact = true;
            }
            if (down && up) return;
            if (left && right) return;

            //Check if interraction key is valid before any other movements cancel it
            if (interact)
            {
                if (TempTSP.Interactible)
                {
                    isInteracting = true;
                    PlayerSub.MovementSpeed = 0;
                    LastDirection = Direction.none;
                    MenuInterraction();
                }
            }

            if (!didsomething && left && up)
            {
                // if we are already going one direction, do not tell it to go the same direction
                if (LastDirection != Direction.up_left)
                {
                    LastDirection = Direction.up_left;
                    PlayerSub.ChangeAnimation(1); //left
                    PlayerSub.SetSpriteDirectionDegrees(135);
                    PlayerSub.AutomaticallyMoves = true;
                    PlayerSub.MovementSpeed = constants.PlayerSpeedMixDirection;


                    TempTSP.Interactible = false;
                    PlayerSub.payload = TempTSP;
                }
                didsomething = true;
            }
            if (!didsomething && left && down)
            {
                if (LastDirection != Direction.down_left)
                {
                    LastDirection = Direction.down_left;
                    PlayerSub.ChangeAnimation(1); //left
                    PlayerSub.SetSpriteDirectionDegrees(225);
                    PlayerSub.AutomaticallyMoves = true;
                    PlayerSub.MovementSpeed = constants.PlayerSpeedMixDirection;

                    TempTSP.Interactible = false;
                    PlayerSub.payload = TempTSP;
                }
                didsomething = true;
            }
            if (!didsomething && left)
            {
                if (LastDirection != Direction.left)
                {
                    LastDirection = Direction.left;
                    PlayerSub.ChangeAnimation(1); //left
                    PlayerSub.SetSpriteDirectionDegrees(180);
                    PlayerSub.AutomaticallyMoves = true;
                    PlayerSub.MovementSpeed = constants.PlayerSpeedLeftRight;

                    TempTSP.Interactible = false;
                    PlayerSub.payload = TempTSP;
                }
                didsomething = true;
            }
            if (!didsomething && right && up)
            {
                if (LastDirection != Direction.up_right)
                {
                    LastDirection = Direction.up_right;
                    PlayerSub.ChangeAnimation(2); //right
                    PlayerSub.SetSpriteDirectionDegrees(45);
                    PlayerSub.AutomaticallyMoves = true;
                    PlayerSub.MovementSpeed = constants.PlayerSpeedMixDirection;

                    TempTSP.Interactible = false;
                    PlayerSub.payload = TempTSP;
                }
                didsomething = true;
            }
            if (!didsomething && right && down)
            {
                if (LastDirection != Direction.down_right)
                {
                    LastDirection = Direction.down_right;
                    PlayerSub.ChangeAnimation(2); //right
                    PlayerSub.SetSpriteDirectionDegrees(315);
                    PlayerSub.AutomaticallyMoves = true;
                    PlayerSub.MovementSpeed = constants.PlayerSpeedMixDirection;

                    TempTSP.Interactible = false;
                    PlayerSub.payload = TempTSP;
                }
                didsomething = true;
            }
            if (!didsomething && right)
            {
                if (LastDirection != Direction.right)
                {
                    LastDirection = Direction.right;
                    PlayerSub.ChangeAnimation(2); //right
                    PlayerSub.SetSpriteDirectionDegrees(0);
                    PlayerSub.AutomaticallyMoves = true;
                    PlayerSub.MovementSpeed = constants.PlayerSpeedLeftRight;

                    TempTSP.Interactible = false;
                    PlayerSub.payload = TempTSP;
                }
                didsomething = true;
            }
            if (!didsomething && up)
            {
                if (LastDirection != Direction.up)
                {
                    LastDirection = Direction.up;
                    PlayerSub.ChangeAnimation(3); //up
                    PlayerSub.SetSpriteDirectionDegrees(90);
                    PlayerSub.AutomaticallyMoves = true;
                    PlayerSub.MovementSpeed = constants.PlayerSpeedUpDown;

                    TempTSP.Interactible = false;
                    PlayerSub.payload = TempTSP;
                }
                didsomething = true;
            }
            if (!didsomething && down)
            {
                if (LastDirection != Direction.down)
                {
                    LastDirection = Direction.down;
                    PlayerSub.ChangeAnimation(0); //down
                    PlayerSub.SetSpriteDirectionDegrees(270);
                    PlayerSub.AutomaticallyMoves = true;
                    PlayerSub.MovementSpeed = constants.PlayerSpeedUpDown;

                    TempTSP.Interactible = false;
                    PlayerSub.payload = TempTSP;
                }
                didsomething = true;
            }

            // if we have not been told to move, we stop
            if (!didsomething)
            {
                PlayerSub.MovementSpeed = 0;
                LastDirection = Direction.none;
            }

        }

        private void MenuInterraction()
        {
            formInteraction = new FormInteraction();
            formInteraction.SetDesktopLocation(100, 100);
            formInteraction.Visible = true;
            formInteraction.Option1.Click += new System.EventHandler(this.Option1Click);
            formInteraction.Option2.Click += new System.EventHandler(this.Option2Click);
            formInteraction.Option3.Click += new System.EventHandler(this.Option3Click);
        }

        private void Option3Click(object sender, EventArgs e)
        {
            formInteraction.Close();
            this.TopMost = true;
            this.TopMost = false;
            this.Close();
        }

        private void Option2Click(object sender, EventArgs e)
        {
            formInteraction.Close();
            this.TopMost = true;
            this.TopMost = false;
            showResult(2);
        }

        private void Option1Click(object sender, EventArgs e)
        {
            formInteraction.Close();
            this.TopMost = true;
            this.TopMost = false;
            showResult(1);
        }

        private void showResult(int v)
        {
            DialogResult result;
            result = MessageBox.Show("l'option sélectionné est le " + v, "option Selectionné", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                isInteracting = false;
            }
        }

        //Set up for the start of the game.  We do this at the beginning of each game.
        //Just before this, we had done a PrintDirections() and were waiting for someone
        //to press enter.
        private void StartGame()
        {
            //Make a copy of the image so we can write to it
            Image nImage = new Bitmap(Properties.Resources.background_zoo2, constants.BackgroundSize);
            TheGameController.ReplaceOriginalImage(nImage);
            MainDrawingArea.BackgroundImage = nImage;
            MainDrawingArea.Invalidate();

            //Get rid of all sprites.  We will add new ones for the game
            foreach (Sprite one in TheGameController.SpritesBasedOffAnything())
            {
                one.Destroy();
            }

            //Pull out the player sprite.
            PlayerSub = TheGameController.DuplicateSprite(SpriteName.PlayerSub.ToString());
            //Add this function to keep the player sub from exiting the box
            PlayerSub.CheckBeforeMove += PlayerCheckBeforeMovement; //CAN ALSO BE BASED ON TorpedoCheckBeforeMouvement
            //Add an event for when our sub collides with something
            PlayerSub.SpriteHitsSprite += PlayerSubHitsSomething;

            //Create a payload for the sprite //NEED TO RENAME AT SOME POINT INTO STATS OR SMTH
            TorpSpritePayload PlayerTSP = new TorpSpritePayload();
            PlayerTSP.isGood = true;
            PlayerSub.payload = PlayerTSP; //payload is the same as a userControl class without the graphics component (Luigi)

            //This is a fail-safe check.  If the main sprite does not load, we are in trouble.
            if (PlayerSub != null)
            {
                PlayerSub.PutBaseImageLocation(constants.PlayerStartingPoint);
                Sprite one = TheGameController.DuplicateSprite(SpriteName.Cloture.ToString());
                one.PutBaseImageLocation(new Point(50, 65));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture.ToString());
                one.PutBaseImageLocation(new Point(215, 65));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture.ToString());
                one.PutBaseImageLocation(new Point(50, 265));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture.ToString());
                one.PutBaseImageLocation(new Point(215, 265));
                one = TheGameController.DuplicateSprite(SpriteName.Ours.ToString());
                one.PutBaseImageLocation(new Point(100, 100));
            }
            else
            {
                PlayingGame = false;
                MessageBox.Show("Unable to load the primary sprite.  Closing!");
                Close();
            }
            //Reset things so the second game does no start insanely
            //mettre les valeurs à false s'il y en a

            //Set the game start to be now.  The level is calculated off this value
            GameStartTime = DateTime.UtcNow;

            //Set it so we are playing the game.  Now DoTick will continue processing stuff
            PlayingGame = true;

            //creatingOtherSprites();
        }

        private void creatingOtherSprites()
        {
            Sprite newSprite = null;
            TorpSpritePayload TSP;
            TSP = new TorpSpritePayload();
            //347, 250 character Position
            int Xlocation;
            int YLocation;

            newSprite = TheGameController.DuplicateSprite(SpriteName.EnemySub.ToString()); //sprite 1
            Xlocation = 350;
            YLocation = 200;
            TSP.Movable = false;
            TSP.Interactible = false;
            if (newSprite != null)
            {
                TSP.isGood = false;
                newSprite.PutBaseImageLocation(new Point(Xlocation, YLocation));
                newSprite.SetSpriteDirectionDegrees(0);
                newSprite.payload = TSP;
                newSprite.AutomaticallyMoves = false;
            }

            newSprite = TheGameController.DuplicateSprite(SpriteName.EnemySub.ToString()); //sprite 2
            Xlocation = 300;
            YLocation = 200;
            TSP.Movable = false;
            TSP.Interactible = false;
            if (newSprite != null)
            {
                TSP.isGood = false;
                newSprite.PutBaseImageLocation(new Point(Xlocation, YLocation));
                newSprite.SetSpriteDirectionDegrees(0);
                newSprite.payload = TSP;
                newSprite.AutomaticallyMoves = false;
            }

            newSprite = TheGameController.DuplicateSprite(SpriteName.EnemySub.ToString()); //sprite 3 
            Xlocation = 400;
            YLocation = 200;
            TSP.Movable = false;
            TSP.Interactible = false;
            if (newSprite != null)
            {
                TSP.isGood = false;
                newSprite.PutBaseImageLocation(new Point(Xlocation, YLocation));
                newSprite.SetSpriteDirectionDegrees(0);
                newSprite.payload = TSP;
                newSprite.AutomaticallyMoves = false;
            }
        }

        /// <summary>
        /// This happens when the player submarine sprite hits something.  It could be a player torpedo, which
        /// gets ignored.  But it could be a whale, an enemy sub, an enemy torpedo, or an enemy depth-charge.
        /// Deal with all those options here.  This function is added to the player sub when we load sprites.
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        /// YOU CAN USE THAT METHOD FOR WHEN YOU GET IN CONTACT WITH ANOTHER SPRITE OR A UNMOVABLE OBJECT
        private void PlayerSubHitsSomething(object Sender, SpriteEventArgs e)
        {
            if (!(Sender is Sprite)) return;
            Sprite Target = (Sprite)Sender;
            //If we are not playing the game, then do not do anything.
            if (!PlayingGame) return;

            //zones de contact possible:
            //en bas direct, en bas droite, en bas gauche, en haut direct, en haut gauche, en haut droit, à gauche direct, à droite direct
            //il faut faire des marges

            Target.PutBaseImageLocation(PlayerLastLocation);
            if (Target.payload is TorpSpritePayload)
            {
                TorpSpritePayload TempTSP = (TorpSpritePayload)Target.payload;
                TempTSP.Interactible = true;
                Target.payload = TempTSP;
            }
        }

        //Before the player moves.  Make sure it is a valid movement
        //Ensure he is not moving off the playing-field.
        void PlayerCheckBeforeMovement(object Sender, SpriteEventArgs e)
        {
            if (!(Sender is Sprite)) return;
            Sprite Target = (Sprite)Sender;
            Direction Where = SpriteDirection(Target);
            PlayerLastLocation = Target.BaseImageLocation;
            //Check left side
            if (e.NewLocation.X < constants.DistanceFromSide && (Where == Direction.down_left || Where == Direction.left || Where == Direction.up_left))
            {
                e.NewLocation.X = constants.DistanceFromSide;
            }

            //Check right side
            if (e.NewLocation.X > MainDrawingArea.BackgroundImage.Width - constants.DistanceFromSide - PlayerSub.GetSize.Width &&
                (Where == Direction.down_right || Where == Direction.right || Where == Direction.up_right))
            {
                e.NewLocation.X = MainDrawingArea.BackgroundImage.Width - constants.DistanceFromSide - PlayerSub.GetSize.Width;
            }

            //check top
            if (e.NewLocation.Y < constants.WaterLevel + constants.DistanceFromBot && (Where == Direction.up_left || Where == Direction.up || Where == Direction.up_right))
            {
                e.NewLocation.Y = constants.WaterLevel + constants.DistanceFromBot;
            }

            //Check bottom
            if (e.NewLocation.Y > constants.GroundLevel - constants.DistanceFromBot - PlayerSub.GetSize.Height &&
                (Where == Direction.down_right || Where == Direction.down || Where == Direction.down_left))
            {
                e.NewLocation.Y = constants.GroundLevel - constants.DistanceFromBot - PlayerSub.GetSize.Height;
            }
        }

        /// <summary>
        /// Return the direction a particular sprite is moving.
        /// </summary>
        /// <param name="Which"></param>
        /// <returns></returns>
        Direction SpriteDirection(Sprite Which)
        {
            Double dir = Which.GetSpriteDegrees();
            if (dir > 30 && dir < 50) return Direction.up_right; //close to 45 degrees
            if (dir > 85 && dir < 95) return Direction.up; //close to 90 degrees
            if (dir > 130 && dir < 140) return Direction.up_left; //close to 135 degrees
            if (dir > 175 && dir < 185) return Direction.left; //close to 180 degrees
            if (dir > 220 && dir < 230) return Direction.down_left; //close to 225 degrees
            if (dir > 265 && dir < 275) return Direction.down; //close to 270
            if (dir > 310 && dir < 320) return Direction.down_right; //close to 315
            if (dir > -5 && dir < 5) return Direction.right; //close to zero degrees
            if (dir > 355 && dir < 365) return Direction.right; //close to 360 degrees

            return Direction.none;
        }

    }


    public class TorpSpritePayload : SpritePayload
    {
        //consider using it for stats (money, state of the object (movable, unmovable) and such)
        public bool isGood = false; //not sure what it does
        public Direction lastdirection = Direction.none; //no idea what it does
        public bool Movable = false;
        public bool Interactible = false;
        public int Solde = 80;
        public int nombreAnimaux = 0;
    }
    public class AnimalSpritePayload : SpritePayload
    {
        //consider using it for stats (money, state of the object (movable, unmovable) and such)
        public bool isGood = false; //not sure what it does
        public Direction lastdirection = Direction.none; //no idea what it does
        public bool Movable = false;
        public bool Interactible = false;
        public int QqtConsomme = 0;
        public int PrixAchat = 0;
        public int TempsGestation = 0;
        public int TempsLimite = 0;
    }


    public class constants
    {
        public const int DistanceFromSide = 10; //limite gauche et droite de l'écran?
        public const int DistanceFromBot = 10; // aucune idée je vais être honnêtre, don't know why
        public const int WaterLevel = 10; //hauteur de l'espace de déplacement peut-être? 
        public const int GroundLevel = 470; //longeur de l'espace de déplacement peut-être? 

        public static Size BackgroundSize { get { return new Size(500, 500); } }
        public static Size EnemySubSize { get { return new Size(50, 20); } }

        public static Size CharacterSize { get { return new Size(30, 40); } } //35,50 parfais
        public static Size OursSize { get { return new Size(40, 55); } }
        public static Point PlayerStartingPoint { get { return new Point(420, 150); } }

        public static Size ClotureSize { get { return new Size(120, 140); } }

        public static int PlayerSpeedLeftRight = 5;
        public static int PlayerSpeedUpDown = 8;
        public static int PlayerSpeedMixDirection = 8;
    }
    public enum SpriteName { PlayerSub, EnemySub, Ours, Chèvre, Girafe, Cloture }
    public enum Direction { none, up, down, left, right, up_left, up_right, down_left, down_right }
}

