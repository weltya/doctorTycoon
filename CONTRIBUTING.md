# Contribuer au Doctor Tycoon : L'art de l'association médicale

- [Installation](#installation)
  - [Installation locale](#installation-locale)
  - [Packages](#packages)
- [Structure du projet](#structure)
	- [Dossiers](#folders)
- [Git](#git)
    - [Branches](#branches)

## Installation
### Installation locale
**Doctor Tycoon** a été développé sur [Unity](https://unity.com/fr/), plus précisément sur la version **2022.3.11f1** de l'éditeur.

Le dépôt est disponible à cette adresse : https://git.unistra.fr/t3_2023/t3

Afin de pouvoir commencer à développer et contribuer au jeu, assurez-vous tout d'abord de faire un fork du projet depuis votre compte GitHub (https://help.github.com/articles/fork-a-repo/)

Il suffit ensuite de cloner votre fork, en téléchargeant les sources depuis le bouton “clone” de github, ou via un terminal avec commande :
```shell
git clone https://github.com/VOTRE_NOM_UTILISATEUR_GITHUB/t3
```

La branche **newproto** est la principale branche de travail - la branche main correspondant aux versions de productions livrées aux utilisateurs. Il est donc nécessaire de créer de nouvelles branches de travail pour l'ajout et la modification depuis la branche **newproto**. (Voir la section [Utilisation > Git](#git), pour le fonctionnement détaillé).

Certains packages ont été utilisés, mais ceux-ci se téléchargeront seuls lors de l'ouverture du projet dans Unity.

## Structure du projet
### Dossiers

L'arborescence du projet et des dossiers que nous avons créé est établie de la façon suivante :
```
/
└── Assets/
    └── Scenes/
	    ├── MapScene/
	└── Scripts/ 
		└── Gameplay/
		└── Managers/
		└── Models/
		└── UI/
		└── Utils/
```

Le dossier **MapScene** comprend tous les objets de la scène principale du jeu.
Le dossier **Scripts** comprend tous les scripts que nous avons créé, rangés dans le dossier dont le nom correspond le mieux à l'utilité du script.

## Git

### Branches
Afin de travailler sur une nouvelle fonctionnalité, ou un correctif d'une fonctionnalité existante, il est nécessaire de créer une nouvelle branche à partir de la branche `newproto`.
```shell
git checkout -b prefixe/ma-branche newproto
```

Une fois le travail terminé, il faut faire un merge request sur la branche `newproto` du git principal afin de faire accepter ou non le travail accompli.