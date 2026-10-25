# Évaluation intermédiaire (80 %) – Kiril Solomatin
## Synthèse

Le code est propre, en anglais, structuré (`Config` avec régions, `CollisionHelper`, `enum`). **Il manque les tirs ennemis, la vie du joueur, la fin de partie**.  
La demande du CdC pour 10 ennemis au départ n'a pas besoin d'être traité, ça ne correspondrait pas à l'esprit du thème des échecs.  
Les images sont rechargées depuis le disque à chaque création d'objet.

## Points à améliorer d'ici au rendu final (30.10)

1. Tirs ennemis, vies du joueur, victoire et défaite.
2. Charger les images **une seule fois** (`Properties.Resources` ou champs `static readonly`).
3. PascalCase pour les membres publics.
4. Mettre à jour le rapport (il en est encore à la version du 04.09).
5. Ajouter des tests unitaires
