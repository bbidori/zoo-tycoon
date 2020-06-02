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


        Sprite PlayerCharacter = null;   //Le sprite du personnage
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
                        Console.WriteLine("//////////////////////////////////Trash Throwed Now////////////////////////////////////////////////////");
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
                        StartGame();
                    //Quitte le DoTick si on a pas commencé le jeu
                    return;
                }
                //Vérifie si on a appuyé sur une touche
                CheckForKeyPress(Sender, e);
            }


            //Ajoute des visiteurs selon le temps passé depuis la dernière fois qu'on a ajouté (ajoute à chaque 15 secondes)
            if ((DateTime.UtcNow - LastAddItem).TotalMilliseconds > constants.TimeBetweenReplenishVisitors)
            {
                LastAddItem = DateTime.UtcNow;

                //ItemTotalCount compte tous les items qu'il y a dans le jeu en ce moment
                int Items = ItemTotalCount();

                //Si le parc n'a pas trop de déchets ou trop de visiteurs
                if (Items < constants.MaxNumberItems)
                {
                    CreateVisitor();
                }
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
        }

        private int ItemTotalCount()
        {
            int Counter = 0;

            foreach (Sprite one in TheGameController.SpritesBasedOffAnything())
            {
                if (one.SpriteOriginName == SpriteName.VisiteurM.ToString())
                    Counter++;
                if (one.SpriteOriginName == SpriteName.VisiteurF.ToString())
                    Counter++;
                if (one.SpriteOriginName == SpriteName.Trash.ToString())
                    Counter++;
            }
            return Counter;
        }

        /// <summary>
        /// Vérifie si une touche qui a été appuyé cause une action. Cette méthode est appelé presque chaque milliseconde
        /// </summary>
        private void CheckForKeyPress(object sender, EventArgs e)
        {
            //cherche le payload du sprite pour savoir s'il est proche d'un autre sprite et qu'il peut intéragir ou non
            PlayerSpritePayload TempTSP = (PlayerSpritePayload)PlayerCharacter.payload;
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
                if (TempTSP.Interactible)
                {
                    isInteracting = true;
                    PlayerCharacter.MovementSpeed = 0;
                    LastDirection = Direction.none;
                    MenuInterraction();
                }
                else
                {
                    isInteracting = true;
                    PlayerCharacter.MovementSpeed = 0;
                    LastDirection = Direction.none;
                    //Pour Assim, appelle la nouvelle form qui montre les infos du personnage (ou les infos générales du jeu) ici.
                    //Pour savoir les données du perso, tu dois faire les commandes suivantes:
                    /*
                     * PlayerSpritePayload TempTSP = (PlayerSpritePayload)PlayerCharacter.payload;
                     * int soldeDuJoueur = TempTSP.Solde
                     * int nombre d'animaux = TempTSP.nombreAnimaux;
                     */
                    //si tu veux ajouter d'autres données pour le joueur, il faut que tu ailles dans PlayerSpritePayload pour les ajouter
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


                    TempTSP.Interactible = false;

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

                    TempTSP.Interactible = false;
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

                    TempTSP.Interactible = false;
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

                    TempTSP.Interactible = false;
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

                    TempTSP.Interactible = false;
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

                    TempTSP.Interactible = false;
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

                    TempTSP.Interactible = false;
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

                    TempTSP.Interactible = false;
                }
                didsomething = true;
            }

            // si on n'appuie plus sur une case de déplacement, on arrete de bouger
            if (!didsomething)
            {
                PlayerCharacter.MovementSpeed = 0;
                LastDirection = Direction.none;
            }

            PlayerCharacter.payload = TempTSP;
            if (didsomething)
            {
                Console.WriteLine(PlayerLastLocation);
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
            PlayerSpritePayload PlayerTSP = new PlayerSpritePayload();
            PlayerCharacter.payload = PlayerTSP;
            AnimalSpritePayload AnimalTPS = new AnimalSpritePayload();

            //Test method, on vérifie si la librairie est bien installé en vérifiant si on peut load le player
            if (PlayerCharacter != null)
            {
                PlayerCharacter.PutBaseImageLocation(constants.PlayerStartingPoint);
                Sprite one = TheGameController.DuplicateSprite(SpriteName.Cloture1.ToString());
                one.payload = AnimalTPS;
                one.PutBaseImageLocation(new Point(50, 65));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture2.ToString());
                one.payload = AnimalTPS;
                one.PutBaseImageLocation(new Point(215, 65));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture3.ToString());
                one.payload = AnimalTPS;
                one.PutBaseImageLocation(new Point(50, 265));
                one = TheGameController.DuplicateSprite(SpriteName.Cloture4.ToString());
                one.payload = AnimalTPS;
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

            //Console.WriteLine(e.TargetSprite.SpriteOriginName.ToString()); 


            if (!PlayingGame) return;

            Target.PutBaseImageLocation(PlayerLastLocation);
            if (Target.payload is PlayerSpritePayload)
            {
                PlayerSpritePayload TempTSP = (PlayerSpritePayload)Target.payload;
                if (e.TargetSprite.payload is VisitorSpritePayload)
                {
                    TempTSP.Interactible = false;
                }
                if (e.TargetSprite.payload is AnimalSpritePayload)
                {
                    TempTSP.Interactible = true;
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
                PlayerSpritePayload PlayerTSP = (PlayerSpritePayload)PlayerCharacter.payload;
                int soldeFinal = PlayerTSP.Solde;
                DialogResult result;
                result = MessageBox.Show("Félicitation! Vous avez terminé avec : " + soldeFinal + "$ en poche!", "Fin de la Partie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    isInteracting = false;
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
            this.labelGarbages.Text = ((PlayerSpritePayload)PlayerCharacter.payload).Solde.ToString();
        }
    }

    public enum SpriteName { PlayerCharacter, VisiteurM, VisiteurF, Ours, Chèvre, Girafe, Cloture1, Cloture2, Cloture3, Cloture4, Trash }
    public enum Direction { none, up, down, left, right, up_left, up_right, down_left, down_right }
}

