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
        SpriteController TheGameController;  //Le sprite qui contrôle tous les autres sprites

        DateTime LastMovement = DateTime.UtcNow;  //Dernière fois qu'on a vérifié si on a appuyé une touche
        DateTime LastAddItem = DateTime.UtcNow - TimeSpan.FromSeconds(1);
        DateTime LastMoneyDeducted = DateTime.UtcNow - TimeSpan.FromSeconds(1);


        Sprite PlayerCharacter = null;   //Le sprite du personnage
        PlayerSpritePayload PlayerTSP = new PlayerSpritePayload();
        AnimalSpritePayload AnimalTSP = new AnimalSpritePayload();
        ClotureSpritePayload ClotureTSP = new ClotureSpritePayload();

        Direction LastDirection = Direction.none;  //dernière direction où on allait
        public Point PlayerLastLocation; //dernière position où on était


        bool PlayingGame = false;
        bool isInteracting = false;

        DateTime GameStartTime = DateTime.UtcNow;
        FormInteraction formInteraction;

        public ZooTycoon()
        {
            InitializeComponent();
            MainDrawingArea.BackgroundImage = new Bitmap(Properties.Resources.background_zoo2, constants.BackgroundSize);
            MainDrawingArea.BackgroundImageLayout = ImageLayout.Stretch;
            TheGameController = new SpriteController(MainDrawingArea, DoTick);
            loadSprites();
            PrintDirections();

        }
        /// <summary>
        /// Écrit une ligne passé en paramètre sur l'image d'acceuil selon la position Y qu'on lui a donné
        /// </summary>
        /// <param position="Y">Position Y sur l'image </param>
        /// <param text="theText">Texte a ecrire sur l'image</param>
        void PrintOneLine(Graphics G, string theText, int Y)
        {
            Font stringFont = new Font("Consolas", 18);
            SizeF Big = G.MeasureString(theText, stringFont);
            int X = (MainDrawingArea.BackgroundImage.Width / 2) - (int)(Big.Width / 2);
            G.DrawString(theText, stringFont, Brushes.White, X - 1, Y - 1);
            G.DrawString(theText, stringFont, Brushes.Black, X, Y);
        }

        /// <summary>
        /// Imprime les directives du jeu sur le background
        /// </summary>
        void PrintDirections()
        {
            Image nImage = new Bitmap(Properties.Resources.Zoo_Tycoon_Opening, constants.BackgroundSize);

            MainDrawingArea.BackgroundImage = nImage;
            using (Graphics G = Graphics.FromImage(nImage))
            {
                PrintOneLine(G, "Press ENTER to begin", 420);
            }
            Image nImageCopy = new Bitmap(nImage, constants.BackgroundSize);
            if (TheGameController != null)
                TheGameController.ReplaceOriginalImage(nImageCopy);
            MainDrawingArea.BackgroundImage = nImage;
            MainDrawingArea.Invalidate();
        }

        /// <summary>
        /// load une copie de chaque sprite possible pour qu'on puisse les duppliquer facilement
        /// </summary>
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
            one.SetName(SpriteName.PlayerCharacter.ToString());
            one.CheckBeforeMove += PlayerCheckBeforeMovement;


            one = new Sprite(new Point(0, 512), TheGameController, Properties.Resources.zoo_tileset, 32, 32, 800, 8);
            one.SetName(SpriteName.Ours.ToString());

            one.AutomaticallyMoves = false;
            one.CannotMoveOutsideBox = true;
            one.SetSpriteDirectionDegrees(180);
            one.MovementSpeed = 0;
            one.SetSize(constants.OursSize);
            //////////////////////////////////////////////////////////
            one = new Sprite(TheGameController, Properties.Resources.dev, constants.ClotureSize);
            one.SetName(SpriteName.Cloture1.ToString());
            one.AutomaticallyMoves = false;
            one.CannotMoveOutsideBox = true;
            one.SetSize(constants.ClotureSize);

            one = new Sprite(TheGameController, Properties.Resources.dev, constants.ClotureSize);
            one.SetName(SpriteName.Cloture2.ToString());
            one.AutomaticallyMoves = false;
            one.CannotMoveOutsideBox = true;
            one.SetSize(constants.ClotureSize);

            one = new Sprite(TheGameController, Properties.Resources.dev, constants.ClotureSize);
            one.SetName(SpriteName.Cloture3.ToString());
            one.AutomaticallyMoves = false;
            one.CannotMoveOutsideBox = true;
            one.SetSize(constants.ClotureSize);

            one = new Sprite(TheGameController, Properties.Resources.dev, constants.ClotureSize);
            one.SetName(SpriteName.Cloture4.ToString());
            one.AutomaticallyMoves = false;
            one.CannotMoveOutsideBox = true;
            one.SetSize(constants.ClotureSize);

            oImage = Properties.Resources.VisiteurMale;
            one = new Sprite(new Point(0, 0), TheGameController, oImage, (oImage.Width / 3), oImage.Height, 400, 3); //animation 0 = standing and facing us
            one.SetName(SpriteName.VisiteurM.ToString());
            one.CannotMoveOutsideBox = true;
            one.SetSize(constants.VisiteurSize);
            one.CheckBeforeMove += VisiteurCheckBeforeMove;

            oImage = Properties.Resources.VisiteurFemale;
            one = new Sprite(new Point(0, 0), TheGameController, oImage, (oImage.Width / 3), oImage.Height, 400, 3); //animation 0 = standing and facing us
            one.SetName(SpriteName.VisiteurF.ToString());
            one.CannotMoveOutsideBox = true;
            one.SetSize(constants.VisiteurSize);
            one.CheckBeforeMove += VisiteurCheckBeforeMove;

            one = new Sprite(TheGameController, Properties.Resources.trash_oject, constants.ClotureSize);
            one.SetName(SpriteName.Trash.ToString());
            one.CannotMoveOutsideBox = true;
            one.SetSize(constants.TrashSize);
        }
        /// <summary>
        /// Pour les visiteurs, vérifie avant de faire chaque mouvement 
        /// </summary>
        /// <remarks>NOTE: on aurait pu faire plus de chemins pour les visiteurs 
        /// mais par manque de temps, on a configuré qu'un seul itinéraire.</remarks>
        /// <param name="Sender">The sprite that is moving</param>
        /// <param name="e"></param>
        private void VisiteurCheckBeforeMove(object Sender, SpriteEventArgs e)
        {

            if (!(Sender is Sprite)) return;
            Sprite visiteur = (Sprite)Sender;
            VisitorSpritePayload visitorTSP;
            //si le payload n'est pas valide, on le détruit.
            if (!(visiteur.payload is VisitorSpritePayload))
            {
                visiteur.Destroy(); //It needs to be killed
                return;
            }
            visitorTSP = (VisitorSpritePayload)visiteur.payload;
            visitorTSP.lastPosition = visiteur.BaseImageLocation;
            switch (visitorTSP.caseDeplacement)
            {
                case 0:
                    {
                        if (visiteur.BaseImageLocation.Equals(new Point(371, 416)))
                        {
                            visiteur.SetSpriteDirectionToPoint(new Point(17, 416));
                            //newSprite.ChangeAnimation(0);  //normalement on ferait un change animation pour chaque mouvement mais on manque de temps
                            visitorTSP.caseDeplacement++;
                        }
                    }
                    break;
                case 1:
                    {
                        if (visiteur.BaseImageLocation.X < 19)
                        {
                            visiteur.AutomaticallyMoves = false;
                            visiteur.PutBaseImageLocation(new Point(17, 416));
                            visiteur.SetSpriteDirectionToPoint(new Point(17, 21));
                            visiteur.AutomaticallyMoves = true;
                            visitorTSP.caseDeplacement++;
                        }

                    }
                    break;
                case 2:
                    {
                        if (visiteur.BaseImageLocation.Y < 23)
                        {
                            visiteur.AutomaticallyMoves = false;
                            visiteur.PutBaseImageLocation(new Point(17, 21));
                            visiteur.SetSpriteDirectionToPoint(new Point(331, 21));
                            visiteur.AutomaticallyMoves = true;
                            visitorTSP.caseDeplacement++;
                        }

                    }
                    break;
                case 3:
                    {
                        if (visiteur.BaseImageLocation.X > 329)
                        {
                            visiteur.AutomaticallyMoves = false;
                            visiteur.PutBaseImageLocation(new Point(331, 21));
                            visiteur.SetSpriteDirectionToPoint(new Point(331, 210));
                            visiteur.AutomaticallyMoves = true;
                            visitorTSP.caseDeplacement++;
                        }

                    }
                    break;
                case 4:
                    {
                        if (visiteur.BaseImageLocation.X > 329)
                        {
                            visiteur.AutomaticallyMoves = false;
                            visiteur.PutBaseImageLocation(new Point(331, 21));
                            visiteur.SetSpriteDirectionToPoint(new Point(331, 210));
                            visiteur.AutomaticallyMoves = true;
                            visitorTSP.caseDeplacement++;
                        }

                    }
                    break;
                case 5:
                    {
                        if (visiteur.BaseImageLocation.Y > 207)
                        {
                            visiteur.AutomaticallyMoves = false;
                            visiteur.PutBaseImageLocation(new Point(331, 210));
                            visiteur.SetSpriteDirectionToPoint(new Point(368, 265));
                            visiteur.AutomaticallyMoves = true;
                            visitorTSP.caseDeplacement++;
                        }

                    }
                    break;
                case 6:
                    {
                        if (visiteur.BaseImageLocation.X > 366 && visiteur.BaseImageLocation.Y > 260)
                        {
                            visiteur.AutomaticallyMoves = false;
                            visiteur.PutBaseImageLocation(new Point(368, 265));
                            visiteur.SetSpriteDirectionToPoint(new Point(371, 420));
                            visiteur.AutomaticallyMoves = true;
                            visitorTSP.caseDeplacement++;
                        }

                    }
                    break;
                default:
                    {
                        if (visiteur.BaseImageLocation.Y > 420)
                        {
                            visiteur.AutomaticallyMoves = false;
                            visiteur.Destroy();
                        }
                    }
                    break;
            }

            //vérifie s'il est temps pour le visiteur de jetter des déchets
            if ((DateTime.UtcNow - visitorTSP.LastTrashThrowedTime).TotalMilliseconds > constants.TimeGenerateNewTrash)
            {

                if (TheGameController.RandomNumberGenerator.Next(100) < 10)
                {
                    if (TheGameController.RandomNumberGenerator.Next(5) == 0)
                    {
                        Sprite newSprite = TheGameController.DuplicateSprite(SpriteName.Trash.ToString());
                        newSprite.PutBaseImageLocation(visiteur.BaseImageLocation);
                        TrashSpritePayload trashTSP = new TrashSpritePayload();
                        newSprite.payload = trashTSP;
                    }
                    //Update le temps, pour s'assurer qu'on ne jette pas des déchets chaque 0,1 secondes
                    visitorTSP.LastTrashThrowedTime = DateTime.UtcNow;
                }
            }
        }

        /// <summary>
        /// DoTick.  C'est la méthode principale du jeu. Elle est appelé aux quelques milliseconds
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void DoTick(object Sender, EventArgs e)
        {
            if (PlayingGame)
            {
                ((PlayerSpritePayload)PlayerCharacter.payload).Jour += 0.02;
                refreshStats();
            }
            //S'assure qu'on ne puisse pas faire d'autres mouvements ou actions si on est en train d'interragir.
            //c'est une commande de sécurité
            if (!isInteracting)
            {
                //Si on ne joue pas encore
                if (!PlayingGame)
                {
                    //On doit appuyer enter pour commencer le jeu
                    if (TheGameController.IsKeyPressed(Keys.Enter))
                    {
                        StartGame();
                        CreateVisitor();
                    }

                    //Quitte le DoTick si on a pas commencé le jeu
                    return;
                }
                //Vérifie si on a appuyé sur une touche
                CheckForKeyPress(Sender, e);
            }

            //Ajoute des visiteurs selon le temps passé depuis la dernière fois qu'on a ajouté (ajoute à chaque 10 secondes)
            if ((DateTime.UtcNow - LastAddItem).TotalMilliseconds > constants.TimeBetweenReplenishVisitors)
            {
                LastAddItem = DateTime.UtcNow;

                //ItemTotalCount compte tous les visiteurs et déchets qu'il y a dans le jeu en ce moment
                int Items = ItemTotalCount("Object Etrangers");

                //Si le parc n'a pas trop de déchets ou trop de visiteurs
                if (Items < constants.MaxNumberItems)
                {
                    CreateVisitor();
                }
            }

            //À chaque 2 secondes, retire 0,10$ pour chaque déchet qu'il y a sur l'écran et 1$ pour chaque animal qui est affamé
            if ((DateTime.UtcNow - LastMoneyDeducted).TotalMilliseconds > constants.TimeBeforeLosingMoney)
            {
                //compte le nombre de déchets sur l'écrant
                int nbrDechets = ItemTotalCount("Dechets");

                PlayerTSP.Solde = PlayerTSP.Solde - (0.1 * nbrDechets);

                //vérifie s'il y a des animaux qui meurent de faim
                foreach (Sprite animal in TheGameController.SpritesBasedOffAnything())
                {
                    if (animal.payload is AnimalSpritePayload)
                    {
                        if (AnimalTSP.NiveauFaimCloture1 != 0 && AnimalTSP.nbrAnimalCloture1 != 0)
                        {
                            AnimalTSP.NiveauFaimCloture1 = AnimalTSP.NiveauFaimCloture1 - 5;
                        }
                        if (AnimalTSP.NiveauFaimCloture2 != 0 && AnimalTSP.nbrAnimalCloture2 != 0)
                        {
                            AnimalTSP.NiveauFaimCloture2 = AnimalTSP.NiveauFaimCloture2 - 5;
                        }
                        if (AnimalTSP.NiveauFaimCloture3 != 0 && AnimalTSP.nbrAnimalCloture3 != 0)
                        {
                            AnimalTSP.NiveauFaimCloture3 = AnimalTSP.NiveauFaimCloture3 - 5;
                        }
                        if (AnimalTSP.NiveauFaimCloture4 != 0 && AnimalTSP.nbrAnimalCloture4 != 0)
                        {
                            AnimalTSP.NiveauFaimCloture4 = AnimalTSP.NiveauFaimCloture4 - 5;
                        }

                        if (AnimalTSP.NiveauFaimCloture1 <= 0 && AnimalTSP.nbrAnimalCloture1 != 0)
                        {
                            PlayerTSP.Solde = PlayerTSP.Solde - 1;
                        }
                        if (AnimalTSP.NiveauFaimCloture2 <= 0 && AnimalTSP.nbrAnimalCloture2 != 0)
                        {
                            PlayerTSP.Solde = PlayerTSP.Solde - 1;
                        }
                        if (AnimalTSP.NiveauFaimCloture3 <= 0 && AnimalTSP.nbrAnimalCloture3 != 0)
                        {
                            PlayerTSP.Solde = PlayerTSP.Solde - 1;
                        }
                        if (AnimalTSP.NiveauFaimCloture4 <= 0 && AnimalTSP.nbrAnimalCloture4 != 0)
                        {
                            PlayerTSP.Solde = PlayerTSP.Solde - 1;
                        }
                    }

                }
                Console.WriteLine("Niveau Animal 1 : " + AnimalTSP.NiveauFaimCloture1);
                Console.WriteLine("Niveau Animal 2 : " + AnimalTSP.NiveauFaimCloture2);
                Console.WriteLine("Niveau Animal 3 : " + AnimalTSP.NiveauFaimCloture3);
                Console.WriteLine("Niveau Animal 4 : " + AnimalTSP.NiveauFaimCloture4);
                PlayerTSP.Solde = Math.Round(PlayerTSP.Solde, 1);
                Console.WriteLine("Solde : " + PlayerTSP.Solde);
                //on update le temps ici car les opérations ci-dessus prennent du temps
                LastMoneyDeducted = DateTime.UtcNow;
            }
        }

        private void CreateVisitor()
        {
            Sprite newSprite = null;
            VisitorSpritePayload VisitorTSP = new VisitorSpritePayload();
            int choice = TheGameController.RandomNumberGenerator.Next(2);
            switch (choice)
            {
                case 0:
                    //add a male
                    {
                        newSprite = TheGameController.DuplicateSprite(SpriteName.VisiteurM.ToString());
                        newSprite.SetSpriteDirectionDegrees(0);
                        //newSprite.ChangeAnimation(0);  //normalement on ferait un change animation mais on manque de temps
                        newSprite.MovementSpeed = constants.VisiteurSpeed;
                    }
                    break;
                case 1:
                    //add a female
                    {
                        newSprite = TheGameController.DuplicateSprite(SpriteName.VisiteurF.ToString());
                        newSprite.SetSpriteDirectionDegrees(0);
                        //newSprite.ChangeAnimation(0);  //normalement on ferait un change animation mais on manque de temps
                        newSprite.MovementSpeed = constants.VisiteurSpeed;
                    }
                    break;
            }

            if (newSprite != null)
            {
                newSprite.PutBaseImageLocation(constants.VisiteurStartingPoint);
                newSprite.CannotMoveOutsideBox = true;
            }
            newSprite.payload = VisitorTSP;
            newSprite.AutomaticallyMoves = true;

            //Gagne 2$ Pour chaque animal présent dans le jeu
            PlayerTSP.Solde = PlayerTSP.Solde + (2 * ItemTotalCount("Animaux"));
        }

        private int ItemTotalCount(string v)
        {
            int Counter = 0;

            if (v.Equals("Dechets"))
            {
                foreach (Sprite one in TheGameController.SpritesBasedOffAnything())
                {
                    if (one.SpriteOriginName == SpriteName.Trash.ToString())
                        Counter++;
                }
            }
            if (v.Equals("Object Etrangers"))
            {
                foreach (Sprite one in TheGameController.SpritesBasedOffAnything())
                {
                    if (one.SpriteOriginName == SpriteName.VisiteurM.ToString())
                        Counter++;
                    if (one.SpriteOriginName == SpriteName.VisiteurF.ToString())
                        Counter++;
                    if (one.SpriteOriginName == SpriteName.Trash.ToString())
                        Counter++;
                }
            }
            if (v.Equals("Animaux"))
            {
                foreach (Sprite one in TheGameController.SpritesBasedOffAnything())
                {
                    if (one.SpriteOriginName == SpriteName.Ours.ToString())
                        Counter++;
                    if (one.SpriteOriginName == SpriteName.Rhinoceros.ToString())
                        Counter++;
                    if (one.SpriteOriginName == SpriteName.Chèvre.ToString())
                        Counter++;
                    if (one.SpriteOriginName == SpriteName.Lion.ToString())
                        Counter++;
                }
            }

            return Counter;
        }

        /// <summary>
        /// Vérifie si une touche qui a été appuyé cause une action. Cette méthode est appelé presque chaque milliseconde
        /// </summary>
        private void CheckForKeyPress(object sender, EventArgs e)
        {
            //cherche le payload du sprite pour savoir s'il est proche d'un autre sprite et qu'il peut intéragir ou non
            bool left = false;
            bool right = false;
            bool up = false;
            bool down = false;
            bool interact = false;
            bool didsomething = false;
            TimeSpan duration = DateTime.UtcNow - LastMovement;
            if (duration.TotalMilliseconds < 200)
                return;

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
                interact = true;
            }
            if (down && up) return;
            if (left && right) return;

            //Verifie si la touche d'interraction a ete appuyé avant de faire d'autres mouvements
            if (interact)
            {
                if (PlayerTSP.Interactible)
                {
                    isInteracting = true;
                    PlayerCharacter.MovementSpeed = 0;
                    LastDirection = Direction.none;
                    MenuInterraction();
                }
            }

            if (!didsomething && left && up)
            {
                // si on va dans une direction, on empêche d'aller dans une autre.
                if (LastDirection != Direction.up_left)
                {
                    LastDirection = Direction.up_left;
                    PlayerCharacter.ChangeAnimation(1); //left
                    PlayerCharacter.SetSpriteDirectionDegrees(135);
                    PlayerCharacter.AutomaticallyMoves = true;
                    PlayerCharacter.MovementSpeed = constants.PlayerSpeedMixDirection;
                    PlayerTSP.Interactible = false;

                }
                didsomething = true;
            }
            if (!didsomething && left && down)
            {
                if (LastDirection != Direction.down_left)
                {
                    LastDirection = Direction.down_left;
                    PlayerCharacter.ChangeAnimation(1); //left
                    PlayerCharacter.SetSpriteDirectionDegrees(225);
                    PlayerCharacter.AutomaticallyMoves = true;
                    PlayerCharacter.MovementSpeed = constants.PlayerSpeedMixDirection;

                    PlayerTSP.Interactible = false;
                }
                didsomething = true;
            }
            if (!didsomething && left)
            {
                if (LastDirection != Direction.left)
                {
                    LastDirection = Direction.left;
                    PlayerCharacter.ChangeAnimation(1); //left
                    PlayerCharacter.SetSpriteDirectionDegrees(180);
                    PlayerCharacter.AutomaticallyMoves = true;
                    PlayerCharacter.MovementSpeed = constants.PlayerSpeedLeftRight;

                    PlayerTSP.Interactible = false;
                }
                didsomething = true;
            }
            if (!didsomething && right && up)
            {
                if (LastDirection != Direction.up_right)
                {
                    LastDirection = Direction.up_right;
                    PlayerCharacter.ChangeAnimation(2); //right
                    PlayerCharacter.SetSpriteDirectionDegrees(45);
                    PlayerCharacter.AutomaticallyMoves = true;
                    PlayerCharacter.MovementSpeed = constants.PlayerSpeedMixDirection;

                    PlayerTSP.Interactible = false;
                }
                didsomething = true;
            }
            if (!didsomething && right && down)
            {
                if (LastDirection != Direction.down_right)
                {
                    LastDirection = Direction.down_right;
                    PlayerCharacter.ChangeAnimation(2); //right
                    PlayerCharacter.SetSpriteDirectionDegrees(315);
                    PlayerCharacter.AutomaticallyMoves = true;
                    PlayerCharacter.MovementSpeed = constants.PlayerSpeedMixDirection;

                    PlayerTSP.Interactible = false;
                }
                didsomething = true;
            }
            if (!didsomething && right)
            {
                if (LastDirection != Direction.right)
                {
                    LastDirection = Direction.right;
                    PlayerCharacter.ChangeAnimation(2); //right
                    PlayerCharacter.SetSpriteDirectionDegrees(0);
                    PlayerCharacter.AutomaticallyMoves = true;
                    PlayerCharacter.MovementSpeed = constants.PlayerSpeedLeftRight;

                    PlayerTSP.Interactible = false;
                }
                didsomething = true;
            }
            if (!didsomething && up)
            {
                if (LastDirection != Direction.up)
                {
                    LastDirection = Direction.up;
                    PlayerCharacter.ChangeAnimation(3); //up
                    PlayerCharacter.SetSpriteDirectionDegrees(90);
                    PlayerCharacter.AutomaticallyMoves = true;
                    PlayerCharacter.MovementSpeed = constants.PlayerSpeedUpDown;

                    PlayerTSP.Interactible = false;
                }
                didsomething = true;
            }
            if (!didsomething && down)
            {
                if (LastDirection != Direction.down)
                {
                    LastDirection = Direction.down;
                    PlayerCharacter.ChangeAnimation(0); //down
                    PlayerCharacter.SetSpriteDirectionDegrees(270);
                    PlayerCharacter.AutomaticallyMoves = true;
                    PlayerCharacter.MovementSpeed = constants.PlayerSpeedUpDown;

                    PlayerTSP.Interactible = false;
                }
                didsomething = true;
            }

            // si on n'appuie plus sur une case de déplacement, on arrete de bouger
            if (!didsomething)
            {
                PlayerCharacter.MovementSpeed = 0;
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
            isInteracting = false;
        }

        private void Option2Click(object sender, EventArgs e)
        {
            formInteraction.Close();
            this.TopMost = true;
            this.TopMost = false;
            /*
             AnimalTSP.nbrAnimalCloture1++;
                AnimalTSP.NiveauFaimCloture1 = 100;
                ClotureTSP.TypeAnimalCloture1 = SpriteName.Ours.ToString();
                ClotureTSP.nbrAnimalCloture1++;
                PlayerTSP.nombreAnimaux++;
             */
            showResult(2);
            isInteracting = false;
        }

        private void Option1Click(object sender, EventArgs e)
        {
            formInteraction.Close();
            this.TopMost = true;
            this.TopMost = false;

            //cherche la cloture avec laquelle on a interagit.
            string nomAnimal = "-";
            int nbrAnimalDansCloture = 0;
            switch (PlayerTSP.nameLastClotureInteracted.ToString())
            {
                case "Cloture1":
                    nomAnimal = ClotureTSP.TypeAnimalCloture1;
                    nbrAnimalDansCloture = ClotureTSP.nbrAnimalCloture1;
                    break;
                case "Cloture2":
                    nomAnimal = ClotureTSP.TypeAnimalCloture2;
                    nbrAnimalDansCloture = ClotureTSP.nbrAnimalCloture2;
                    break;
                case "Cloture3":
                    nomAnimal = ClotureTSP.TypeAnimalCloture3;
                    nbrAnimalDansCloture = ClotureTSP.nbrAnimalCloture3;
                    break;
                case "Cloture4":
                    nomAnimal = ClotureTSP.TypeAnimalCloture4;
                    nbrAnimalDansCloture = ClotureTSP.nbrAnimalCloture4;
                    break;
            }
            //vérifie qu'il y a un animal à l'interieur de la cloture.
            if (nomAnimal != "-")
            {
                if (PlayerTSP.Solde > nbrAnimalDansCloture)
                {
                    switch (PlayerTSP.nameLastClotureInteracted.ToString())
                    {
                        case "Cloture1":
                            AnimalTSP.NiveauFaimCloture1 = 100;
                            break;
                        case "Cloture2":
                            AnimalTSP.NiveauFaimCloture2 = 100;
                            break;
                        case "Cloture3":
                            AnimalTSP.NiveauFaimCloture3 = 100;
                            break;
                        case "Cloture4":
                            AnimalTSP.NiveauFaimCloture4 = 100;
                            break;
                    }

                    PlayerTSP.Solde = PlayerTSP.Solde - nbrAnimalDansCloture;

                    showResult(1);
                }
                else
                {
                    showResult(3);
                }

            }
            isInteracting = false;
        }

        private void showResult(int v)
        {
            DialogResult result;
            switch (v)
            {
                case 1:
                    result = MessageBox.Show("Tous les animaux de l'enclot ont été nourri", "Animaux nourri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 2:
                    result = MessageBox.Show("Animal ajouté avec succès", "Ajout Animal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:
                    result = MessageBox.Show("Vous n'avez pas assez d'argent pour faire cette opération", "Manque argent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                isInteracting = false;
            }
        }

        /// <summary>
        /// On fait cette méthode après que le joueur est appuyé sur enter pour commencer la partie.
        /// </summary>
        private void StartGame()
        {
            Image nImage = new Bitmap(Properties.Resources.background_zoo2, constants.BackgroundSize);
            TheGameController.ReplaceOriginalImage(nImage);
            MainDrawingArea.BackgroundImage = nImage;
            MainDrawingArea.Invalidate();

            //Detruit tout les sprites qu'il y a dans le jeu pour s'assurer que tout va bien
            foreach (Sprite one in TheGameController.SpritesBasedOffAnything())
            {
                one.Destroy();
            }

            //Crée une copie du sprite du Joueur enregistré dans TheGameControler
            PlayerCharacter = TheGameController.DuplicateSprite(SpriteName.PlayerCharacter.ToString());
            //evenement pour s'assurer que le character ne sort pas du form
            PlayerCharacter.CheckBeforeMove += PlayerCheckBeforeMovement;
            //evènement si le sprite du joueur touche quelque chose
            PlayerCharacter.SpriteHitsSprite += PlayerCharacterHitsSomething;

            //Ajoute un payload au joueur
            PlayerCharacter.payload = PlayerTSP;

            //Test method, on vérifie si la librairie est bien installé en vérifiant si on peut load le player
            if (PlayerCharacter != null)
            {
                PlayerCharacter.PutBaseImageLocation(constants.PlayerStartingPoint);
                Sprite one = TheGameController.DuplicateSprite(SpriteName.Cloture1.ToString());
                one.payload = ClotureTSP;
                one.PutBaseImageLocation(new Point(50, 65));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture2.ToString());
                one.payload = ClotureTSP;
                one.PutBaseImageLocation(new Point(215, 65));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture3.ToString());
                one.payload = ClotureTSP;
                one.PutBaseImageLocation(new Point(50, 265));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture4.ToString());
                one.payload = ClotureTSP;
                one.PutBaseImageLocation(new Point(215, 265));
                one = TheGameController.DuplicateSprite(SpriteName.Ours.ToString());

                //Ajout animal dans cloture1
                AnimalTSP.nbrAnimalCloture1++;
                AnimalTSP.NiveauFaimCloture1 = 100;
                ClotureTSP.TypeAnimalCloture1 = SpriteName.Ours.ToString();
                ClotureTSP.nbrAnimalCloture1++;
                PlayerTSP.nombreAnimaux++;

                one.payload = AnimalTSP;
                one.PutBaseImageLocation(new Point(100, 100));

            }
            else
            {
                PlayingGame = false;
                MessageBox.Show("Unable to load the primary sprite.  Closing!");
                Close();
            }

            //Le jeu commence maintenant, le timer sert dans toutes les autres méthodes du jeu
            GameStartTime = DateTime.UtcNow;

            //Le jeu commence maintenat, DoTick va maintenant commencer à faire des actions
            PlayingGame = true;
            stats.Visible = true;
        }

        /// <summary>
        /// Méthode qui décrit ce qui arrive si notre character touche un object.
        /// L'objet en question doit être un autre sprite pour causer une réaction. 
        /// Modifie le paramètre interactible pour qu'on puisse afficher le menu d'interaction lorsqu'on est proche d'un object.
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void PlayerCharacterHitsSomething(object Sender, SpriteEventArgs e)
        {
            if (!(Sender is Sprite)) return;
            Sprite Target = (Sprite)Sender;

            if (!PlayingGame) return;

            Target.PutBaseImageLocation(PlayerLastLocation);
            if (Target.payload is PlayerSpritePayload)
            {
                PlayerSpritePayload TempTSP = (PlayerSpritePayload)Target.payload;
                if (e.TargetSprite.payload is VisitorSpritePayload)
                {
                    TempTSP.Interactible = false;
                }
                if (e.TargetSprite.payload is ClotureSpritePayload)
                {
                    TempTSP.Interactible = true;
                    TempTSP.nameLastClotureInteracted = e.TargetSprite.SpriteOriginName.ToString();
                }
                if (e.TargetSprite.payload is TrashSpritePayload)
                {
                    TempTSP.Interactible = true;
                    e.TargetSprite.Destroy();
                }
                Target.payload = TempTSP;
            }
        }


        /// <summary>
        /// Vérifie avant que le player se déplace si c'est un mouvement valide.
        /// s'assure que le player ne sort pas de l'écran.
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
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
            if (e.NewLocation.X > MainDrawingArea.BackgroundImage.Width - constants.DistanceFromSide - PlayerCharacter.GetSize.Width &&
                (Where == Direction.down_right || Where == Direction.right || Where == Direction.up_right))
            {
                e.NewLocation.X = MainDrawingArea.BackgroundImage.Width - constants.DistanceFromSide - PlayerCharacter.GetSize.Width;
            }

            //check top
            if (e.NewLocation.Y < constants.DistanceFromTop + constants.DistanceFromBot && (Where == Direction.up_left || Where == Direction.up || Where == Direction.up_right))
            {
                e.NewLocation.Y = constants.DistanceFromTop + constants.DistanceFromBot;
            }

            //Check bottom
            if (e.NewLocation.Y > constants.TailleDiagonale - constants.DistanceFromBot - PlayerCharacter.GetSize.Height &&
                (Where == Direction.down_right || Where == Direction.down || Where == Direction.down_left))
            {
                e.NewLocation.Y = constants.TailleDiagonale - constants.DistanceFromBot - PlayerCharacter.GetSize.Height;
            }
        }

        /// <summary>
        /// Retourne la direction où le joueur se déplace.
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

        private void ZooTycoon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PlayerCharacter != null)
            {
                double soldeFinal = PlayerTSP.Solde;
                DialogResult result;
                result = MessageBox.Show("Félicitation! Vous avez terminé avec : " + soldeFinal + "$ en poche!", "Fin de la Partie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    PlayerTSP.Interactible = false;
                }
            }

        }

        private void ZooTycoon_Load(object sender, EventArgs e)
        {

        }

        public void refreshStats()
        {
            this.labelDate.Text = "Jour " + (Convert.ToInt32(((PlayerSpritePayload)PlayerCharacter.payload).Jour)).ToString();
            this.labelMoney.Text = ((PlayerSpritePayload)PlayerCharacter.payload).Solde.ToString();
            this.labelAnimals.Text = ((PlayerSpritePayload)PlayerCharacter.payload).nombreAnimaux.ToString();
            //this.labelGarbages.Text = ItemTotalCount("Dechets");
        }
    }

    public enum SpriteName { PlayerCharacter, VisiteurM, VisiteurF, Ours, Chèvre, Rhinoceros, Lion, Cloture1, Cloture2, Cloture3, Cloture4, Trash }
    public enum Direction { none, up, down, left, right, up_left, up_right, down_left, down_right }
}

