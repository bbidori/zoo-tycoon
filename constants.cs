using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo_tycoon
{
    public class constants
    {
        public const int DistanceFromSide = 10; //hauteur de la marge de gauche et droite
        public const int DistanceFromBot = 10; //hauteur de la marge du bas
        public const int DistanceFromTop = 10; //hauteur de la marge du haut
        public const int TailleDiagonale = 470; //longeur de l'espace de déplacement

        public const int MaxNumberItems = 10;

        public static Size BackgroundSize { get { return new Size(500, 500); } }
        public static Size EnemySubSize { get { return new Size(50, 20); } }

        public static Size CharacterSize { get { return new Size(30, 40); } } //35,50 parfais
        public static Size OursSize { get { return new Size(40, 55); } }

        public static Size TrashSize { get { return new Size(15, 20); } }
        public static Point PlayerStartingPoint { get { return new Point(420, 150); } }
        public static Point VisiteurStartingPoint { get { return new Point(371, 416); } }
        public static Size VisiteurSize { get { return new Size(25, 35); } } //35,50 parfais

        public static int TimeBetweenReplenishVisitors = 10000; //Temps en MS pour ajouter des visiteurs

        public static int TimeBeforeLosingMoney = 2000; //Temps en MS pour créer de nouveaux déchets

        public static Size ClotureSize { get { return new Size(120, 140); } }

        public static int TimeGenerateNewTrash = 3000; //Temps en MS pour créer de nouveaux déchets

        public static int PlayerSpeedLeftRight = 5;
        public static int PlayerSpeedUpDown = 8;
        public static int PlayerSpeedMixDirection = 8;

        public static int VisiteurSpeed = 2;

    }
}
