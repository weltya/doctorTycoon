la branch newproto est la branche par défaut
# Sommaire

- [Le jeu](#doctor-tycoon--lart-de-lassociation-médicale)
- [Comment jouer](#comment-y-jouer)
  - [L'interface graphique](#linterface-graphique)
  - [but du jeu](#but-du-jeu)
  - [les touches](#les-touches)
  - [bugs](#bugs)
- [Description](#description)
- [Contribution](#comment-y-contribuer)
- [questions / signaler un bug](#des-questions--signalement-bug)
# Doctor Tycoon : L'art de l'association médicale

Bienvenue sur notre jeu dans lequel vous pourrez découvrir le monde de la médecine. Dans ce jeu de type Tycoon, vous serez plongé au sein d'un hôpital. Il vous faudra choisir judicieusement les bâtiments à votre disposition afin que vos patients aient la meilleure approche de leur maladie et que tout ce qui l'entoure lui donne envie de se battre contre sa maladie.

Il est important de pouvoir gérer le flux continu de patients tout en s'assurant que ceux-ci sont soignés de la meilleure façon possible afin que leur expérience subjective n'affecte pas de façon négative leur ressenti par rapport à la maladie.

*Voici à quoi ressemble le jeu :*
![Une réception avec des salles d'infirmières et une salle d'attente](https://i.imgur.com/1sd5aoJ.png)

# Comment y jouer ?
Le jeu est à disposition [En ligne](https://berbie.itch.io/t3). Il suffira de cliquer sur le bouton `Run game` pour démarrer la partie.

Pour lancer une partie il faut placer une réception, une salle d'attente, une salle infirmière, une salle médecin.
Les patients effectuent dans l'ordre :
Spawn -> réception -> salle d'attente -> salle infirmier -> salle d'attente -> salle médecin
-> sortie (waypoint) -> supression du personnage.

## l'interface graphique
* l'interface graphique :
* * au centre : le jeu
* * en bas à droite : menu=quitter la partie
* * en bas à gauche : menu construction
* * en haut : jauge et informations sur la partie
* * * expérience subjective :
* * * * jauge rouge : ne prend pas en compte l'expérience subjective
* * * * jauge orange : prend en compte l'experience subjective
* * * * jauge vert : prend bien en compte l'experience subjective
* * * guérison :
* * * * jauge rouge : les patient sont mal soignés
* * * * jauge orange : les patient sont soignés
* * * * jauge vert : les patient sont bien soignés
* * * Salle attente/médecins/infirmières :
* * * * jauge rouge : il manque de la place
* * * * jauge orange : bientôt plus de place
* * * * jauge vert : il y a de la place
* * * patients : nombre de patient au spawn qui attend
* * * * jauge rouge : il manque de la place
* * * * jauge orange : bientôt plus de place
* * * * jauge vert : il y a de la place

## But du jeu
* Il faut que tout les voyants soient au vert

## les touches
* on peut se déplacer avec les fleches
* zoom/dezoom avec la molette de la souris
* R rotation des batiments (bug)
* clique gauche de la souris pour cliquer sur les bouttons


## Bugs
* bug sur la rotation (ne fait pas planter le jeu, mauvais point d'arret des personnages)
* le jeu peut se bloquer parfois si il n'y a pas assez de salle d'attente


# Description 
[Contribution Guidelines](CONTRIBUTING.md)

# Comment y contribuer
[comment y contribue](Description.md)

# Des questions / signalement bug
T3 2023
AZHZHAN Céline, NEU Tanguy, DUCHMANN Leo, WELTY Alexandre

nous contacter / signaler un bug:
welty.alex67@gmail.com

Ou créer une issue (milestone Bug, label Bug)
