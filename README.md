# zoo-tycoon
 TP2 en dev graphique
 Créateurs: Luigi Eid, Nahwa Al-Ansary, Assim Amenas
 
 Fonctionnement:
 
 Déplacement: Touches directionnelles du clavier ou les touches ASDW 
 
 Interactions: touche X lorsque vous êtes en contact avec un object
 
 But du jeu: Se faire le plus d'argent possible! 
 
 Déroulement du jeu: 
 Vous êtes l'unique responsable d'un zoo en campagne, et vous devez tout gérer par vous-même! 
 Pas de concierges ni employés. Vous êtes à la fois le directeur, le concierge et le nourrisseur d'animaux!
 Vous commencez avec 1 seul ours (il faut bien avoir quelque chose pour attirer les clients) et 100$ en poche.
 Puisque c'est en campagne, on est dans un jeu vidéo et que vous êtes populaire, vous avez des visiteurs entrent à chaque 10 secondes. (Voir object visiteurs pour plus d'infos)
 Mais attention! Les visiteurs jettent souvent des déchets et s'il y en a trop, les visiteurs vont arrêter de rentrer! (Voir object déchet pour plus d'infos)
 Appuyez sur la touche x lorsque vous êtes à côté d'un enclot pour intéragir avec celle-ci.
 Vous pouvez nourrir des animaux, ou acheter des animaux et les placer dans l'enclot (certaines conditions s'applique voir l'object enclot pour plus d'infos)
 Nourrissez vos animaux régulièrement pour éviter qu'ils mangent votre argent.
 Le but semble assez simple: garder les animaux remplis, ramasser les déchets et acheter le maximum d'animaux. Mais c'est plus difficile que cela en a l'air!
 
 Objects:
	
	Visiteurs: 
	Les visiteurs entrent à chaque 10 secondes dans le jeu et suivent un parcourt tracé. Pas question qu'ils fassent la fête et sacagent votre terrain non mais!
	Les visiteurs laissent trainer des déchets de temps en temps car leurs parents ne les ont pas éduqués (qu'est ce que vous voulez, c'est la vie)
	Même si c'est eux qui laissent les déchets, s'il y en a trop dans le zoo alors ils vont cessé de rentrer à l'interieur (HA, l'ironie)
	Le client est le roi! Si le visiteur veut passer, vous ne pouvez pas l'arrêter et si vous voulez le dépasser, vous ne pouvez pas le faire car il est trop fort.
	Par contre, il y a des bénifices à avoir des visiteurs! Et oui, c'est votre seul source d'argent! Les visiteurs payent 2$ pour chaque animal que vous avez dans le zoo.
	
	Déchets: 
	Ils sont déposé le long du chemin que suivent les visiteurs dans un petit sac (il ne sont pas SI sale au point de les éparpiller)
	Chaque déchets sur le terrain vous fait perdre 0.10$ à chaque 2 secondes.
	Après un certain nombre de déchets, les visiteurs n'osent plus rentrer dans le zoo alors assurer-vous de les ramasser! 
	Pour ramasser un déchet, vous n'avez qu'à faire passer le personnage par dessus l'object pour qu'il le ramasse.
	
	Enclot:
	Tous les animaux sont dans un enclot (évidemment).
	Appuyez sur x lorsque vous êtes à proximité d'un enclot pour afficher le menu.
	Vous aurez 2 options, Soit nourrir les animaux qui sont dans l'enclot, soit ajouter des animaux. 
	Un enclot peut accueillir qu'un seul type d'animal! Alors faites attention à quelle espèce d'animal vous voulez ajouter.
	L'option nourrir les animaux montre les animaux dans l'enclot et leur niveau de faim (Voir object animal pour plus d'infos)
	
	Animaux:
	4 types d'animaux possibles: Ours, Chèvre, Rhinoceros et Lion. (plus d'animaux et d'enclots disponibles dans des futures MAJ)
	Chaque espèce d'animal à un prix d'achat différent. 
	Vous ne pouvez placer qu'une seule fois chaque espèce animale (ce n'est pas un zoo si vous avez 4 enclots de chèvres!)
	Vous commencez la partie avec un ours. Il faut bien avoir quelque chose d'exceptionnel pour attirer les gens!
	Les animaux d'une même cloture ont une seule barre de faim. Elle commence à 100 lorsque vous les nourrissez et diminue de 5 à chaque 2 secondes.
	Lorsque la barre des animaux atteint le 0, les animaux se mettent à manger votre argent pour survivre (littéralement 1$ à chaque 2 secondes pour chaque animal affamé)
	L'option nourrir donne de la nourriture à tous les animaux de l'enclot. 
	Lorsque vous ajoutez un animal, vous célébrez l'occasion en donnant de la nourriture à tous les animaux de l'enclot.
	Le coût pour nourrir un seul animal est de 1$, ce qui n'est pas cher du tout! Mais est-ce que vous allez être capable de vous rappeler de les nourrir tout en gérant le zoo?
 
 
 