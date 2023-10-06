# Rappel des commandes pour git :

D'abord forkez sur gitlab, sur le [projet](https://git.unistra.fr/t3_2023/t3)
puis cliquer sur "Fork" en haut de la page.
Configuerez en choisissant le namespace avec votre nom + prénom,
le reste est au choix.

## Commande pour créer le dépôt
git clone [ssh de votre fork]

## Commande pour créer le remote
cd [nom dossier]

git remote add [nom du remote] git@git.unistra.fr:t3_2023/t3.git

git fetch [nom du remote]

**Pour obtenir la liste des remotes en cas d'oubli : git remote**

## Commande pour amener les changements du dépôt principal vers votre forke

git pull [nom du remote] master

## Commande pour pousser les changements de votre dépôt vers le principal

git push [nom du remote] master

## Commande pour push avec unity

git add .
git commit -m ""
git pull
git push
